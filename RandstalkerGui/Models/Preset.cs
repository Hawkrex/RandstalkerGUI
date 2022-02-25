using Newtonsoft.Json;
using System.Collections.Generic;

namespace RandstalkerGui.Models
{
    public class Preset
    {
        [JsonProperty("gameSettings")]
        public GameSettings GameSettings { get; set; }

        [JsonProperty("randomizerSettings")]
        public RandomizerSettings RandomizerSettings { get; set; }

        [JsonProperty("christmasEvent")]
        public bool ChristmasEvent { get; set; }
    }

    public class GameSettings
    {
        [JsonProperty("jewelCount")]
        public int JewelCount { get; set; }

        [JsonProperty("armorUpgrades")]
        public bool ArmorUpgrades { get; set; }

        [JsonProperty("startingGold")]
        public int StartingGold { get; set; }

        [JsonProperty("startingLife")]
        public int StartingLife { get; set; }

        [JsonProperty("startingItems")]
        public Dictionary<string, int> StartingItems { get; set; }

        [JsonProperty("fixArmletSkip")]
        public bool FixArmletSkip { get; set; }

        [JsonProperty("removeTreeCuttingGlitchDrops")]
        public bool RemoveTreeCuttingGlitchDrops { get; set; }

        [JsonProperty("consumableRecordBook")]
        public bool ConsumableRecordBook { get; set; }

        [JsonProperty("removeGumiBoulder")]
        public bool RemoveGumiBoulder { get; set; }

        [JsonProperty("removeTiborRequirement")]
        public bool RemoveTiborRequirement { get; set; }

        [JsonProperty("allTreesVisitedAtStart")]
        public bool AllTreesVisitedAtStart { get; set; }

        [JsonProperty("enemiesDamageFactor")]
        public int EnemiesDamageFactor { get; set; }

        [JsonProperty("enemiesHealthFactor")]
        public int EnemiesHealthFactor { get; set; }

        [JsonProperty("enemiesArmorFactor")]
        public int EnemiesArmorFactor { get; set; }

        [JsonProperty("enemiesGoldsFactor")]
        public int EnemiesGoldsFactor { get; set; }

        [JsonProperty("enemiesDropChanceFactor")]
        public int EnemiesDropChanceFactor { get; set; }

        [JsonProperty("healthGainedPerLifestock")]
        public int HealthGainedPerLifestock { get; set; }
    }

    public class RandomizerSettings
    {
        [JsonProperty("allowSpoilerLog")]
        public bool AllowSpoilerLog { get; set; }

        [JsonProperty("spawnLocations")]
        public List<string> SpawnLocations { get; set; }

        [JsonProperty("shuffleTrees")]
        public bool ShuffleTrees { get; set; }

        [JsonProperty("enemyJumpingInLogic")]
        public bool EnemyJumpingInLogic { get; set; }

        [JsonProperty("damageBoostingInLogic")]
        public bool DamageBoostingInLogic { get; set; }

        [JsonProperty("treeCuttingGlitchInLogic")]
        public bool TreeCuttingGlitchInLogic { get; set; }

        [JsonProperty("itemsDistribution")]
        public Dictionary<string, int> ItemsDistribution { get; set; }

        [JsonProperty("hintsDistribution")]
        public HintsDistribution HintsDistribution { get; set; }
    }

    public class HintsDistribution
    {
        [JsonProperty("regionRequirement")]
        public int RegionRequirement { get; set; }

        [JsonProperty("itemRequirement")]
        public int ItemRequirement { get; set; }

        [JsonProperty("itemLocation")]
        public int ItemLocation { get; set; }

        [JsonProperty("darkRegion")]
        public int DarkRegion { get; set; }

        [JsonProperty("joke")]
        public int Joke { get; set; }
    }
}
