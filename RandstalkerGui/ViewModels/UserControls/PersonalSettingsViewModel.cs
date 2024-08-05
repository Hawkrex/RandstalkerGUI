using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.ViewModels.UserControls.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public partial class PersonalSettingsViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private PersonalSettings personalSettings;
        private readonly NigelEditablePixels nigelEditablePixels;

        public FileTreeViewModel PersonalSettingsTreeViewModel { get; set; }

        public bool RemoveMusic
        {
            get => personalSettings.RemoveMusic;
            set => SetProperty(personalSettings.RemoveMusic, value, personalSettings, (ps, rm) => ps.RemoveMusic = rm);
        }

        public bool SwapOverworldMusic
        {
            get => personalSettings.SwapOverworldMusic;
            set => SetProperty(personalSettings.SwapOverworldMusic, value, personalSettings, (ps, som) => ps.SwapOverworldMusic = som);
        }

        public bool InGameTracker
        {
            get => personalSettings.InGameTracker;
            set => SetProperty(personalSettings.InGameTracker, value, personalSettings, (ps, igt) => ps.InGameTracker = igt);
        }

        public ColorPickerViewModel HudColorPickerViewModel { get; set; }

        public ColorPickerViewModel MainNigelColorPickerViewModel { get; set; }

        public ColorPickerViewModel SecondaryNigelColorPickerViewModel { get; set; }

        [ObservableProperty]
        private Bitmap nigelSprite;

        [RelayCommand]
        private void SavePersonalSettings()
        {
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
                Log.Error(errorMessage, ex);
                MessageBox.Show(errorMessage, (string)App.Instance.TryFindResource("ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

            PersonalSettingsTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PersonalSettingsDirectoryPath, new Dictionary<string, string>() { { ".json", "json files (*.json)|*.json" } }, Resources.DefaultPersonalSettings, UserConfig.Instance.LastUsedPersonalSettingsFilePath);
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
            var mainColor = CodeToColor(MainNigelColorPickerViewModel.RedValue, MainNigelColorPickerViewModel.GreenValue, MainNigelColorPickerViewModel.BlueValue);
            foreach (var pixel in nigelEditablePixels.MainColorPixels)
            {
                NigelSprite.SetPixel(pixel[0], pixel[1], mainColor);
            }

            var secondaryColor = CodeToColor(SecondaryNigelColorPickerViewModel.RedValue, SecondaryNigelColorPickerViewModel.GreenValue, SecondaryNigelColorPickerViewModel.BlueValue);
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
