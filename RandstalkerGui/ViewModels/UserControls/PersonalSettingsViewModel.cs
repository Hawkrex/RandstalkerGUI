using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.Tools;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class PersonalSettingsViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private PersonalSettings personalSettings;

        public FileTreeViewModel PersonalSettingsTreeViewModel { get; set; }

        public bool RemoveMusic
        {
            get
            {
                return personalSettings.RemoveMusic;
            }
            set
            {
                if (personalSettings.RemoveMusic != value)
                {
                    Log.Debug($"{nameof(personalSettings.RemoveMusic)} => <{personalSettings.RemoveMusic}> will change to <{value}>");
                    personalSettings.RemoveMusic = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool SwapOverworldMusic
        {
            get
            {
                return personalSettings.SwapOverworldMusic;
            }
            set
            {
                if (personalSettings.SwapOverworldMusic != value)
                {
                    Log.Debug($"{nameof(personalSettings.SwapOverworldMusic)} => <{personalSettings.SwapOverworldMusic}> will change to <{value}>");
                    personalSettings.SwapOverworldMusic = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool InGameTracker
        {
            get
            {
                return personalSettings.InGameTracker;
            }
            set
            {
                if (personalSettings.InGameTracker != value)
                {
                    Log.Debug($"{nameof(personalSettings.InGameTracker)} => <{personalSettings.InGameTracker}> will change to <{value}>");
                    personalSettings.InGameTracker = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HudColor
        {
            get
            {
                return personalSettings.HudColor;
            }
            set
            {
                if (personalSettings.HudColor != value)
                {
                    Log.Debug($"{nameof(personalSettings.HudColor)} => <{personalSettings.HudColor}> will change to <{value}>");
                    personalSettings.HudColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MainNigelColor
        {
            get
            {
                return personalSettings.NigelColor[0];
            }
            set
            {
                if (personalSettings.NigelColor[0] != value)
                {
                    Log.Debug($"{nameof(MainNigelColor)} => <{personalSettings.NigelColor[0]}> will change to <{value}>");
                    personalSettings.NigelColor[0] = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecondaryNigelColor
        {
            get
            {
                return personalSettings.NigelColor[1];
            }
            set
            {
                if (personalSettings.NigelColor[1] != value)
                {
                    Log.Debug($"{nameof(SecondaryNigelColor)} => <{personalSettings.NigelColor[1]}> will change to <{value}>");
                    personalSettings.NigelColor[1] = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SavePersonalSettings { get { return new RelayCommand(_ => SavePersonalSettingsHandler()); } }

        private void SavePersonalSettingsHandler()
        {
            Log.Debug($"{nameof(SavePersonalSettingsHandler)}() => Command requested ...");

            try
            {
                File.WriteAllText(Path.Combine(UserConfig.Instance.PersonalSettingsDirectoryPath, PersonalSettingsTreeViewModel.SelectedFileRelativePath), JsonConvert.SerializeObject(personalSettings));
                MessageBox.Show((string)App.Instance.TryFindResource("FileWriteSuccessMessage"), (string)App.Instance.TryFindResource("SuccessTitle"), MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                string errorMessage = (string)App.Instance.TryFindResource("FileWriteErrorMessage");
                MessageBox.Show(errorMessage, (string)App.Instance.TryFindResource("ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                Log.Error(errorMessage + " : " + ex);
            }

            Log.Debug($"{nameof(SavePersonalSettingsHandler)}() => Command executed");
        }

        public PersonalSettingsViewModel()
        {
            if (File.Exists(Path.Combine(UserConfig.Instance.PersonalSettingsDirectoryPath, UserConfig.Instance.LastUsedPersonalSettingsFilePath)))
            {
                personalSettings = JsonConvert.DeserializeObject<PersonalSettings>(File.ReadAllText(Path.Combine(UserConfig.Instance.PersonalSettingsDirectoryPath, UserConfig.Instance.LastUsedPersonalSettingsFilePath)));
            }
            else
            {
                personalSettings = JsonConvert.DeserializeObject<PersonalSettings>(Encoding.UTF8.GetString(Resources.DefaultPersonalSettings));
            }

            PersonalSettingsTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PersonalSettingsDirectoryPath, UserConfig.Instance.LastUsedPersonalSettingsFilePath);
            PersonalSettingsTreeViewModel.PropertyChanged += PersonalSettingsTreeViewModel_PropertyChanged;
        }

        private void PersonalSettingsTreeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PersonalSettingsTreeViewModel.SelectedFileRelativePath))
            {
                personalSettings = JsonConvert.DeserializeObject<PersonalSettings>(File.ReadAllText(Path.Combine(UserConfig.Instance.PersonalSettingsDirectoryPath, PersonalSettingsTreeViewModel.SelectedFileRelativePath)));

                UpdateProperties();
            }
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(RemoveMusic));
            OnPropertyChanged(nameof(SwapOverworldMusic));
            OnPropertyChanged(nameof(InGameTracker));
            OnPropertyChanged(nameof(HudColor));
            OnPropertyChanged(nameof(MainNigelColor));
            OnPropertyChanged(nameof(SecondaryNigelColor));
        }
    }
}
