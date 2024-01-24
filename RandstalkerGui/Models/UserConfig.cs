using Newtonsoft.Json;
using RandstalkerGui.Tools;
using System;
using System.IO;

namespace RandstalkerGui.Models
{
    public class UserConfig
    {
        [JsonProperty("randstlakerExeFilePath")]
        public string RandstlakerExeFilePath { get; set; }

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

        public static event EventHandler<StatusBarMessageEventArgs> OnSavedValidUserConfig;

        private static readonly Lazy<UserConfig> instance = new Lazy<UserConfig>(() => JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText("Resources/userConfig.json")));

        public static UserConfig Instance => instance.Value;

        public string CheckParametersValidity()
        {
            string parametersInvalid = string.Empty;

            if (!File.Exists(RandstlakerExeFilePath))
            {
                parametersInvalid += (string)App.Instance.TryFindResource("RandstlakerExeNotFound") + Environment.NewLine;
            }

            if (!File.Exists(InputRomFilePath))
            {
                parametersInvalid += (string)App.Instance.TryFindResource("InputRomNotFound") + Environment.NewLine;
            }

            parametersInvalid = parametersInvalid.Trim();

            return parametersInvalid;
        }

        public static void NotifySavedValidUserConfig()
        {
            OnSavedValidUserConfig?.Invoke(Instance, new StatusBarMessageEventArgs() { Message = Instance.CheckParametersValidity(), Sender = "UserConfig" });
        }
    }
}
