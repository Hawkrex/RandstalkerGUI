using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.UserControls.PresetViewModels;
using RandstalkerGui.ViewModels.UserControls.SubPresets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public partial class PresetViewModel : ValidationViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Preset preset;
        private readonly ItemDefinitions itemDefinitions;

        public FileTreeViewModel PresetTreeViewModel { get; set; }

        public PresetStartingSettingsViewModel PresetStartingSettingsViewModel { get; set; }
        public PresetLogicSettingsViewModel PresetLogicSettingsViewModel { get; set; }
        public PresetQolSettingsViewModel PresetQolSettingsViewModel { get; set; }

        [RelayCommand]
        private void SavePreset()
        {
            preset.RandomizerSettings.SpawnLocations = PresetStartingSettingsViewModel.SpawnLocationsViewModel.FormatSettings();
            preset.GameSettings.StartingItems = PresetStartingSettingsViewModel.StartingsItemsViewModel.FormatSettings();

            preset.RandomizerSettings.ItemsDistribution = PresetLogicSettingsViewModel.ItemsDistributionViewModel.FormatSettings();
            preset.GameSettings.FiniteGroundItems = PresetLogicSettingsViewModel.FiniteGroundItemsViewModel.FormatSettings();
            preset.GameSettings.FiniteShopItems = PresetLogicSettingsViewModel.FiniteShopItemsViewModel.FormatSettings();

            try
            {
                File.WriteAllText(Path.Combine(UserConfig.Instance.PresetsDirectoryPath, PresetTreeViewModel.SelectedFileRelativePath), JsonConvert.SerializeObject(preset));

                MessageBox.Show((string)App.Instance.TryFindResource("FileWriteSuccessMessage"), (string)App.Instance.TryFindResource("SuccessTitle"), MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                string errorMessage = (string)App.Instance.TryFindResource("FileWriteErrorMessage");
                Log.Error(errorMessage, ex);
                MessageBox.Show(errorMessage, (string)App.Instance.TryFindResource("ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private EventHandler<StatusBarMessageEventArgs> setStatusBarMessage;

        public PresetViewModel(EventHandler<StatusBarMessageEventArgs> setStatusBarMessage)
        {
            preset = File.Exists(Path.Combine(UserConfig.Instance.PresetsDirectoryPath, UserConfig.Instance.LastUsedPresetFilePath))
                ? JsonConvert.DeserializeObject<Preset>(File.ReadAllText(Path.Combine(UserConfig.Instance.PresetsDirectoryPath, UserConfig.Instance.LastUsedPresetFilePath)))
                : JsonConvert.DeserializeObject<Preset>(Encoding.UTF8.GetString(Resources.DefaultPreset));

            this.setStatusBarMessage = setStatusBarMessage;

            itemDefinitions = new ItemDefinitions();

            PresetTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PresetsDirectoryPath, new Dictionary<string, string>() { { ".json", "json files (*.json)|*.json" } }, Resources.DefaultPreset, UserConfig.Instance.LastUsedPresetFilePath);
            PresetTreeViewModel.PropertyChanged += PresetTreeViewModel_PropertyChanged;

            PresetStartingSettingsViewModel = new PresetStartingSettingsViewModel(preset, itemDefinitions);
            PresetLogicSettingsViewModel = new PresetLogicSettingsViewModel(preset, itemDefinitions, setStatusBarMessage);
            PresetQolSettingsViewModel = new PresetQolSettingsViewModel(preset);
            PresetQolSettingsViewModel.OnError += setStatusBarMessage;
        }

        private void PresetTreeViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(PresetTreeViewModel.SelectedFileRelativePath))
            {
                return;
            }

            preset = JsonConvert.DeserializeObject<Preset>(File.ReadAllText(Path.Combine(UserConfig.Instance.PresetsDirectoryPath, PresetTreeViewModel.SelectedFileRelativePath)));

            PresetStartingSettingsViewModel.SpawnLocationsViewModel = new SpawnLocationsViewModel(preset.RandomizerSettings.SpawnLocations);
            PresetStartingSettingsViewModel.StartingsItemsViewModel = new ItemsCounterViewModel(preset.GameSettings.StartingItems, itemDefinitions);

            PresetLogicSettingsViewModel.ItemsDistributionViewModel = new ItemsCounterViewModel(preset.RandomizerSettings.ItemsDistribution, itemDefinitions, setStatusBarMessage);

            UpdateProperties();
        }

        private void UpdateProperties()
        {
            PresetStartingSettingsViewModel.UpdateProperties();
            PresetLogicSettingsViewModel.UpdateProperties();
            PresetQolSettingsViewModel.UpdateProperties();
        }
    }
}
