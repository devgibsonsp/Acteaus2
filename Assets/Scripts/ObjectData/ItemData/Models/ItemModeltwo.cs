using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace ObjectData.ItemData.Models
{
    public partial class ItemModelTwo
    {
        [JsonProperty("Items")]
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Rarity")]
        public int Rarity { get; set; }

        [JsonProperty("Requirement")]
        public Requirement Requirement { get; set; }

        [JsonProperty("Properties")]
        public Properties Properties { get; set; }

        [JsonProperty("Value")]
        public int Value { get; set; }
    }

    public partial class Properties
    {
        [JsonProperty("Physical")]
        public int Physical { get; set; }

        [JsonProperty("Holy")]
        public int Holy { get; set; }

        [JsonProperty("Unholy")]
        public int Unholy { get; set; }

        [JsonProperty("Electric")]
        public int Electric { get; set; }

        [JsonProperty("Fire")]
        public int Fire { get; set; }

        [JsonProperty("Frost")]
        public int Frost { get; set; }

        [JsonProperty("Poision")]
        public int Poision { get; set; }

        [JsonProperty("All")]
        public int All { get; set; }

        [JsonProperty("HealthSteal")]
        public int HealthSteal { get; set; }

        [JsonProperty("MagicSteal")]
        public int MagicSteal { get; set; }

        [JsonProperty("HealthRegen")]
        public int HealthRegen { get; set; }

        [JsonProperty("MagicRegen")]
        public int MagicRegen { get; set; }

        [JsonProperty("Curse")]
        public int Curse { get; set; }

        [JsonProperty("Uncurse")]
        public int Uncurse { get; set; }

        [JsonProperty("Armor")]
        public int Armor { get; set; }
    }

    public partial class Requirement
    {
        [JsonProperty("Level")]
        public int Level { get; set; }

        [JsonProperty("Strength")]
        public int Strength { get; set; }

        [JsonProperty("Dexterity")]
        public int Dexterity { get; set; }

        [JsonProperty("Vitality")]
        public int Vitality { get; set; }

        [JsonProperty("Wisdom")]
        public int Wisdom { get; set; }

        [JsonProperty("Intellect")]
        public int Intellect { get; set; }
    }
}
