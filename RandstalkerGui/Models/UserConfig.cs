using Newtonsoft.Json;
using RandstalkerGui.Tools;
using System;
using System.IO;

namespace RandstalkerGui.Models
{
    public class UserConfig
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string UserConfigFilepath = "Resources/Datas/userConfig.json";

        public static event EventHandler<StatusBarMessageEventArgs> OnSavedValidUserConfig;

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

        public static UserConfig Instance => instance.Value;
        private static readonly Lazy<UserConfig> instance = new Lazy<UserConfig>(() =>
        {
            if (!File.Exists(UserConfigFilepath))
            {
                Log.Info("UserConfig file does not exists, creating empty one");
                return new UserConfig();
            }

            string userConfigRawText = string.Empty;
            try
            {
                userConfigRawText = File.ReadAllText(UserConfigFilepath);
            }
            catch (Exception ex)
            {
                Log.Error("Error occured while reading UserConfig file, creating empty one", ex);
                return new UserConfig();
            }

            if (string.IsNullOrEmpty(userConfigRawText))
            {
                Log.Warn("UserConfig file read but is empty, creating empty one");
                return new UserConfig();
            }

            var userConfig = JsonConvert.DeserializeObject<UserConfig>(userConfigRawText);
            if (userConfig == null)
            {
                Log.Error("Error occured while deserializing UserConfig raw text, creating empty one");
                return new UserConfig();
            }

            Log.Info("Successfully read UserConfig file");
            return userConfig;
        });

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

        public static void SaveFile()
        {
            File.WriteAllText(UserConfigFilepath, JsonConvert.SerializeObject(Instance));
        }
    }
}
