using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using PoEBrowser.Services;
using PoEBrowser.Models;

namespace PoEBrowser.Controllers
{
    [Route("DivinationCards")]
    public class DivinationCardController : Controller
    {
        // Constructor + Dependency Injection
        private readonly DBContext dB;

        public DivinationCardController(DBContext dBContext)
        {
            dB = dBContext;
        }

        // GET: /DivinationCards
        [Route("")]
        [ActionName("GetDivinationCards")]
        public ActionResult GetDivinationCards([FromQuery] string q)
        {
            var query = from b in dB.BaseItems.AsQueryable()
                        where b.ReleaseState == "released" &&
                              b.ItemClass == "DivinationCard"
                        orderby b.ItemName
                        select new DivinationCard()
                        {
                            ItemName = b.ItemName,
                            VisualIdentity = b.VisualIdentity,
                            DropLevel = b.DropLevel,
                            Properties = b.Properties,
                            Tags = b.Tags
                        };

            var items = query.ToList();
            
            // Filter our collection if a query parameter is passed in the URL
            if (!string.IsNullOrEmpty(q))
            {
                var searchTerm = q.ToLower();

                items = items.Where(i => 
                                    i.ItemName.ToLower().Contains(searchTerm) || 
                                    i.Tags.Contains(searchTerm) || 
                                    i.Properties.GetValueOrDefault("description", "").ToString().ToLower().Contains(searchTerm))
                                    .ToList();
            }
            
            // 
            items.ForEach(x => {
                SetProperties(x);
                SetImgSrc(x);
                FormatDescriptionForHTML(x);
                });

            return View("GetDivinationCards", items);
        }

        // GET: /DivinationCards/TheDoctor
        [Route("{name}")]
        [ActionName("GetDivinationCard")]
        public ActionResult GetDivinationCard(string name)
        {
            var model = new DivinationCard();
            var query = from b in dB.BaseItems.AsQueryable()
                        where b.ReleaseState == "released" &&
                              b.ItemClass == "DivinationCard" &&
                              b.ItemName.ToLower() == name.ToLower()
                        orderby b.ItemName
                        select new DivinationCard()
                        {
                            ItemName = b.ItemName,
                            VisualIdentity = b.VisualIdentity,
                            DropLevel = b.DropLevel,
                            Properties = b.Properties,
                            Tags = b.Tags
                        };

            model = query.FirstOrDefault();

            SetProperties(model);
            SetImgSrc(model);
            FormatDescriptionForHTML(model);

            return View("GetDivinationCard", model);
        }

        // Manipulates the visual identity string in order to generate the required link from the PoE CDN
        private void SetImgSrc(DivinationCard divCard)
        {
            var raw = (string)divCard.VisualIdentity.GetValueOrDefault("id", "");
            divCard.CardArtString = new string("https://web.poecdn.com/image/divination-card/" + raw.Split('.')[0] + ".png");
        }

        // Workaround method to set individual item properties after the fact
        // Select statement in the query does not support GetValueOrDefault unfortunately.
        private void SetProperties(DivinationCard divCard)
        {
            divCard.Description = (string)divCard.Properties.GetValueOrDefault("description", "");
            divCard.StackSize = (int)divCard.Properties.GetValueOrDefault("stack_size", 0);
            divCard.CurrencyTabStackSize = (int)divCard.Properties.GetValueOrDefault("stack_size_currency_tab", 0);
            divCard.FlavourText = (string)divCard.Properties.GetValueOrDefault("flavour_text", "");
            
            // Grab the areas_found element
            divCard.Properties.ToBsonDocument().TryGetElement("areas_found", out BsonElement areasFound);
            if(areasFound.Value != null)
            {
                // Convert areas_found to BsonArray and select into a normal array
                var areas = areasFound.Value.AsBsonArray;
                divCard.DropAreas = areas.Select(x => x.ToString()).ToArray();
            }
        }

        // Alters the raw description data to use respective HTML tags and formatting.
        // Uses RegEx to pair formatting tags and their content to be rendered.
        private void FormatDescriptionForHTML(DivinationCard divCard)
        {   
            // Matches any <*> followed by {*} and captures the contents of both as capture groups.
            string pattern = @"<([a-zA-Z]*)>{([a-zA-Z0-9ö \t+%:'.,-]*)}";
            Regex rgx = new Regex(pattern);
            var matchList = rgx.Matches(divCard.Description).ToList();

            bool openSpanTag = false;
            foreach (Match match in rgx.Matches(divCard.Description))
            {
                var wholeMatch = match.Groups[0].ToString();
                var cssClass = match.Groups[1].ToString();
                var content = match.Groups[2].ToString();

                // If we have a situation like "Item Level:", we don't want to break into a new line until after the.
                // post colon data is displayed.
                var containsColon = wholeMatch.Contains(":");
                
                // If the base string contains a colon, we add the lineContainer wrapper tag.
                var baseString = containsColon ? $"<span class=\"lineContainer\"><span class=\"{cssClass}\">{content}</span>" : $"<span class=\"{cssClass}\">{content}</span>";
                
                // If there's an unclosed lineContainer tag, we close it.
                var treatedString = openSpanTag ? $"{baseString}</span>" : baseString;
                if (openSpanTag)
                    openSpanTag = false;
                
                // Replace the original regex match with our treated string with html tags.
                divCard.Description = divCard.Description.Replace($"<{cssClass}>{{{content}}}", treatedString);

                if (containsColon)
                    openSpanTag = true;

            }
        }
    }
}
