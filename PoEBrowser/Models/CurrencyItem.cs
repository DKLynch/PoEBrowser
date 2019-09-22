using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEBrowser.Models
{
    public class CurrencyItem
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Directions { get; set; }
        public int DropLevel { get; set; }
        public int StackSize { get; set; }
        public int CurrencyTabStackSize { get; set; }
        public Dictionary<string, object> VisualIdentity { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public string[] Tags { get; set; }
        public string ItemClass { get; set; }
        public string ImgSrcString { get; set; }
    }
}
