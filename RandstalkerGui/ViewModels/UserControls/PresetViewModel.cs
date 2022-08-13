using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.UserControls.SubPresets;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class PresetViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Preset preset;

        private ItemDefinitions itemDefinitions;

        public FileTreeViewModel PresetTreeViewModel { get; set; }

        public bool ChristmasEvent
        {
            get
            {
                return preset.ChristmasEvent;
            }
            set
            {
                if (preset.ChristmasEvent != value)
                {
                    Log.Debug($"{nameof(preset.ChristmasEvent)} => <{preset.ChristmasEvent}> will change to <{value}>");
                    preset.ChristmasEvent = value;
                    OnPropertyChanged();
                }
            }
        }

        public int JewelCount
        {
            get
            {
                return preset.GameSettings.JewelCount;
            }
            set
            {
                if (preset.GameSettings.JewelCount != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.JewelCount)} => <{preset.GameSettings.JewelCount}> will change to <{value}>");
                    preset.GameSettings.JewelCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ArmorUpgrades
        {
            get
            {
                return preset.GameSettings.ArmorUpgrades;
            }
            set
            {
                if (preset.GameSettings.ArmorUpgrades != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.ArmorUpgrades)} => <{preset.GameSettings.ArmorUpgrades}> will change to <{value}>");
                    preset.GameSettings.ArmorUpgrades = value;
                    OnPropertyChanged();
                }
            }
        }

        public int StartingGold
        {
            get
            {
                return preset.GameSettings.StartingGold;
            }
            set
            {
                if (preset.GameSettings.StartingGold != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.StartingGold)} => <{preset.GameSettings.StartingGold}> will change to <{value}>");
                    preset.GameSettings.StartingGold = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AutomaticStartingLife
        {
            get
            {
                return preset.GameSettings.StartingLife == 0;
            }
            set
            {
                if (value && preset.GameSettings.StartingLife != 0)
                {
                    Log.Debug($"{nameof(preset.GameSettings.StartingLife)} => <{preset.GameSettings.StartingLife}> will change to 0");

                    preset.GameSettings.StartingLife = 0;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StartingLife));
                }
                else if (!value && preset.GameSettings.StartingLife == 0)
                {
                    Log.Debug($"{nameof(preset.GameSettings.StartingLife)} => <{preset.GameSettings.StartingLife}> will change to 0");

                    preset.GameSettings.StartingLife = 4;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StartingLife));
                }
            }
        }

        public int StartingLife
        {
            get
            {
                return preset.GameSettings.StartingLife;
            }
            set
            {
                if (preset.GameSettings.StartingLife != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.StartingLife)} => <{preset.GameSettings.StartingLife}> will change to <{value}>");
                    preset.GameSettings.StartingLife = value;
                    OnPropertyChanged();
                }
            }
        }

        public ItemsCounterViewModel StartingsItemsViewModel { get; set; }

        public bool FixArmletSkip
        {
            get
            {
                return preset.GameSettings.FixArmletSkip;
            }
            set
            {
                if (preset.GameSettings.FixArmletSkip != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.FixArmletSkip)} => <{preset.GameSettings.FixArmletSkip}> will change to <{value}>");
                    preset.GameSettings.FixArmletSkip = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RemoveTreeCuttingGlitchDrops
        {
            get
            {
                return preset.GameSettings.RemoveTreeCuttingGlitchDrops;
            }
            set
            {
                if (preset.GameSettings.RemoveTreeCuttingGlitchDrops != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.RemoveTreeCuttingGlitchDrops)} => <{preset.GameSettings.RemoveTreeCuttingGlitchDrops}> will change to <{value}>");
                    preset.GameSettings.RemoveTreeCuttingGlitchDrops = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ConsumableRecordBook
        {
            get
            {
                return preset.GameSettings.ConsumableRecordBook;
            }
            set
            {
                if (preset.GameSettings.ConsumableRecordBook != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.ConsumableRecordBook)} => <{preset.GameSettings.ConsumableRecordBook}> will change to <{value}>");
                    preset.GameSettings.ConsumableRecordBook = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ConsumableSpellBook
        {
            get
            {
                return preset.GameSettings.ConsumableSpellBook;
            }
            set
            {
                if (preset.GameSettings.ConsumableSpellBook != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.ConsumableSpellBook)} => <{preset.GameSettings.ConsumableSpellBook}> will change to <{value}>");
                    preset.GameSettings.ConsumableSpellBook = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RemoveGumiBoulder
        {
            get
            {
                return preset.GameSettings.RemoveGumiBoulder;
            }
            set
            {
                if (preset.GameSettings.RemoveGumiBoulder != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.RemoveGumiBoulder)} => <{preset.GameSettings.RemoveGumiBoulder}> will change to <{value}>");
                    preset.GameSettings.RemoveGumiBoulder = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RemoveTiborRequirement
        {
            get
            {
                return preset.GameSettings.RemoveTiborRequirement;
            }
            set
            {
                if (preset.GameSettings.RemoveTiborRequirement != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.RemoveTiborRequirement)} => <{preset.GameSettings.RemoveTiborRequirement}> will change to <{value}>");
                    preset.GameSettings.RemoveTiborRequirement = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AllTreesVisitedAtStart
        {
            get
            {
                return preset.GameSettings.AllTreesVisitedAtStart;
            }
            set
            {
                if (preset.GameSettings.AllTreesVisitedAtStart != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.AllTreesVisitedAtStart)} => <{preset.GameSettings.AllTreesVisitedAtStart}> will change to <{value}>");
                    preset.GameSettings.AllTreesVisitedAtStart = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesDamageFactor
        {
            get
            {
                return preset.GameSettings.EnemiesDamageFactor;
            }
            set
            {
                if (preset.GameSettings.EnemiesDamageFactor != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.EnemiesDamageFactor)} => <{preset.GameSettings.EnemiesDamageFactor}> will change to <{value}>");
                    preset.GameSettings.EnemiesDamageFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesHealthFactor
        {
            get
            {
                return preset.GameSettings.EnemiesHealthFactor;
            }
            set
            {
                if (preset.GameSettings.EnemiesHealthFactor != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.EnemiesHealthFactor)} => <{preset.GameSettings.EnemiesHealthFactor}> will change to <{value}>");
                    preset.GameSettings.EnemiesHealthFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesArmorFactor
        {
            get
            {
                return preset.GameSettings.EnemiesArmorFactor;
            }
            set
            {
                if (preset.GameSettings.EnemiesArmorFactor != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.EnemiesArmorFactor)} => <{preset.GameSettings.EnemiesArmorFactor}> will change to <{value}>");
                    preset.GameSettings.EnemiesArmorFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesGoldsFactor
        {
            get
            {
                return preset.GameSettings.EnemiesGoldsFactor;
            }
            set
            {
                if (preset.GameSettings.EnemiesGoldsFactor != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.EnemiesGoldsFactor)} => <{preset.GameSettings.EnemiesGoldsFactor}> will change to <{value}>");
                    preset.GameSettings.EnemiesGoldsFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesDropChanceFactor
        {
            get
            {
                return preset.GameSettings.EnemiesDropChanceFactor;
            }
            set
            {
                if (preset.GameSettings.EnemiesDropChanceFactor != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.EnemiesDropChanceFactor)} => <{preset.GameSettings.EnemiesDropChanceFactor}> will change to <{value}>");
                    preset.GameSettings.EnemiesDropChanceFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int HealthGainedPerLifestock
        {
            get
            {
                return preset.GameSettings.HealthGainedPerLifestock;
            }
            set
            {
                if (preset.GameSettings.HealthGainedPerLifestock != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.HealthGainedPerLifestock)} => <{preset.GameSettings.HealthGainedPerLifestock}> will change to <{value}>");
                    preset.GameSettings.HealthGainedPerLifestock = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool FastMenuTransitions
        {
            get
            {
                return preset.GameSettings.FastMenuTransitions;
            }
            set
            {
                if (preset.GameSettings.FastMenuTransitions != value)
                {
                    Log.Debug($"{nameof(preset.GameSettings.FastMenuTransitions)} => <{preset.GameSettings.FastMenuTransitions}> will change to <{value}>");
                    preset.GameSettings.FastMenuTransitions = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AllowSpoilerLog
        {
            get
            {
                return preset.RandomizerSettings.AllowSpoilerLog;
            }
            set
            {
                if (preset.RandomizerSettings.AllowSpoilerLog != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.AllowSpoilerLog)} => <{preset.RandomizerSettings.AllowSpoilerLog}> will change to <{value}>");
                    preset.RandomizerSettings.AllowSpoilerLog = value;
                    OnPropertyChanged();
                }
            }
        }

        public SpawnLocationsViewModel SpawnLocationsViewModel { get; set; }

        public bool ShuffleTrees
        {
            get
            {
                return preset.RandomizerSettings.ShuffleTrees;
            }
            set
            {
                if (preset.RandomizerSettings.ShuffleTrees != value)

                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.ShuffleTrees)} => <{preset.RandomizerSettings.ShuffleTrees}> will change to <{value}>");
                    preset.RandomizerSettings.ShuffleTrees = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool EnemyJumpingInLogic
        {
            get
            {
                return preset.RandomizerSettings.EnemyJumpingInLogic;
            }
            set
            {
                if (preset.RandomizerSettings.EnemyJumpingInLogic != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.EnemyJumpingInLogic)} => <{preset.RandomizerSettings.EnemyJumpingInLogic}> will change to <{value}>");
                    preset.RandomizerSettings.EnemyJumpingInLogic = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool DamageBoostingInLogic
        {
            get
            {
                return preset.RandomizerSettings.DamageBoostingInLogic;
            }
            set
            {
                if (preset.RandomizerSettings.DamageBoostingInLogic != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.DamageBoostingInLogic)} => <{preset.RandomizerSettings.DamageBoostingInLogic}> will change to <{value}>");
                    preset.RandomizerSettings.DamageBoostingInLogic = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool TreeCuttingGlitchInLogic
        {
            get
            {
                return preset.RandomizerSettings.TreeCuttingGlitchInLogic;
            }
            set
            {
                if (preset.RandomizerSettings.TreeCuttingGlitchInLogic != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.TreeCuttingGlitchInLogic)} => <{preset.RandomizerSettings.TreeCuttingGlitchInLogic}> will change to <{value}>");
                    preset.RandomizerSettings.TreeCuttingGlitchInLogic = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AllowWhistleUsageBehindTrees
        {
            get
            {
                return preset.RandomizerSettings.AllowWhistleUsageBehindTrees;
            }
            set
            {
                if (preset.RandomizerSettings.AllowWhistleUsageBehindTrees != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.AllowWhistleUsageBehindTrees)} => <{preset.RandomizerSettings.AllowWhistleUsageBehindTrees}> will change to <{value}>");
                    preset.RandomizerSettings.AllowWhistleUsageBehindTrees = value;
                    OnPropertyChanged();
                }
            }
        }

        public ItemsCounterViewModel ItemsDistributionViewModel { get; set; }

        public int RegionRequirement
        {
            get
            {
                return preset.RandomizerSettings.HintsDistribution.RegionRequirement;
            }
            set
            {
                if (preset.RandomizerSettings.HintsDistribution.RegionRequirement != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.HintsDistribution.RegionRequirement)} => <{preset.RandomizerSettings.HintsDistribution.RegionRequirement}> will change to <{value}>");
                    preset.RandomizerSettings.HintsDistribution.RegionRequirement = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ItemRequirement
        {
            get
            {
                return preset.RandomizerSettings.HintsDistribution.ItemRequirement;
            }
            set
            {
                if (preset.RandomizerSettings.HintsDistribution.ItemRequirement != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.HintsDistribution.ItemRequirement)} => <{preset.RandomizerSettings.HintsDistribution.ItemRequirement}> will change to <{value}>");
                    preset.RandomizerSettings.HintsDistribution.ItemRequirement = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ItemLocation
        {
            get
            {
                return preset.RandomizerSettings.HintsDistribution.ItemLocation;
            }
            set
            {
                if (preset.RandomizerSettings.HintsDistribution.ItemLocation != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.HintsDistribution.ItemLocation)} => <{preset.RandomizerSettings.HintsDistribution.ItemLocation}> will change to <{value}>");
                    preset.RandomizerSettings.HintsDistribution.ItemLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        public int DarkRegion
        {
            get
            {
                return preset.RandomizerSettings.HintsDistribution.DarkRegion;
            }
            set
            {
                if (preset.RandomizerSettings.HintsDistribution.DarkRegion != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.HintsDistribution.DarkRegion)} => <{preset.RandomizerSettings.HintsDistribution.DarkRegion}> will change to <{value}>");
                    preset.RandomizerSettings.HintsDistribution.DarkRegion = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Joke
        {
            get
            {
                return preset.RandomizerSettings.HintsDistribution.Joke;
            }
            set
            {
                if (preset.RandomizerSettings.HintsDistribution.Joke != value)
                {
                    Log.Debug($"{nameof(preset.RandomizerSettings.HintsDistribution.Joke)} => <{preset.RandomizerSettings.HintsDistribution.Joke}> will change to <{value}>");
                    preset.RandomizerSettings.HintsDistribution.Joke = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SavePreset { get { return new RelayCommand(_ => SavePresetHandler()); } }

        private void SavePresetHandler()
        {
            Log.Debug($"{nameof(SavePresetHandler)}() => Command requested ...");

            preset.GameSettings.StartingItems = StartingsItemsViewModel.FormatSettings();
            preset.RandomizerSettings.SpawnLocations = SpawnLocationsViewModel.FormatSettings();
            preset.RandomizerSettings.ItemsDistribution = ItemsDistributionViewModel.FormatSettings();

            try
            {
                File.WriteAllText(Path.Combine(UserConfig.Instance.PresetsDirectoryPath, PresetTreeViewModel.SelectedFileRelativePath), JsonConvert.SerializeObject(preset));

                MessageBox.Show((string)App.Instance.TryFindResource("FileWriteSuccessMessage"), (string)App.Instance.TryFindResource("SuccessTitle"), MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                string errorMessage = (string)App.Instance.TryFindResource("FileWriteErrorMessage");
                MessageBox.Show(errorMessage, (string)App.Instance.TryFindResource("ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                Log.Error(errorMessage + " : " + ex);
            }

            Log.Debug($"{nameof(SavePresetHandler)}() => Command executed");
        }

        public PresetViewModel()
        {
            if (File.Exists(Path.Combine(UserConfig.Instance.PresetsDirectoryPath, UserConfig.Instance.LastUsedPresetFilePath)))
            {
                preset = JsonConvert.DeserializeObject<Preset>(File.ReadAllText(Path.Combine(UserConfig.Instance.PresetsDirectoryPath, UserConfig.Instance.LastUsedPresetFilePath)));
            }
            else
            {
                preset = JsonConvert.DeserializeObject<Preset>(Encoding.UTF8.GetString(Resources.DefaultPreset));
            }

            itemDefinitions = new ItemDefinitions();

            PresetTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PresetsDirectoryPath, UserConfig.Instance.LastUsedPresetFilePath, Resources.DefaultPreset);
            PresetTreeViewModel.PropertyChanged += PresetTreeViewModel_PropertyChanged;

            StartingsItemsViewModel = new ItemsCounterViewModel(preset.GameSettings.StartingItems, itemDefinitions);
            SpawnLocationsViewModel = new SpawnLocationsViewModel(preset.RandomizerSettings.SpawnLocations);
            ItemsDistributionViewModel = new ItemsCounterViewModel(preset.RandomizerSettings.ItemsDistribution, itemDefinitions);
        }

        private void PresetTreeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(PresetTreeViewModel.SelectedFileRelativePath))
            {
                preset = JsonConvert.DeserializeObject<Preset>(File.ReadAllText(Path.Combine(UserConfig.Instance.PresetsDirectoryPath, PresetTreeViewModel.SelectedFileRelativePath)));

                StartingsItemsViewModel = new ItemsCounterViewModel(preset.GameSettings.StartingItems, itemDefinitions);
                SpawnLocationsViewModel = new SpawnLocationsViewModel(preset.RandomizerSettings.SpawnLocations);
                ItemsDistributionViewModel = new ItemsCounterViewModel(preset.RandomizerSettings.ItemsDistribution, itemDefinitions);

                UpdateProperties();
            }
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(ChristmasEvent));
            OnPropertyChanged(nameof(JewelCount));
            OnPropertyChanged(nameof(ArmorUpgrades));
            OnPropertyChanged(nameof(StartingGold));
            OnPropertyChanged(nameof(AutomaticStartingLife));
            OnPropertyChanged(nameof(StartingLife));
            OnPropertyChanged(nameof(StartingsItemsViewModel));
            OnPropertyChanged(nameof(FixArmletSkip));
            OnPropertyChanged(nameof(RemoveTreeCuttingGlitchDrops));
            OnPropertyChanged(nameof(ConsumableRecordBook));
            OnPropertyChanged(nameof(ConsumableSpellBook));
            OnPropertyChanged(nameof(RemoveGumiBoulder));
            OnPropertyChanged(nameof(RemoveTiborRequirement));
            OnPropertyChanged(nameof(AllTreesVisitedAtStart));
            OnPropertyChanged(nameof(EnemiesDamageFactor));
            OnPropertyChanged(nameof(EnemiesHealthFactor));
            OnPropertyChanged(nameof(EnemiesArmorFactor));
            OnPropertyChanged(nameof(EnemiesGoldsFactor));
            OnPropertyChanged(nameof(EnemiesDropChanceFactor));
            OnPropertyChanged(nameof(HealthGainedPerLifestock));
            OnPropertyChanged(nameof(FastMenuTransitions));
            OnPropertyChanged(nameof(AllowSpoilerLog));
            OnPropertyChanged(nameof(SpawnLocationsViewModel));
            OnPropertyChanged(nameof(ShuffleTrees));
            OnPropertyChanged(nameof(EnemyJumpingInLogic));
            OnPropertyChanged(nameof(DamageBoostingInLogic));
            OnPropertyChanged(nameof(TreeCuttingGlitchInLogic));
            OnPropertyChanged(nameof(AllowWhistleUsageBehindTrees));
            OnPropertyChanged(nameof(ItemsDistributionViewModel));
            OnPropertyChanged(nameof(RegionRequirement));
            OnPropertyChanged(nameof(ItemRequirement));
            OnPropertyChanged(nameof(ItemLocation));
            OnPropertyChanged(nameof(DarkRegion));
            OnPropertyChanged(nameof(Joke));
        }
    }
}
