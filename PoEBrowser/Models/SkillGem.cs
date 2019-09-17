using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace PoEBrowser.Models
{
    [BsonIgnoreExtraElements]
    public class SkillGem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string GemName { get; set; }
        public string ImgSrcString { get; set; }
        public string GemType { get; set; }

        public Dictionary<string, object> VisualIdentity { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("active_skill")]
        public ActiveSkill ActiveSkill { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("base_item")]
        public BaseItemData BaseItem { get; set; }

        [BsonElement("cast_time")]
        public int CastTime { get; set; }

        [BsonElement("is_support")]
        public bool IsSupport { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("per_level")]
        public Dictionary<string, LevelData> PerLevel { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("stat_translation_file")]
        public string StatTranslationFile { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("static")]
        public StaticData Static { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("tags")]
        public string[] Tags { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("projectile_speed")]
        public int ProjectileSpeed { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("secondary_granted_effect")]
        public string SecondaryEffect { get; set; }
    }

    public class ActiveSkill
    {
        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("display_name")]
        public string DisplayName { get; set; }

        [BsonElement("id")]
        public string ID { get; set; }

        [BsonElement("is_manually_casted")]
        public bool IsManuallyCasted { get; set; }

        [BsonElement("is_skill_totem")]
        public bool IsSkillTotem { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("skill_totem_life_multiplier")]
        public double TotemLifeMultiplier { get; set; }

        [BsonElement("minion_types")]
        public string[] MinionTypes { get; set; }

        [BsonElement("stat_conversions")]
        public Dictionary<string, string> StatConversions { get; set; }

        [BsonElement("types")]
        public string[] Types { get; set; }

        [BsonElement("weapon_restrictions")]
        public string[] WeaponRestrictions { get; set; }
    }

    public class BaseItemData
    {
        [BsonElement("display_name")]
        public string DisplayName { get; set; }

        [BsonElement("id")]
        public string ID { get; set; }

        [BsonElement("release_state")]
        public string ReleaseState { get; set; }
    }

    public class LevelData
    {
        [BsonIgnoreIfNull]
        [BsonElement("mana_cost")]
        public int ManaCost { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("mana_multiplier")]
        public int ManaMultiplier { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("damage_effectiveness")]
        public int DamageEffectiveness { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("damage_multiplier")]
        public int DamageMultiplier { get; set; }

        [BsonElement("required_level")]
        public int RequiredLevel { get; set; }

        [BsonElement("stat_requirements")]
        public Dictionary<string, int> StatRequirements { get; set; }

        [BsonElement("stats")]
        public Dictionary<string, object>[] Stats { get; set; }
    }

    public class StaticData
    {
        [BsonIgnoreIfNull]
        [BsonElement("cooldown")]
        public int Cooldown { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("cooldown_bypass_type")]
        public string CooldownBypassType { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("mana_cost")]
        public int ManaCost { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("required_level")]
        public int RequiredLevel { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("mana_multiplier")]
        public int ManaMultiplier { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("mana_reservation_override")]
        public int ManaReservationOverride { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("attack_speed_multiplier")]
        public double AttackSpeedMultiplier { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("damage_effectiveness")]
        public int DamageEffectiveness { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("damage_multiplier")]
        public int DamageMultiplier { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("crit_chance")]
        public int CritChance { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("quality_stats")]
        public List<Dictionary<string, object>> QualityStats { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("stat_requirements")]
        public Dictionary<string, int> StatRequirements { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("stats")]
        public Dictionary<string, object>[] Stats { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("stored_uses")]
        public int StoredUses { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("vaal")]
        public Dictionary<string, int> VaalData { get; set; }
    }
} 