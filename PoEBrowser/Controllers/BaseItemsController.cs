﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using PoEBrowser.Services;
using PoEBrowser.Models;

namespace PoEBrowser.Controllers
{
    [Route("BaseItems")]
    public class BaseItemsController : Controller
    {
        private readonly DBContext dB;

        public BaseItemsController(DBContext dBContext)
        {
            dB = dBContext;
        }

        // GET: /BaseItems/
        [Route("")]
        [ActionName("GetBaseItems")]
        public ActionResult GetBaseItems()
        {
            var model = new List<BaseItem>();
            var query = from b in dB.BaseItems.AsQueryable()
                        where b.ReleaseState != "unreleased" &&
                              b.ItemClass != "Active Skill Gem" &&
                              b.ItemClass != "Support Skill Gem" &&
                              b.ItemClass != "StackableCurrency"
                        orderby b.ItemClass ascending, b.ItemName ascending
                        select b;

            model = query.ToList();
            model.ForEach(x => SetBaseItemImgSrc(x)); 
            return View("GetBaseItems", model);
        }

        // GET: /BaseItems/type=Body%20Armour
        [Route("type={type}")]
        public ActionResult GetBaseItemsByType(string type)
        {
            var model = new List<BaseItem>();
            var query = from b in dB.BaseItems.AsQueryable()
                        where b.ReleaseState != "unreleased" &&
                              b.ItemClass != "Active Skill Gem" &&
                              b.ItemClass != "Support Skill Gem" &&
                              b.ItemClass != "StackableCurrency" &&
                              b.ItemClass == type
                        orderby b.ItemClass ascending, b.ItemName ascending
                        select b;

            model = query.ToList();
            model.ForEach(x => SetBaseItemImgSrc(x));
            return View("GetBaseItems", model);
        }

        // GET: /BaseItems/Sorcerer%20Boots
        [Route("{name}")]
        public ActionResult GetBaseItemByName(string name)
        {
            var query = from b in dB.BaseItems.AsQueryable()
                        where b.ReleaseState != "unreleased" &&
                              b.ItemClass != "Active Skill Gem" &&
                              b.ItemClass != "Support Skill Gem" &&
                              b.ItemClass != "StackableCurrency" &&
                              b.ItemName == name
                        select b;

            var model = query.FirstOrDefault();
            SetBaseItemImgSrc(model);
            return View("GetBaseItem", model);
        }

        // Manipulates the visual identity string in order to generate the required link from the PoE CDN
        private void SetBaseItemImgSrc(BaseItem baseItem)
        {
            var raw = (string)baseItem.VisualIdentity.GetValueOrDefault("dds_file", "");
            baseItem.ImgSrcString = new string("https://web.poecdn.com/image/" + raw.Split('.')[0] + ".png?scale=1");
        }
    }
}