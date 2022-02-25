using Newtonsoft.Json;
using System;
using System.IO;

namespace RandstalkerGui.Models
{
    public class UserConfig
    {
        [JsonProperty("randstlakerExePath")]
        public string RandstlakerExePath { get; set; }

        [JsonProperty("presetsPath")]
        public string PresetsPath { get; set; }

        [JsonProperty("personalSettingsPath")]
        public string PersonalSettingsPath { get; set; }

        [JsonProperty("inputRomPath")]
        public string InputRomPath { get; set; }

        [JsonProperty("outputRomPath")]
        public string OutputRomPath { get; set; }

        private static readonly Lazy<UserConfig> _instance = new Lazy<UserConfig>(() => JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText("Resources/userConfig.json")));
        public static UserConfig Instance => _instance.Value;
    }
}
