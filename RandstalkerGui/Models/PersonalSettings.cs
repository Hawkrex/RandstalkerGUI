using Newtonsoft.Json;
using System.Collections.Generic;

namespace RandstalkerGui.Models
{
    public class PersonalSettings
    {
        [JsonProperty("inGameTracker")]
        public bool InGameTracker { get; set; }

        [JsonProperty("hudColor")]
        public string HudColor { get; set; }

        [JsonProperty("nigelColor")]
        public List<string> NigelColor { get; set; }

        [JsonProperty("removeMusic")]
        public bool RemoveMusic { get; set; }
    }
}
