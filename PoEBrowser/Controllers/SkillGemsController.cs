using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PoEBrowser.Services;
using PoEBrowser.Models;

namespace PoEBrowser.Controllers
{
    [Route("SkillGems")]
    public class SkillGemsController : Controller
    {
        private readonly DBContext dB;

        public SkillGemsController(DBContext dBContext)
        {
            dB = dBContext;
        }

        // GET: /SkillGems
        [Route("Active")]
        public ActionResult GetActiveSkillGems()
        {
            var query = from g in dB.SkillGems.AsQueryable()
                        join b in dB.BaseItems.AsQueryable() on g.BaseItem.DisplayName equals b.ItemName
                        where g.BaseItem.ReleaseState != "unreleased" &&
                              g.IsSupport == false
                        orderby g.BaseItem.DisplayName
                        select new SkillGem()
                        {
                            GemName = g.BaseItem.DisplayName,
                            Tags = g.Tags,
                            IsSupport = g.IsSupport,
                            VisualIdentity = b.VisualIdentity
                        };

            var gems = query.ToList();
            var model = SortSkillGems(gems);
            return View("GetSkillGems", model);
        }

        // GET: /SkillGems/type=cold
        [Route("Active/type={type}")]
        public ActionResult GetActiveSkillGemsByType(string type)
        {
            var query = from g in dB.SkillGems.AsQueryable()
                        join b in dB.BaseItems.AsQueryable() on g.BaseItem.DisplayName equals b.ItemName
                        where g.BaseItem.ReleaseState != "unreleased" &&
                              g.IsSupport == false &&
                              g.Tags.Contains(type)
                        orderby g.BaseItem.DisplayName
                        select new SkillGem()
                        {
                            GemName = g.BaseItem.DisplayName,
                            Tags = g.Tags,
                            IsSupport = g.IsSupport,
                            VisualIdentity = b.VisualIdentity
                        };

            var gems = query.ToList();
            var model = SortSkillGems(gems);
            return View("GetSkillGems", model);
        }

        // GET: /SkillGems/Precision
        [Route("{name}")]
        public ActionResult GetActiveSkillGemByName(string name)
        {
            var gQuery = from g in dB.SkillGems.AsQueryable()
                         where g.BaseItem.ReleaseState != "unreleased" &&
                               g.IsSupport == false &&
                               g.BaseItem.DisplayName == name
                         select g;
            var gem = gQuery.FirstOrDefault();

            var bQuery = from b in dB.BaseItems.AsQueryable()
                         where b.ItemName == gem.BaseItem.DisplayName
                         select b;
            var baseItem = bQuery.FirstOrDefault();

            gem.VisualIdentity = baseItem.VisualIdentity;
            SetSkillGemImgSource(gem);
            var model = gem;
            return View("GetSkillGem", model);
        }

        // GET: /SkillGems/Random
        [Route("Random")]
        public ActionResult GetRandomActiveSkillGem()
        {
            var query = from g in dB.SkillGems.AsQueryable()
                        join b in dB.BaseItems.AsQueryable() on g.BaseItem.DisplayName equals b.ItemName
                        where g.BaseItem.ReleaseState != "unreleased" &&
                              g.IsSupport == false
                        orderby g.BaseItem.DisplayName
                        select new SkillGem()
                        {
                            GemName = g.BaseItem.DisplayName,
                            Tags = g.Tags,
                            IsSupport = g.IsSupport,
                            VisualIdentity = b.VisualIdentity
                        };

            var gems = query.ToList();

            var r = new Random();
            var model = gems[r.Next(0, gems.Count())];
            return View("GetSkillGem", model);
        }

        // GET: /SkillGems/Support
        [Route("Support")]
        public ActionResult GetSupportSkillGems()
        {
            var query = from g in dB.SkillGems.AsQueryable()
                        join b in dB.BaseItems.AsQueryable() on g.BaseItem.DisplayName equals b.ItemName
                        where g.BaseItem.ReleaseState != "unreleased" &&
                              g.IsSupport == true
                        orderby g.BaseItem.DisplayName
                        select new SkillGem()
                        {
                            GemName = g.BaseItem.DisplayName,
                            Tags = g.Tags,
                            IsSupport = g.IsSupport,
                            VisualIdentity = b.VisualIdentity
                        };

            var gems = query.ToList();
            var model = SortSkillGems(gems);
            return View("GetSkillGems", model);
        }

        // GET: /SkillGems/Support/Combustion
        [Route("Support/{name}")]
        public ActionResult GetSupportSkillGemByName(string name)
        {
            var gQuery = from g in dB.SkillGems.AsQueryable()
                         where g.BaseItem.ReleaseState != "unreleased" &&
                               g.IsSupport == true &&
                               g.BaseItem.DisplayName == name
                         select g;
            var gem = gQuery.FirstOrDefault();

            var bQuery = from b in dB.BaseItems.AsQueryable()
                         where b.ItemName == gem.BaseItem.DisplayName
                         select b;
            var baseItem = bQuery.FirstOrDefault();

            gem.VisualIdentity = baseItem.VisualIdentity;
            SetSkillGemImgSource(gem);
            var model = gem;
            return View("GetSkillGem", model);
        }

        // GET: /SkillGems/Support/type=cold
        [Route("Support/type={type}")]
        public ActionResult GetSupportSkillGemsByType(string type)
        {
            var query = from g in dB.SkillGems.AsQueryable()
                        join b in dB.BaseItems.AsQueryable() on g.BaseItem.DisplayName equals b.ItemName
                        where g.BaseItem.ReleaseState != "unreleased" &&
                              g.IsSupport == true &&
                              g.Tags.Contains(type)
                        orderby g.BaseItem.DisplayName
                        select new SkillGem()
                        {
                            GemName = g.BaseItem.DisplayName,
                            Tags = g.Tags,
                            IsSupport = g.IsSupport,
                            VisualIdentity = b.VisualIdentity
                        };

            var gems = query.ToList();
            var model = SortSkillGems(gems);
            return View("GetSkillGems", model);
        }

        // Divides the skill gems into lists depending on their respective gem type
        private SkillGems SortSkillGems(List<SkillGem> skillGems)
        {
            var output = new SkillGems();

            foreach (var gem in skillGems)
            {
                SetSkillGemImgSource(gem);

                if (gem.Tags.Contains("dexterity"))
                {
                    gem.GemType = "dexterity";
                    output.DexSkillGems.Add(gem);
                }
                else if (gem.Tags.Contains("intelligence"))
                {
                    gem.GemType = "intelligence";
                    output.IntSkillGems.Add(gem);
                }
                else if (gem.Tags.Contains("strength"))
                {
                    gem.GemType = "strength";
                    output.StrSkillGems.Add(gem);
                }
                else
                {
                    gem.GemType = "other";
                    output.OtherSkillGems.Add(gem);
                }
            }
            return output;
        }

        // Manipulates the visual identity string in order to generate the required link from the PoE CDN
        private void SetSkillGemImgSource(SkillGem skillGem)
        {
            var raw = (string)skillGem.VisualIdentity.GetValueOrDefault("dds_file", "");
            skillGem.ImgSrcString = "https://web.poecdn.com/image/" + raw.Split('.')[0] + ".png?scale=1";
        }
    }
}