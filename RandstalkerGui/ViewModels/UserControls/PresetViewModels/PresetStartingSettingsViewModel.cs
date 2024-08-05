using CommunityToolkit.Mvvm.ComponentModel;
using RandstalkerGui.Models;
using RandstalkerGui.ViewModels.UserControls.SubPresets;

namespace RandstalkerGui.ViewModels.UserControls.PresetViewModels
{
    public class PresetStartingSettingsViewModel : ObservableObject
    {
        private Preset preset;

        public bool AutomaticStartingLife
        {
            get => preset.GameSettings.StartingLife == 0;
            set
            {
                if (value && preset.GameSettings.StartingLife != 0)
                {
                    if (SetProperty(preset.GameSettings.StartingLife, 0, preset.GameSettings, (gs, sl) => gs.StartingLife = sl))
                    {
                        OnPropertyChanged();
                        OnPropertyChanged(nameof(StartingLife));
                    }
                }
                else if (!value && preset.GameSettings.StartingLife == 0)
                {
                    if (SetProperty(preset.GameSettings.StartingLife, 4, preset.GameSettings, (gs, sl) => gs.StartingLife = sl))
                    {
                        OnPropertyChanged();
                        OnPropertyChanged(nameof(StartingLife));
                    }
                }
            }
        }

        public int StartingLife
        {
            get => preset.GameSettings.StartingLife;
            set => SetProperty(preset.GameSettings.StartingLife, value, preset.GameSettings, (gs, sl) => gs.StartingLife = sl);
        }

        public int StartingGold
        {
            get => preset.GameSettings.StartingGold;
            set => SetProperty(preset.GameSettings.StartingGold, value, preset.GameSettings, (gs, sg) => gs.StartingGold = sg);
        }

        public SpawnLocationsViewModel SpawnLocationsViewModel { get; set; }

        public ItemsCounterViewModel StartingsItemsViewModel { get; set; }

        public PresetStartingSettingsViewModel(Preset preset, ItemDefinitions itemDefinitions)
        {
            this.preset = preset;

            SpawnLocationsViewModel = new SpawnLocationsViewModel(preset.RandomizerSettings.SpawnLocations);
            StartingsItemsViewModel = new ItemsCounterViewModel(preset.GameSettings.StartingItems, itemDefinitions);
        }

        public void UpdateProperties()
        {
            OnPropertyChanged(nameof(AutomaticStartingLife));
            OnPropertyChanged(nameof(StartingLife));
            OnPropertyChanged(nameof(StartingGold));

            OnPropertyChanged(nameof(SpawnLocationsViewModel));
            OnPropertyChanged(nameof(StartingsItemsViewModel));
        }
    }
}
