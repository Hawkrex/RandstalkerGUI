using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.UserControls.Tools;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class PersonalSettingsViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private PersonalSettings personalSettings;
        private NigelEditablePixels nigelEditablePixels;

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

        public ColorPickerViewModel HudColorPickerViewModel { get; set; }

        public ColorPickerViewModel MainNigelColorPickerViewModel { get; set; }

        public ColorPickerViewModel SecondaryNigelColorPickerViewModel { get; set; }

        private Bitmap nigelSprite;
        public Bitmap NigelSprite
        {
            get
            {
                return nigelSprite;
            }
            set
            {
                if (nigelSprite != value)
                {
                    Log.Debug($"{nameof(NigelSprite)} => <{NigelSprite}> will change to <{value}>");
                    nigelSprite = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SavePersonalSettings { get { return new RelayCommand(_ => SavePersonalSettingsHandler()); } }

        private void SavePersonalSettingsHandler()
        {
            Log.Debug($"{nameof(SavePersonalSettingsHandler)}() => Command requested ...");

            personalSettings.HudColor = HudColorPickerViewModel.FormatSettings();
            personalSettings.NigelColor[0] = MainNigelColorPickerViewModel.FormatSettings();
            personalSettings.NigelColor[1] = SecondaryNigelColorPickerViewModel.FormatSettings();

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

            nigelEditablePixels = JsonConvert.DeserializeObject<NigelEditablePixels>(Encoding.UTF8.GetString(Resources.NigelEditablePixels));

            PersonalSettingsTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PersonalSettingsDirectoryPath, UserConfig.Instance.LastUsedPersonalSettingsFilePath, Resources.DefaultPersonalSettings);
            PersonalSettingsTreeViewModel.PropertyChanged += PersonalSettingsTreeViewModel_PropertyChanged;

            NigelSprite = Resources.Nigel;
            CreateSubViewModels();
        }

        private void PersonalSettingsTreeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PersonalSettingsTreeViewModel.SelectedFileRelativePath))
            {
                personalSettings = JsonConvert.DeserializeObject<PersonalSettings>(File.ReadAllText(Path.Combine(UserConfig.Instance.PersonalSettingsDirectoryPath, PersonalSettingsTreeViewModel.SelectedFileRelativePath)));

                CreateSubViewModels();

                UpdateProperties();
            }
        }

        private void CreateSubViewModels()
        {
            HudColorPickerViewModel = new ColorPickerViewModel(personalSettings.HudColor);

            MainNigelColorPickerViewModel = new ColorPickerViewModel(personalSettings.NigelColor[0]);
            MainNigelColorPickerViewModel.PropertyChanged += NigelColorPickerViewModel_PropertyChanged;

            SecondaryNigelColorPickerViewModel = new ColorPickerViewModel(personalSettings.NigelColor[1]);
            SecondaryNigelColorPickerViewModel.PropertyChanged += NigelColorPickerViewModel_PropertyChanged;

            EditNigelColors();
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(RemoveMusic));
            OnPropertyChanged(nameof(SwapOverworldMusic));
            OnPropertyChanged(nameof(InGameTracker));
            OnPropertyChanged(nameof(HudColorPickerViewModel));
            OnPropertyChanged(nameof(MainNigelColorPickerViewModel));
            OnPropertyChanged(nameof(SecondaryNigelColorPickerViewModel));
        }

        private void NigelColorPickerViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            EditNigelColors();
        }

        private void EditNigelColors()
        {
            Color mainColor = CodeToColor(MainNigelColorPickerViewModel.RedValue, MainNigelColorPickerViewModel.GreenValue, MainNigelColorPickerViewModel.BlueValue);
            foreach (var pixel in nigelEditablePixels.MainColorPixels)
            {
                NigelSprite.SetPixel(pixel[0], pixel[1], mainColor);
            }

            Color secondaryColor = CodeToColor(SecondaryNigelColorPickerViewModel.RedValue, SecondaryNigelColorPickerViewModel.GreenValue, SecondaryNigelColorPickerViewModel.BlueValue);
            foreach (var pixel in nigelEditablePixels.SecondaryColorPixels)
            {
                NigelSprite.SetPixel(pixel[0], pixel[1], secondaryColor);
            }

            OnPropertyChanged(nameof(NigelSprite));
        }

        private Color CodeToColor(char redCode, char greenCode, char blueCode)
        {
            return (Color)new ColorConverter().ConvertFromString($"#{redCode}{redCode}{greenCode}{greenCode}{blueCode}{blueCode}");
        }
    }
}
