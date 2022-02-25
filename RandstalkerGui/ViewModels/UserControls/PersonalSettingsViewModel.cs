using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System.IO;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class PersonalSettingsViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private PersonalSettings _personalSettings;

        public bool InGameTracker
        {
            get
            {
                return _personalSettings.InGameTracker;
            }
            set
            {
                if (_personalSettings.InGameTracker != value)
                {
                    Log.Debug($"{nameof(_personalSettings.InGameTracker)} => <{_personalSettings.InGameTracker}> will change to <{value}>");
                    _personalSettings.InGameTracker = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HudColor
        {
            get
            {
                return _personalSettings.HudColor;
            }
            set
            {
                if (_personalSettings.HudColor != value)
                {
                    Log.Debug($"{nameof(_personalSettings.HudColor)} => <{_personalSettings.HudColor}> will change to <{value}>");
                    _personalSettings.HudColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MainNigelColor
        {
            get
            {
                return _personalSettings.NigelColor[0];
            }
            set
            {
                if (_personalSettings.NigelColor[0] != value)
                {
                    Log.Debug($"{nameof(MainNigelColor)} => <{_personalSettings.NigelColor[0]}> will change to <{value}>");
                    _personalSettings.NigelColor[0] = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryNigelColor
        {
            get
            {
                return _personalSettings.NigelColor[1];
            }
            set
            {
                if (_personalSettings.NigelColor[1] != value)
                {
                    Log.Debug($"{nameof(SecondaryNigelColor)} => <{_personalSettings.NigelColor[1]}> will change to <{value}>");
                    _personalSettings.NigelColor[1] = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SavePersonalSettings { get { return new RelayCommand(_ => SavePersonalSettingsHandler()); } }
        private void SavePersonalSettingsHandler()
        {
            Log.Debug($"{nameof(SavePersonalSettingsHandler)}() => Command requested ...");

            File.WriteAllText(UserConfig.Instance.PersonalSettingsPath + "personal_settings.json", JsonConvert.SerializeObject(_personalSettings));

            Log.Debug($"{nameof(SavePersonalSettingsHandler)}() => Command executed");
        }

        public PersonalSettingsViewModel()
        {
            _personalSettings = JsonConvert.DeserializeObject<PersonalSettings>(File.ReadAllText(UserConfig.Instance.PersonalSettingsPath + "personal_settings.json"));
        }
    }
}
