using Newtonsoft.Json;
using System.Collections.Generic;

namespace RandstalkerGui.Models
{
    public class NigelEditablePixels
    {
        [JsonProperty("mainColorPixels")]
        public List<List<int>> MainColorPixels { get; set; }

        [JsonProperty("secondaryColorPixels")]
        public List<List<int>> SecondaryColorPixels { get; set; }
    }
}
