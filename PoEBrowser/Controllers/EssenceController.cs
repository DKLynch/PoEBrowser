﻿using System;
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
        // Constructor + Dependency Injection
        private readonly DBContext dB;

        public EssenceController(DBContext dBContext)
        {
            dB = dBContext;
        }

        // GET: /Essences/
        [Route("")]
        [ActionName("GetEssences")]
        public ActionResult GetEssences([FromQuery] string q)
        {
            var query = from e in dB.Essences.AsQueryable()
                        join b in dB.BaseItems.AsQueryable() on e.Name equals b.ItemName
                        select new Essence()
                        {
                            Name = e.Name,
                            Level = e.Level,
                            LevelRestriction = (e.LevelRestriction == BsonNull.Value) ? 0 : e.LevelRestriction,
                            IsCorruptionOnly = e.IsCorruptionOnly,
                            Mods = e.Mods,
                            VisualIdentity = b.VisualIdentity
                        };

            var items = query;

            if (!string.IsNullOrEmpty(q))
            {
                var searchTerm = q.ToLower();
                items = items.Where(i => i.Name.ToLower().Contains(searchTerm));
            }

            var model = items.ToList();

            model.ForEach(x =>
            {
                SetType(x);
                SetImgSrc(x);
            });

            var grouped = model.OrderBy(x => x.Type).ThenBy(x => x.Level).ToList();
            model = grouped;
            return View("GetEssences", model);
        }

        [Route("{name}")]
        [ActionName("GetEssenceByName")]
        public ActionResult GetEssenceByName(string name)
        {
            var model = new Essence();
            var query = from e in dB.Essences.AsQueryable()
                        join b in dB.BaseItems.AsQueryable() on e.Name equals b.ItemName
                        where e.Name == name
                        select new Essence()
                        {
                            Name = e.Name,
                            Level = e.Level,
                            LevelRestriction = (e.LevelRestriction == BsonNull.Value) ? 0 : e.LevelRestriction,
                            SpawnLevelMin = e.SpawnLevelMin,
                            SpawnLevelMax = e.SpawnLevelMax,
                            IsCorruptionOnly = e.IsCorruptionOnly,
                            Mods = e.Mods,
                            OtherProperties = e.OtherProperties,
                            VisualIdentity = b.VisualIdentity,
                        };

            model = query.FirstOrDefault();
            SetType(model);
            SetImgSrc(model);

            return View("GetEssence", model);
        }

        // Utilizes regex to determine the last word in the name string to classify each essence's type
        private void SetType(Essence essence)
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
        private void SetImgSrc(Essence essence)
        {
            var raw = (string)essence.VisualIdentity.GetValueOrDefault("dds_file", "");
            essence.ImgSrcString = new string("https://web.poecdn.com/image/" + raw.Split('.')[0] + ".png?scale=1");
        }
    }
}
