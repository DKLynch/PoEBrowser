using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using PoEBrowser.Services;
using PoEBrowser.Models;
using System.Text.RegularExpressions;

namespace PoEBrowser.Controllers
{
    [Route("Essences")]
    public class EssenceController : Controller
    {
        private readonly DBContext dB;

        public EssenceController(DBContext dBContext)
        {
            dB = dBContext;
        }

        // GET: /Essences/
        [Route("")]
        [ActionName("GetEssences")]
        public ActionResult GetEssences()
        {
            var model = new List<Essence>();
            var query = from e in dB.Essences.AsQueryable()
                        join b in dB.BaseItems.AsQueryable() on e.Name equals b.ItemName
                        select new Essence()
                        {
                            Name = e.Name,
                            Level = e.Level,
                            LevelRestriction = (e.LevelRestriction == BsonNull.Value) ? 100 : e.LevelRestriction,
                            IsCorruptionOnly = e.IsCorruptionOnly,
                            Mods = e.Mods,
                            VisualIdentity = b.VisualIdentity
                        };

            model = query.ToList();
            model.ForEach(x =>
            {
                SetEssenceType(x);
                SetEssenceImgSrc(x);
            });

            var grouped = model.OrderBy(x => x.Type).ThenBy(x => x.Level).ToList();
            model = grouped;
            return View("GetEssences", model);
        }

        // Utilizes regex to determine the last word in the name string to classify each essence's type
        private void SetEssenceType(Essence essence)
        {
            if (essence.IsCorruptionOnly)
            {
                essence.Type = "Corruption Only";
            }
            else
            {
                if (!essence.Name.Contains("Remnant"))
                {
                    string pattern = @"\b(\w+)$";
                    essence.Type = Regex.Match(essence.Name, pattern).ToString();
                }
                else
                {
                    essence.Type = "Corruption";
                }
            }
        }

        // Manipulates the visual identity string in order to generate the required link from the PoE CDN
        private void SetEssenceImgSrc(Essence essence)
        {
            var raw = (string)essence.VisualIdentity.GetValueOrDefault("dds_file", "");
            essence.ImgSrcString = new string("https://web.poecdn.com/image/" + raw.Split('.')[0] + ".png?scale=1");
        }
    }
}