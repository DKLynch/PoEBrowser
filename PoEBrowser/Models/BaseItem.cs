using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace PoEBrowser.Models
{
    public class BaseItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string ItemName { get; set; }

        [BsonElement("domain")]
        public string Domain { get; set; }

        [BsonElement("item_class")]
        public string ItemClass { get; set; }

        [BsonElement("inventory_height")]
        public int InventoryHeight { get; set; }

        [BsonElement("inventory_width")]
        public int InventoryWidth { get; set; }

        [BsonElement("implicits")]
        public string[] Implicits { get; set; }

        [BsonElement("drop_level")]
        public int DropLevel { get; set; }

        [BsonElement("release_state")]
        public string ReleaseState { get; set; }

        [BsonElement("tags")]
        public string[] Tags { get; set; }

        [BsonElement("properties")]
        [BsonIgnoreIfNull]
        public Dictionary<string, object> Properties { get; set; }

        [BsonElement("requirements")]
        public Dictionary<string, int> Requirements { get; set; }

        [BsonElement("visual_identity")]
        public Dictionary<string, object> VisualIdentity { get; set; }

        [BsonExtraElements]
        [BsonIgnoreIfNull]
        public Dictionary<string, object> ExtraValues { get; set; }

        public string ImgSrcString { get; set; }
    }
}