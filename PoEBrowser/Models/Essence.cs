using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PoEBrowser.Models
{
    public class Essence
    {
        [BsonElement("item_level_restriction")]
        public int LevelRestriction { get; set; }

        [BsonElement("level")]
        public int Level { get; set; }

        [BsonElement("mods")]
        public Dictionary<string, string> Mods { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("spawn_level_min")]
        public int SpawnLevelMin { get; set; }

        [BsonElement("spawn_level_max")]
        public int SpawnLevelMax { get; set; }

        [BsonElement("type")]
        public Dictionary<string, object> OtherProperties { get; set; }

        public bool IsCorruptionOnly { get; set; }
        public Dictionary<string, object> VisualIdentity { get; set; }
        public string Type { get; set; }
        public string ImgSrcString { get; set; }
    }
}
