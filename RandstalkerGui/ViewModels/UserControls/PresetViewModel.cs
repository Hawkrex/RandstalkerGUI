using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.UserControls.SubPresets;
using System;
using System.IO;
using System.Text;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class PresetViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Preset _preset;
        private ItemDefinitions _itemDefinitions;

        public FileTreeViewModel PresetTreeViewModel { get; set; }

        public bool ChristmasEvent
        {
            get
            {
                return _preset.ChristmasEvent;
            }
            set
            {
                if (_preset.ChristmasEvent != value)
                {
                    Log.Debug($"{nameof(_preset.ChristmasEvent)} => <{_preset.ChristmasEvent}> will change to <{value}>");
                    _preset.ChristmasEvent = value;
                    OnPropertyChanged();
                }
            }
        }

        public int JewelCount
        {
            get
            {
                return _preset.GameSettings.JewelCount;
            }
            set
            {
                if (_preset.GameSettings.JewelCount != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.JewelCount)} => <{_preset.GameSettings.JewelCount}> will change to <{value}>");
                    _preset.GameSettings.JewelCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ArmorUpgrades
        {
            get
            {
                return _preset.GameSettings.ArmorUpgrades;
            }
            set
            {
                if (_preset.GameSettings.ArmorUpgrades != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.ArmorUpgrades)} => <{_preset.GameSettings.ArmorUpgrades}> will change to <{value}>");
                    _preset.GameSettings.ArmorUpgrades = value;
                    OnPropertyChanged();
                }
            }
        }

        public int StartingGold
        {
            get
            {
                return _preset.GameSettings.StartingGold;
            }
            set
            {
                if (_preset.GameSettings.StartingGold != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.StartingGold)} => <{_preset.GameSettings.StartingGold}> will change to <{value}>");
                    _preset.GameSettings.StartingGold = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AutomaticStartingLife
        {
            get
            {
                return _preset.GameSettings.StartingLife == 0;
            }
            set
            {
                if (value && _preset.GameSettings.StartingLife != 0)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.StartingLife)} => <{_preset.GameSettings.StartingLife}> will change to 0");
                    _preset.GameSettings.StartingLife = 0;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StartingLife));
                }
                else if (!value && _preset.GameSettings.StartingLife == 0)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.StartingLife)} => <{_preset.GameSettings.StartingLife}> will change to 0");
                    _preset.GameSettings.StartingLife = 4;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StartingLife));
                }
            }
        }

        public int StartingLife
        {
            get
            {
                return _preset.GameSettings.StartingLife;
            }
            set
            {
                if (_preset.GameSettings.StartingLife != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.StartingLife)} => <{_preset.GameSettings.StartingLife}> will change to <{value}>");
                    _preset.GameSettings.StartingLife = value;
                    OnPropertyChanged();
                }
            }
        }

        public ItemsCounterViewModel StartingsItemsViewModel { get; set; }

        public bool FixArmletSkip
        {
            get
            {
                return _preset.GameSettings.FixArmletSkip;
            }
            set
            {
                if (_preset.GameSettings.FixArmletSkip != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.FixArmletSkip)} => <{_preset.GameSettings.FixArmletSkip}> will change to <{value}>");
                    _preset.GameSettings.FixArmletSkip = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RemoveTreeCuttingGlitchDrops
        {
            get
            {
                return _preset.GameSettings.RemoveTreeCuttingGlitchDrops;
            }
            set
            {
                if (_preset.GameSettings.RemoveTreeCuttingGlitchDrops != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.RemoveTreeCuttingGlitchDrops)} => <{_preset.GameSettings.RemoveTreeCuttingGlitchDrops}> will change to <{value}>");
                    _preset.GameSettings.RemoveTreeCuttingGlitchDrops = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ConsumableRecordBook
        {
            get
            {
                return _preset.GameSettings.ConsumableRecordBook;
            }
            set
            {
                if (_preset.GameSettings.ConsumableRecordBook != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.ConsumableRecordBook)} => <{_preset.GameSettings.ConsumableRecordBook}> will change to <{value}>");
                    _preset.GameSettings.ConsumableRecordBook = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RemoveGumiBoulder
        {
            get
            {
                return _preset.GameSettings.RemoveGumiBoulder;
            }
            set
            {
                if (_preset.GameSettings.RemoveGumiBoulder != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.RemoveGumiBoulder)} => <{_preset.GameSettings.RemoveGumiBoulder}> will change to <{value}>");
                    _preset.GameSettings.RemoveGumiBoulder = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RemoveTiborRequirement
        {
            get
            {
                return _preset.GameSettings.RemoveTiborRequirement;
            }
            set
            {
                if (_preset.GameSettings.RemoveTiborRequirement != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.RemoveTiborRequirement)} => <{_preset.GameSettings.RemoveTiborRequirement}> will change to <{value}>");
                    _preset.GameSettings.RemoveTiborRequirement = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AllTreesVisitedAtStart
        {
            get
            {
                return _preset.GameSettings.AllTreesVisitedAtStart;
            }
            set
            {
                if (_preset.GameSettings.AllTreesVisitedAtStart != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.AllTreesVisitedAtStart)} => <{_preset.GameSettings.AllTreesVisitedAtStart}> will change to <{value}>");
                    _preset.GameSettings.AllTreesVisitedAtStart = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesDamageFactor
        {
            get
            {
                return _preset.GameSettings.EnemiesDamageFactor;
            }
            set
            {
                if (_preset.GameSettings.EnemiesDamageFactor != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.EnemiesDamageFactor)} => <{_preset.GameSettings.EnemiesDamageFactor}> will change to <{value}>");
                    _preset.GameSettings.EnemiesDamageFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesHealthFactor
        {
            get
            {
                return _preset.GameSettings.EnemiesHealthFactor;
            }
            set
            {
                if (_preset.GameSettings.EnemiesHealthFactor != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.EnemiesHealthFactor)} => <{_preset.GameSettings.EnemiesHealthFactor}> will change to <{value}>");
                    _preset.GameSettings.EnemiesHealthFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesArmorFactor
        {
            get
            {
                return _preset.GameSettings.EnemiesArmorFactor;
            }
            set
            {
                if (_preset.GameSettings.EnemiesArmorFactor != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.EnemiesArmorFactor)} => <{_preset.GameSettings.EnemiesArmorFactor}> will change to <{value}>");
                    _preset.GameSettings.EnemiesArmorFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesGoldsFactor
        {
            get
            {
                return _preset.GameSettings.EnemiesGoldsFactor;
            }
            set
            {
                if (_preset.GameSettings.EnemiesGoldsFactor != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.EnemiesGoldsFactor)} => <{_preset.GameSettings.EnemiesGoldsFactor}> will change to <{value}>");
                    _preset.GameSettings.EnemiesGoldsFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int EnemiesDropChanceFactor
        {
            get
            {
                return _preset.GameSettings.EnemiesDropChanceFactor;
            }
            set
            {
                if (_preset.GameSettings.EnemiesDropChanceFactor != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.EnemiesDropChanceFactor)} => <{_preset.GameSettings.EnemiesDropChanceFactor}> will change to <{value}>");
                    _preset.GameSettings.EnemiesDropChanceFactor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int HealthGainedPerLifestock
        {
            get
            {
                return _preset.GameSettings.HealthGainedPerLifestock;
            }
            set
            {
                if (_preset.GameSettings.HealthGainedPerLifestock != value)
                {
                    Log.Debug($"{nameof(_preset.GameSettings.HealthGainedPerLifestock)} => <{_preset.GameSettings.HealthGainedPerLifestock}> will change to <{value}>");
                    _preset.GameSettings.HealthGainedPerLifestock = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AllowSpoilerLog
        {
            get
            {
                return _preset.RandomizerSettings.AllowSpoilerLog;
            }
            set
            {
                if (_preset.RandomizerSettings.AllowSpoilerLog != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.AllowSpoilerLog)} => <{_preset.RandomizerSettings.AllowSpoilerLog}> will change to <{value}>");
                    _preset.RandomizerSettings.AllowSpoilerLog = value;
                    OnPropertyChanged();
                }
            }
        }

        public SpawnLocationsViewModel SpawnLocationsViewModel { get; set; }

        public bool ShuffleTrees
        {
            get
            {
                return _preset.RandomizerSettings.ShuffleTrees;
            }
            set
            {
                if (_preset.RandomizerSettings.ShuffleTrees != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.ShuffleTrees)} => <{_preset.RandomizerSettings.ShuffleTrees}> will change to <{value}>");
                    _preset.RandomizerSettings.ShuffleTrees = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool EnemyJumpingInLogic
        {
            get
            {
                return _preset.RandomizerSettings.EnemyJumpingInLogic;
            }
            set
            {
                if (_preset.RandomizerSettings.EnemyJumpingInLogic != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.EnemyJumpingInLogic)} => <{_preset.RandomizerSettings.EnemyJumpingInLogic}> will change to <{value}>");
                    _preset.RandomizerSettings.EnemyJumpingInLogic = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool DamageBoostingInLogic
        {
            get
            {
                return _preset.RandomizerSettings.DamageBoostingInLogic;
            }
            set
            {
                if (_preset.RandomizerSettings.DamageBoostingInLogic != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.DamageBoostingInLogic)} => <{_preset.RandomizerSettings.DamageBoostingInLogic}> will change to <{value}>");
                    _preset.RandomizerSettings.DamageBoostingInLogic = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool TreeCuttingGlitchInLogic
        {
            get
            {
                return _preset.RandomizerSettings.TreeCuttingGlitchInLogic;
            }
            set
            {
                if (_preset.RandomizerSettings.TreeCuttingGlitchInLogic != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.TreeCuttingGlitchInLogic)} => <{_preset.RandomizerSettings.TreeCuttingGlitchInLogic}> will change to <{value}>");
                    _preset.RandomizerSettings.TreeCuttingGlitchInLogic = value;
                    OnPropertyChanged();
                }
            }
        }

        public ItemsCounterViewModel ItemsDistributionViewModel { get; set; }

        public int RegionRequirement
        {
            get
            {
                return _preset.RandomizerSettings.HintsDistribution.RegionRequirement;
            }
            set
            {
                if (_preset.RandomizerSettings.HintsDistribution.RegionRequirement != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.HintsDistribution.RegionRequirement)} => <{_preset.RandomizerSettings.HintsDistribution.RegionRequirement}> will change to <{value}>");
                    _preset.RandomizerSettings.HintsDistribution.RegionRequirement = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ItemRequirement
        {
            get
            {
                return _preset.RandomizerSettings.HintsDistribution.ItemRequirement;
            }
            set
            {
                if (_preset.RandomizerSettings.HintsDistribution.ItemRequirement != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.HintsDistribution.ItemRequirement)} => <{_preset.RandomizerSettings.HintsDistribution.ItemRequirement}> will change to <{value}>");
                    _preset.RandomizerSettings.HintsDistribution.ItemRequirement = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ItemLocation
        {
            get
            {
                return _preset.RandomizerSettings.HintsDistribution.ItemLocation;
            }
            set
            {
                if (_preset.RandomizerSettings.HintsDistribution.ItemLocation != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.HintsDistribution.ItemLocation)} => <{_preset.RandomizerSettings.HintsDistribution.ItemLocation}> will change to <{value}>");
                    _preset.RandomizerSettings.HintsDistribution.ItemLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        public int DarkRegion
        {
            get
            {
                return _preset.RandomizerSettings.HintsDistribution.DarkRegion;
            }
            set
            {
                if (_preset.RandomizerSettings.HintsDistribution.DarkRegion != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.HintsDistribution.DarkRegion)} => <{_preset.RandomizerSettings.HintsDistribution.DarkRegion}> will change to <{value}>");
                    _preset.RandomizerSettings.HintsDistribution.DarkRegion = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Joke
        {
            get
            {
                return _preset.RandomizerSettings.HintsDistribution.Joke;
            }
            set
            {
                if (_preset.RandomizerSettings.HintsDistribution.Joke != value)
                {
                    Log.Debug($"{nameof(_preset.RandomizerSettings.HintsDistribution.Joke)} => <{_preset.RandomizerSettings.HintsDistribution.Joke}> will change to <{value}>");
                    _preset.RandomizerSettings.HintsDistribution.Joke = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand SavePreset { get { return new RelayCommand(_ => SavePresetHandler()); } }
        private void SavePresetHandler()
        {
            Log.Debug($"{nameof(SavePresetHandler)}() => Command requested ...");

            _preset.GameSettings.StartingItems = StartingsItemsViewModel.ComputePresetInfos();
            _preset.RandomizerSettings.SpawnLocations = SpawnLocationsViewModel.ComputePresetInfos();
            _preset.RandomizerSettings.ItemsDistribution = ItemsDistributionViewModel.ComputePresetInfos();

            File.WriteAllText(UserConfig.Instance.PresetsDirectoryPath + '/' + PresetTreeViewModel.SelectedFileRelativePath, JsonConvert.SerializeObject(_preset));

            Log.Debug($"{nameof(SavePresetHandler)}() => Command executed");
        }

        public PresetViewModel()
        {
            if (File.Exists(UserConfig.Instance.PresetsDirectoryPath + '/' + UserConfig.Instance.LastUsedPresetFilePath))
                _preset = JsonConvert.DeserializeObject<Preset>(File.ReadAllText(UserConfig.Instance.PresetsDirectoryPath + '/' + UserConfig.Instance.LastUsedPresetFilePath));
            else
                _preset = JsonConvert.DeserializeObject<Preset>(Encoding.UTF8.GetString(Resources.DefaultPreset));

            _itemDefinitions = new ItemDefinitions();

            PresetTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PresetsDirectoryPath);
            PresetTreeViewModel.PropertyChanged += PresetTreeViewModel_PropertyChanged;

            StartingsItemsViewModel = new ItemsCounterViewModel(_preset.GameSettings.StartingItems, _itemDefinitions);
            SpawnLocationsViewModel = new SpawnLocationsViewModel(_preset.RandomizerSettings.SpawnLocations);
            ItemsDistributionViewModel = new ItemsCounterViewModel(_preset.RandomizerSettings.ItemsDistribution, _itemDefinitions);
        }

        private void PresetTreeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(PresetTreeViewModel.SelectedFileRelativePath))
            {
                _preset = JsonConvert.DeserializeObject<Preset>(File.ReadAllText(UserConfig.Instance.PresetsDirectoryPath + '/' + PresetTreeViewModel.SelectedFileRelativePath));
                StartingsItemsViewModel = new ItemsCounterViewModel(_preset.GameSettings.StartingItems, _itemDefinitions);
                SpawnLocationsViewModel = new SpawnLocationsViewModel(_preset.RandomizerSettings.SpawnLocations);
                ItemsDistributionViewModel = new ItemsCounterViewModel(_preset.RandomizerSettings.ItemsDistribution, _itemDefinitions);

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
            OnPropertyChanged(nameof(RemoveGumiBoulder));
            OnPropertyChanged(nameof(RemoveTiborRequirement));
            OnPropertyChanged(nameof(AllTreesVisitedAtStart));
            OnPropertyChanged(nameof(EnemiesDamageFactor));
            OnPropertyChanged(nameof(EnemiesHealthFactor));
            OnPropertyChanged(nameof(EnemiesArmorFactor));
            OnPropertyChanged(nameof(EnemiesGoldsFactor));
            OnPropertyChanged(nameof(EnemiesDropChanceFactor));
            OnPropertyChanged(nameof(HealthGainedPerLifestock));
            OnPropertyChanged(nameof(AllowSpoilerLog));
            OnPropertyChanged(nameof(SpawnLocationsViewModel));
            OnPropertyChanged(nameof(ShuffleTrees));
            OnPropertyChanged(nameof(EnemyJumpingInLogic));
            OnPropertyChanged(nameof(DamageBoostingInLogic));
            OnPropertyChanged(nameof(TreeCuttingGlitchInLogic));
            OnPropertyChanged(nameof(ItemsDistributionViewModel));
            OnPropertyChanged(nameof(RegionRequirement));
            OnPropertyChanged(nameof(ItemRequirement));
            OnPropertyChanged(nameof(ItemLocation));
            OnPropertyChanged(nameof(DarkRegion));
            OnPropertyChanged(nameof(Joke));
        }
    }
}
