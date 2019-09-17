using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using PoEBrowser.Models;
using System.Text.RegularExpressions;

namespace PoEBrowser.Services
{
    public class DBContext
    {
        public readonly IMongoCollection<SkillGem> SkillGems;
        public readonly IMongoCollection<BaseItem> BaseItems;
        public readonly IMongoCollection<Essence> Essences;

        public DBContext(IPoEDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            SkillGems = database.GetCollection<SkillGem>(settings.SkillGemsCollectionName);
            BaseItems = database.GetCollection<BaseItem>(settings.BaseItemsCollectionName);
            Essences = database.GetCollection<Essence>(settings.EssencesCollectionName);
        }
    }
}