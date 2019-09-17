using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEBrowser.Models
{
    public class CurrencyItem
    {
        public string ItemName { get; set; }
        public Dictionary<string, object> VisualIdentity { get; set; }
        public string[] Tags { get; set; }
        public string ItemClass { get; set; }
        public string ImgSrcString { get; set; }
    }
}
