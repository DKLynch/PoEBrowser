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
    [Route("Currency")]
    public class CurrencyController : Controller
    {
        private readonly DBContext dB;

        public CurrencyController(DBContext dBContext)
        {
            dB = dBContext;
        }

        [Route("")]
        [ActionName("GetCurrencyItems")]
        public ActionResult GetAllCurrencyItems()
        {
            var model = new List<CurrencyItem>();
            var query = from b in dB.BaseItems.AsQueryable()
                        where b.ReleaseState != "unreleased" &&
                              b.ItemClass == "StackableCurrency" &&
                              !b.ItemName.Contains("Fossil") &&
                              !b.ItemName.Contains("Essence") &&
                              !b.ItemName.Contains("Remnant") &&
                              b.ItemName != "Enchant"
                        select new CurrencyItem()
                        {
                            ItemName = b.ItemName,
                            VisualIdentity = b.VisualIdentity,
                            Tags = b.Tags
                        };

            model = query.ToList();
            model.ForEach(x =>
            {
                SetCurrencyImgSrc(x);
                SetCurrencyType(x);
            });
   
            var grouped = model.OrderBy(x => x.ItemClass).ThenBy(x => x.ItemName).ToList();
            model = grouped;
            return View("GetCurrencyItems", model);
        }

        [Route("{name}")]
        [ActionName("GetCurrencyItem")]
        public ActionResult GetCurrencyItem(string name)
        {
            var model = new CurrencyItem();
            var query = from b in dB.BaseItems.AsQueryable()
                        where b.ReleaseState != "unreleased" &&
                              b.ItemClass == "StackableCurrency" &&
                              !b.ItemName.Contains("Fossil") &&
                              !b.ItemName.Contains("Essence") &&
                              !b.ItemName.Contains("Remnant") &&
                              b.ItemName != "Enchant" &&
                              b.ItemName == name
                        select new CurrencyItem()
                        {
                            ItemName = b.ItemName,
                            VisualIdentity = b.VisualIdentity,
                            Tags = b.Tags,
                            DropLevel = b.DropLevel,
                            Properties = b.Properties                        
                        };

            model = query.FirstOrDefault();
            SetCurrencyImgSrc(model);
            SetCurrencyType(model);
            GetCurrencyProperties(model);

            return View("GetCurrencyItem", model);
        }

        // Manipulates the visual identity string in order to generate the required link from the PoE CDN
        private void SetCurrencyImgSrc(CurrencyItem currency)
        {
            var raw = (string)currency.VisualIdentity.GetValueOrDefault("dds_file", "Art/2DItems/Armours/BodyArmours/BodyDex1A.dds");
            currency.ImgSrcString = new string("https://web.poecdn.com/image/" + raw.Split('.')[0] + ".png?scale=1");
        }

        // Slightly messy implementation simply setting currency types depending on the contents of their names
        private void SetCurrencyType(CurrencyItem currency)
        {
            if (currency.ItemName.Contains("Bestiary") || currency.ItemName.Contains(" Net") || currency.ItemName.Contains("Imprint"))
            {
                currency.ItemClass = "Bestiary";
            }
            else if (currency.ItemName.Contains("Splinter of") || currency.ItemName.Contains("Blessing of"))
            {
                currency.ItemClass = "Breach";
            }
            else if (currency.ItemName.Contains("Timeless"))
            {
                currency.ItemClass = "Legion";
            }
            else if (currency.ItemName.Contains("Oil"))
            {
                currency.ItemClass = "Blight";
            }
            else if (currency.ItemName.Contains("Vial"))
            {
                currency.ItemClass = "Incursion";
            }
            else if ((currency.ItemName.Contains("Cartographer") || currency.ItemName.Contains("Unshaping")) && !currency.ItemName.Contains("Chisel"))
            {
                currency.ItemClass = "Atlas";
            }
            else if (currency.ItemName == "Prophecy" || currency.ItemName == "Silver Coin")
            {
                currency.ItemClass = "Prophecy";
            }
            else
            {
                currency.ItemClass = "Currency";
            }
        }

        // Workaround method to set individual item properties after the fact
        // Select statement in the query does not support GetValueOrDefault unfortunately.
        private void GetCurrencyProperties(CurrencyItem currency)
        {
            currency.Description = (string)currency.Properties.GetValueOrDefault("description", "");
            currency.Directions = (string)currency.Properties.GetValueOrDefault("directions", "");
            currency.StackSize = (int)currency.Properties.GetValueOrDefault("stack_size", 0);
            currency.CurrencyTabStackSize = (int)currency.Properties.GetValueOrDefault("stack_size_currency_tab", 0);
        }
    }
}

    