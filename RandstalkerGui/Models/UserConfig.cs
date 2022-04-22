using Newtonsoft.Json;
using System;
using System.IO;

namespace RandstalkerGui.Models
{
    public class UserConfig
    {
        [JsonProperty("randstlakerExeDirectoryPath")]
        public string RandstlakerExeDirectoryPath { get; set; }

        [JsonProperty("presetsDirectoryPath")]
        public string PresetsDirectoryPath { get; set; }

        [JsonProperty("lastUsedPresetFilePath")]
        public string LastUsedPresetFilePath { get; set; }

        [JsonProperty("personalSettingsDirectoryPath")]
        public string PersonalSettingsDirectoryPath { get; set; }

        [JsonProperty("lastUsedPersonalSettingsFilePath")]
        public string LastUsedPersonalSettingsFilePath { get; set; }

        [JsonProperty("inputRomFilePath")]
        public string InputRomFilePath { get; set; }

        [JsonProperty("outputRomDirectoryPath")]
        public string OutputRomDirectoryPath { get; set; }


        private static readonly Lazy<UserConfig> instance = new Lazy<UserConfig>(() => JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText("Resources/userConfig.json")));

        public static UserConfig Instance => instance.Value;


        public bool ArePathsValid()
        {
            if(!File.Exists(RandstlakerExeDirectoryPath + "/randstalker.exe"))
                return false;

            if (string.IsNullOrWhiteSpace(PresetsDirectoryPath))
                return false;

            if (string.IsNullOrWhiteSpace(PersonalSettingsDirectoryPath))
                return false;

            if (!File.Exists(InputRomFilePath))
                return false;

            if (string.IsNullOrWhiteSpace(OutputRomDirectoryPath))
                return false;

            return true;
        }
    }
}
