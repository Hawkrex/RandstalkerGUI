using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RandstalkerGui.Models
{
    public class ItemDefinitions
    {
        public List<ItemDefinition> Items { get; set; }

        public ItemDefinitions()
        {
            Items = new List<ItemDefinition>();

            foreach (var item in JsonConvert.DeserializeObject<JObject>(Properties.Resources.Items))
            {
                var obj = JsonConvert.DeserializeObject<ItemDefinition>(item.Value.ToString());
                obj.Id = int.Parse(item.Key);

                Items.Add(obj);
            }
        }
    }

    public class ItemDefinition
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("maxQuantity")]
        public int MaxQuantity { get; set; }

        [JsonProperty("startingQuantity")]
        public int StartingQuantity { get; set; }

        [JsonProperty("goldValue")]
        public int GoldValue { get; set; }
    }
}
