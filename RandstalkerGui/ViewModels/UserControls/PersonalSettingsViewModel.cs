using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.UserControls.Tools;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

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

        public ColorPickerViewModel MainNigelColorPickerViewModel { get; set; }

        public ColorPickerViewModel SecondaryNigelColorPickerViewModel { get; set; }

        private BitmapImage nigelImage;
        public BitmapImage NigelImage
        {
            get
            {
                return nigelImage;
            }
            set
            {
                if (nigelImage != value)
                {
                    Log.Debug($"{nameof(nigelImage)} => <{nigelImage}> will change to <{value}>");
                    nigelImage = value;
                    OnPropertyChanged();
                }
            }
        }

        public Bitmap NigelSprite { get; set; }

        public RelayCommand SavePersonalSettings { get { return new RelayCommand(_ => SavePersonalSettingsHandler()); } }

        private void SavePersonalSettingsHandler()
        {
            Log.Debug($"{nameof(SavePersonalSettingsHandler)}() => Command requested ...");

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

            PersonalSettingsTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PersonalSettingsDirectoryPath, UserConfig.Instance.LastUsedPersonalSettingsFilePath);
            PersonalSettingsTreeViewModel.PropertyChanged += PersonalSettingsTreeViewModel_PropertyChanged;

            MainNigelColorPickerViewModel = new ColorPickerViewModel(personalSettings.NigelColor[0]);
            SecondaryNigelColorPickerViewModel = new ColorPickerViewModel(personalSettings.NigelColor[1]);

            MainNigelColorPickerViewModel.PropertyChanged += NigelColorPickerViewModel_PropertyChanged;
            SecondaryNigelColorPickerViewModel.PropertyChanged += NigelColorPickerViewModel_PropertyChanged;

            NigelSprite = new Bitmap("../../Resources/Images/Nigel.png");
            NigelImage = ToBitmapImage(NigelSprite);
            EditNigelColors();
        }

        private void PersonalSettingsTreeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PersonalSettingsTreeViewModel.SelectedFileRelativePath))
            {
                personalSettings = JsonConvert.DeserializeObject<PersonalSettings>(File.ReadAllText(Path.Combine(UserConfig.Instance.PersonalSettingsDirectoryPath, PersonalSettingsTreeViewModel.SelectedFileRelativePath)));

                UpdateProperties();
            }
        }

        private void NigelColorPickerViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            EditNigelColors();
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(RemoveMusic));
            OnPropertyChanged(nameof(SwapOverworldMusic));
            OnPropertyChanged(nameof(InGameTracker));
            OnPropertyChanged(nameof(HudColor));
            OnPropertyChanged(nameof(MainNigelColorPickerViewModel));
            OnPropertyChanged(nameof(SecondaryNigelColorPickerViewModel));
        }

        private void EditNigelColors()
        {
            Color mainColor = CodeToColor(MainNigelColorPickerViewModel.RedValue, MainNigelColorPickerViewModel.GreenValue, MainNigelColorPickerViewModel.BlueValue);
            Color secondColor = CodeToColor(SecondaryNigelColorPickerViewModel.RedValue, SecondaryNigelColorPickerViewModel.GreenValue, SecondaryNigelColorPickerViewModel.BlueValue);

            // Main color
            // Right arm
            NigelSprite.SetPixel(7, 15, mainColor);

            // Torso
            NigelSprite.SetPixel(9, 17, mainColor);
            NigelSprite.SetPixel(10, 17, mainColor);
            NigelSprite.SetPixel(11, 17, mainColor);
            NigelSprite.SetPixel(9, 18, mainColor);
            NigelSprite.SetPixel(10, 18, mainColor);

            // Belly
            NigelSprite.SetPixel(9, 25, mainColor);
            NigelSprite.SetPixel(10, 25, mainColor);
            NigelSprite.SetPixel(11, 25, mainColor);

            // Left arm
            NigelSprite.SetPixel(17, 16, mainColor);
            NigelSprite.SetPixel(18, 16, mainColor);
            NigelSprite.SetPixel(17, 17, mainColor);
            NigelSprite.SetPixel(19, 17, mainColor);
            NigelSprite.SetPixel(18, 18, mainColor);
            NigelSprite.SetPixel(19, 18, mainColor);

            // Secondary color
            // Right arm
            NigelSprite.SetPixel(7, 14, secondColor);
            NigelSprite.SetPixel(6, 15, secondColor);
            NigelSprite.SetPixel(6, 16, secondColor);
            NigelSprite.SetPixel(6, 17, secondColor);
            NigelSprite.SetPixel(7, 16, secondColor);

            // Torso
            NigelSprite.SetPixel(12, 17, secondColor);
            NigelSprite.SetPixel(11, 18, secondColor);
            NigelSprite.SetPixel(10, 19, secondColor);
            NigelSprite.SetPixel(9, 19, secondColor);
            NigelSprite.SetPixel(9, 20, secondColor);
            NigelSprite.SetPixel(9, 21, secondColor);

            // Belly
            NigelSprite.SetPixel(8, 24, secondColor);
            NigelSprite.SetPixel(9, 24, secondColor);
            NigelSprite.SetPixel(10, 24, secondColor);
            NigelSprite.SetPixel(12, 25, secondColor);
            NigelSprite.SetPixel(13, 24, secondColor);
            NigelSprite.SetPixel(14, 23, secondColor);

            // Left arm
            NigelSprite.SetPixel(18, 15, secondColor);
            NigelSprite.SetPixel(19, 16, secondColor);
            NigelSprite.SetPixel(20, 17, secondColor);
            NigelSprite.SetPixel(20, 18, secondColor);
            NigelSprite.SetPixel(20, 19, secondColor);
            NigelSprite.SetPixel(19, 19, secondColor);
            NigelSprite.SetPixel(18, 19, secondColor);
            NigelSprite.SetPixel(17, 19, secondColor);
            NigelSprite.SetPixel(17, 18, secondColor);
            NigelSprite.SetPixel(16, 18, secondColor);
            NigelSprite.SetPixel(16, 17, secondColor);
            NigelSprite.SetPixel(16, 16, secondColor);

            NigelImage = ToBitmapImage(NigelSprite);
        }

        private BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        private Color CodeToColor(char redCode, char greenCode, char blueCode)
        {
            return (Color)new ColorConverter().ConvertFromString($"#{redCode}{redCode}{greenCode}{greenCode}{blueCode}{blueCode}");
        }
    }
}
