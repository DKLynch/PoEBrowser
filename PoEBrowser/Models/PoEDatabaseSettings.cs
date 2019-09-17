using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoEBrowser.Models
{
    public class PoEDatabaseSettings : IPoEDatabaseSettings
    {
        public string BaseItemsCollectionName { get; set; }
        public string SkillGemsCollectionName { get; set; }
        public string EssencesCollectionName { get; set; }
        public string StatTranslationsCollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }

    public interface IPoEDatabaseSettings
    {
        string BaseItemsCollectionName { get; set; }
        string SkillGemsCollectionName { get; set; }
        string EssencesCollectionName { get; set; }
        string StatTranslationsCollectionName { get; set; }
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}