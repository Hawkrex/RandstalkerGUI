using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.UserControls.SubPresets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class PresetViewModel : ValidationViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Preset preset;

        private readonly ItemDefinitions itemDefinitions;

        public FileTreeViewModel PresetTreeViewModel { get; set; }

        public ObservableCollection<string> Items { get; set; }

        public bool ChristmasEvent
        {
            get => preset.ChristmasEvent;
            set => SetProperty(preset.ChristmasEvent, value, preset, (p, ce) => p.ChristmasEvent = ce);
        }

        public Dictionary<string, string> GoalList { get; private set; }

        public string Goal
        {
            get => preset.GameSettings.Goal;
            set => SetProperty(preset.GameSettings.Goal, value, preset.GameSettings, (gs, g) => gs.Goal = g);
        }

        public int JewelCount
        {
            get => preset.GameSettings.JewelCount;
            set => SetProperty(preset.GameSettings.JewelCount, value, preset.GameSettings, (gs, jc) => gs.JewelCount = jc);
        }

        public bool ArmorUpgrades
        {
            get => preset.GameSettings.ArmorUpgrades;
            set => SetProperty(preset.GameSettings.ArmorUpgrades, value, preset.GameSettings, (gs, au) => gs.ArmorUpgrades = au);
        }

        public int StartingGold
        {
            get => preset.GameSettings.StartingGold;
            set => SetProperty(preset.GameSettings.StartingGold, value, preset.GameSettings, (gs, sg) => gs.StartingGold = sg);
        }

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

        public ItemsCounterViewModel StartingsItemsViewModel { get; set; }

        public bool FixArmletSkip
        {
            get => preset.GameSettings.FixArmletSkip;
            set => SetProperty(preset.GameSettings.FixArmletSkip, value, preset.GameSettings, (gs, fas) => gs.FixArmletSkip = fas);
        }

        public bool RemoveTreeCuttingGlitchDrops
        {
            get => preset.GameSettings.RemoveTreeCuttingGlitchDrops;
            set => SetProperty(preset.GameSettings.RemoveTreeCuttingGlitchDrops, value, preset.GameSettings, (gs, rtcgd) => gs.RemoveTreeCuttingGlitchDrops = rtcgd);
        }

        public bool ConsumableRecordBook
        {
            get => preset.GameSettings.ConsumableRecordBook;
            set => SetProperty(preset.GameSettings.ConsumableRecordBook, value, preset.GameSettings, (gs, crb) => gs.ConsumableRecordBook = crb);
        }

        public bool ConsumableSpellBook
        {
            get => preset.GameSettings.ConsumableSpellBook;
            set => SetProperty(preset.GameSettings.ConsumableSpellBook, value, preset.GameSettings, (gs, csb) => gs.ConsumableSpellBook = csb);
        }

        public bool RemoveGumiBoulder
        {
            get => preset.GameSettings.RemoveGumiBoulder;
            set => SetProperty(preset.GameSettings.RemoveGumiBoulder, value, preset.GameSettings, (gs, rgb) => gs.RemoveGumiBoulder = rgb);
        }

        public bool RemoveTiborRequirement
        {
            get => preset.GameSettings.RemoveTiborRequirement;
            set => SetProperty(preset.GameSettings.RemoveTiborRequirement, value, preset.GameSettings, (gs, rtr) => gs.RemoveTiborRequirement = rtr);
        }

        public bool AllTreesVisitedAtStart
        {
            get => preset.GameSettings.AllTreesVisitedAtStart;
            set => SetProperty(preset.GameSettings.AllTreesVisitedAtStart, value, preset.GameSettings, (gs, atvas) => gs.AllTreesVisitedAtStart = atvas);
        }

        public bool FastMenuTransitions
        {
            get => preset.GameSettings.FastMenuTransitions;
            set => SetProperty(preset.GameSettings.FastMenuTransitions, value, preset.GameSettings, (gs, fmt) => gs.FastMenuTransitions = fmt);
        }

        public bool EkeekeAutoRevive
        {
            get => preset.GameSettings.EkeekeAutoRevive;
            set => SetProperty(preset.GameSettings.EkeekeAutoRevive, value, preset.GameSettings, (gs, ear) => gs.EkeekeAutoRevive = ear);
        }

        public int EnemiesDamageFactor
        {
            get => preset.GameSettings.EnemiesDamageFactor;
            set => SetProperty(preset.GameSettings.EnemiesDamageFactor, value, preset.GameSettings, (gs, edf) => gs.EnemiesDamageFactor = edf);
        }

        public int EnemiesHealthFactor
        {
            get => preset.GameSettings.EnemiesHealthFactor;
            set => SetProperty(preset.GameSettings.EnemiesHealthFactor, value, preset.GameSettings, (gs, ehf) => gs.EnemiesHealthFactor = ehf);
        }

        public int EnemiesArmorFactor
        {
            get => preset.GameSettings.EnemiesArmorFactor;
            set => SetProperty(preset.GameSettings.EnemiesArmorFactor, value, preset.GameSettings, (gs, eaf) => gs.EnemiesArmorFactor = eaf);
        }

        public int EnemiesGoldsFactor
        {
            get => preset.GameSettings.EnemiesGoldsFactor;
            set => SetProperty(preset.GameSettings.EnemiesGoldsFactor, value, preset.GameSettings, (gs, egf) => gs.EnemiesGoldsFactor = egf);
        }

        public int EnemiesDropChanceFactor
        {
            get => preset.GameSettings.EnemiesDropChanceFactor;
            set => SetProperty(preset.GameSettings.EnemiesDropChanceFactor, value, preset.GameSettings, (gs, edcf) => gs.EnemiesDropChanceFactor = edcf);
        }

        public int HealthGainedPerLifestock
        {
            get => preset.GameSettings.HealthGainedPerLifestock;
            set => SetProperty(preset.GameSettings.HealthGainedPerLifestock, value, preset.GameSettings, (gs, hgpl) => gs.HealthGainedPerLifestock = hgpl);
        }

        public bool AllowSpoilerLog
        {
            get => preset.RandomizerSettings.AllowSpoilerLog;
            set => SetProperty(preset.RandomizerSettings.AllowSpoilerLog, value, preset.RandomizerSettings, (rs, asl) => rs.AllowSpoilerLog = asl);
        }

        public SpawnLocationsViewModel SpawnLocationsViewModel { get; set; }

        public bool ShuffleTrees
        {
            get => preset.RandomizerSettings.ShuffleTrees;
            set => SetProperty(preset.RandomizerSettings.ShuffleTrees, value, preset.RandomizerSettings, (rs, st) => rs.ShuffleTrees = st);
        }

        public int ShopPricesFactor
        {
            get => preset.RandomizerSettings.ShopPricesFactor;
            set => SetProperty(preset.RandomizerSettings.ShopPricesFactor, value, preset.RandomizerSettings, (rs, spf) => rs.ShopPricesFactor = spf);
        }

        public bool EnemyJumpingInLogic
        {
            get => preset.RandomizerSettings.EnemyJumpingInLogic;
            set => SetProperty(preset.RandomizerSettings.EnemyJumpingInLogic, value, preset.RandomizerSettings, (rs, ejil) => rs.EnemyJumpingInLogic = ejil);
        }

        public bool DamageBoostingInLogic
        {
            get => preset.RandomizerSettings.DamageBoostingInLogic;
            set => SetProperty(preset.RandomizerSettings.DamageBoostingInLogic, value, preset.RandomizerSettings, (rs, dbil) => rs.DamageBoostingInLogic = dbil);
        }

        public bool TreeCuttingGlitchInLogic
        {
            get => preset.RandomizerSettings.TreeCuttingGlitchInLogic;
            set => SetProperty(preset.RandomizerSettings.TreeCuttingGlitchInLogic, value, preset.RandomizerSettings, (rs, tcgil) => rs.TreeCuttingGlitchInLogic = tcgil);
        }

        public bool AllowWhistleUsageBehindTrees
        {
            get => preset.RandomizerSettings.AllowWhistleUsageBehindTrees;
            set => SetProperty(preset.RandomizerSettings.AllowWhistleUsageBehindTrees, value, preset.RandomizerSettings, (rs, awubt) => rs.AllowWhistleUsageBehindTrees = awubt);
        }

        public bool EnsureEkeEkeInShops
        {
            get => preset.RandomizerSettings.EnsureEkeEkeInShops;
            set => SetProperty(preset.RandomizerSettings.EnsureEkeEkeInShops, value, preset.RandomizerSettings, (rs, eeis) => rs.EnsureEkeEkeInShops = eeis);
        }

        public string FillerItem
        {
            get => preset.RandomizerSettings.FillerItem;
            set => SetProperty(preset.RandomizerSettings.FillerItem, value, preset.RandomizerSettings, (rs, fi) => rs.FillerItem = fi);
        }

        public ItemsCounterViewModel ItemsDistributionViewModel { get; set; }

        public ItemsListViewModel FiniteGroundItemsViewModel { get; set; }

        public ItemsListViewModel FiniteShopItemsViewModel { get; set; }

        public int RegionRequirement
        {
            get => preset.RandomizerSettings.HintsDistribution.RegionRequirement;
            set
            {
                if (SetProperty(preset.RandomizerSettings.HintsDistribution.RegionRequirement, value, preset.RandomizerSettings.HintsDistribution, (hd, rr) => hd.RegionRequirement = rr))
                {
                    ValidateRequirements(nameof(RegionRequirement));

                    OnPropertyChanged(nameof(ItemRequirement));
                    OnPropertyChanged(nameof(ItemLocation));
                    OnPropertyChanged(nameof(DarkRegion));
                    OnPropertyChanged(nameof(Joke));
                }
            }
        }

        public int ItemRequirement
        {
            get => preset.RandomizerSettings.HintsDistribution.ItemRequirement;
            set
            {
                if (SetProperty(preset.RandomizerSettings.HintsDistribution.ItemRequirement, value, preset.RandomizerSettings.HintsDistribution, (hd, ir) => hd.ItemRequirement = ir))
                {
                    ValidateRequirements(nameof(ItemRequirement));

                    OnPropertyChanged(nameof(RegionRequirement));
                    OnPropertyChanged(nameof(ItemLocation));
                    OnPropertyChanged(nameof(DarkRegion));
                    OnPropertyChanged(nameof(Joke));
                }
            }
        }

        public int ItemLocation
        {
            get => preset.RandomizerSettings.HintsDistribution.ItemLocation;
            set
            {
                if (SetProperty(preset.RandomizerSettings.HintsDistribution.ItemLocation, value, preset.RandomizerSettings.HintsDistribution, (hd, il) => hd.ItemLocation = il))
                {
                    ValidateRequirements(nameof(ItemLocation));

                    OnPropertyChanged(nameof(RegionRequirement));
                    OnPropertyChanged(nameof(ItemRequirement));
                    OnPropertyChanged(nameof(DarkRegion));
                    OnPropertyChanged(nameof(Joke));
                }
            }
        }

        public int DarkRegion
        {
            get => preset.RandomizerSettings.HintsDistribution.DarkRegion;
            set
            {
                if (SetProperty(preset.RandomizerSettings.HintsDistribution.DarkRegion, value, preset.RandomizerSettings.HintsDistribution, (hd, dr) => hd.DarkRegion = dr))
                {
                    ValidateRequirements(nameof(DarkRegion));

                    OnPropertyChanged(nameof(RegionRequirement));
                    OnPropertyChanged(nameof(ItemRequirement));
                    OnPropertyChanged(nameof(ItemLocation));
                    OnPropertyChanged(nameof(Joke));
                }
            }
        }

        public int Joke
        {
            get => preset.RandomizerSettings.HintsDistribution.Joke;
            set
            {
                if (SetProperty(preset.RandomizerSettings.HintsDistribution.Joke, value, preset.RandomizerSettings.HintsDistribution, (hd, j) => hd.Joke = j))
                {
                    ValidateRequirements(nameof(Joke));

                    OnPropertyChanged(nameof(RegionRequirement));
                    OnPropertyChanged(nameof(ItemRequirement));
                    OnPropertyChanged(nameof(ItemLocation));
                    OnPropertyChanged(nameof(DarkRegion));
                }
            }
        }

        public event EventHandler<StatusBarMessageEventArgs> OnError;

        public void ValidateRequirements(string propertyName)
        {
            string error = string.Empty;

            ClearErrors(propertyName);

            switch (propertyName)
            {
                case nameof(RegionRequirement):
                case nameof(ItemRequirement):
                case nameof(ItemLocation):
                case nameof(DarkRegion):
                case nameof(Joke):

                    ClearErrors(nameof(RegionRequirement));
                    ClearErrors(nameof(ItemRequirement));
                    ClearErrors(nameof(ItemLocation));
                    ClearErrors(nameof(DarkRegion));
                    ClearErrors(nameof(Joke));

                    if (RegionRequirement + ItemRequirement + ItemLocation + DarkRegion + Joke > 147)
                    {
                        error = (string)App.Instance.TryFindResource("HintsNumberError");
                        AddError(nameof(RegionRequirement), error);
                        AddError(nameof(ItemRequirement), error);
                        AddError(nameof(ItemLocation), error);
                        AddError(nameof(DarkRegion), error);
                        AddError(nameof(Joke), error);
                    }
                    break;
            }

            OnError?.Invoke(this, new StatusBarMessageEventArgs() { Message = error, Sender = "Preset" });
        }

        public RelayCommand SavePreset => new(SavePresetHandler);

        private void SavePresetHandler()
        {
            preset.GameSettings.StartingItems = StartingsItemsViewModel.FormatSettings();
            preset.RandomizerSettings.SpawnLocations = SpawnLocationsViewModel.FormatSettings();
            preset.RandomizerSettings.ItemsDistribution = ItemsDistributionViewModel.FormatSettings();
            preset.GameSettings.FiniteGroundItems = FiniteGroundItemsViewModel.FormatSettings();
            preset.GameSettings.FiniteShopItems = FiniteShopItemsViewModel.FormatSettings();

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

            StartingsItemsViewModel = new ItemsCounterViewModel(preset.GameSettings.StartingItems, itemDefinitions);
            SpawnLocationsViewModel = new SpawnLocationsViewModel(preset.RandomizerSettings.SpawnLocations);
            ItemsDistributionViewModel = new ItemsCounterViewModel(preset.RandomizerSettings.ItemsDistribution, itemDefinitions, setStatusBarMessage);
            FiniteGroundItemsViewModel = new ItemsListViewModel(preset.GameSettings.FiniteGroundItems, itemDefinitions);
            FiniteShopItemsViewModel = new ItemsListViewModel(preset.GameSettings.FiniteShopItems, itemDefinitions);

            GoalList = new Dictionary<string, string>
            {
                { "beat_gola", "Beat Gola" },
                { "reach_kazalt", "Reach Kazat" },
                { "beat_dark_nole", "Beat Dark Nole" }
            };

            Items = new ObservableCollection<string>(itemDefinitions.Items.Select(i => i.Name));
        }

        private void PresetTreeViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(PresetTreeViewModel.SelectedFileRelativePath))
            {
                return;
            }

            preset = JsonConvert.DeserializeObject<Preset>(File.ReadAllText(Path.Combine(UserConfig.Instance.PresetsDirectoryPath, PresetTreeViewModel.SelectedFileRelativePath)));

            StartingsItemsViewModel = new ItemsCounterViewModel(preset.GameSettings.StartingItems, itemDefinitions);
            SpawnLocationsViewModel = new SpawnLocationsViewModel(preset.RandomizerSettings.SpawnLocations);
            ItemsDistributionViewModel = new ItemsCounterViewModel(preset.RandomizerSettings.ItemsDistribution, itemDefinitions, setStatusBarMessage);

            UpdateProperties();
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(ChristmasEvent));
            OnPropertyChanged(nameof(Goal));
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
            OnPropertyChanged(nameof(FastMenuTransitions));
            OnPropertyChanged(nameof(FiniteGroundItemsViewModel));
            OnPropertyChanged(nameof(FiniteShopItemsViewModel));
            OnPropertyChanged(nameof(EkeekeAutoRevive));
            OnPropertyChanged(nameof(EnemiesDamageFactor));
            OnPropertyChanged(nameof(EnemiesHealthFactor));
            OnPropertyChanged(nameof(EnemiesArmorFactor));
            OnPropertyChanged(nameof(EnemiesGoldsFactor));
            OnPropertyChanged(nameof(EnemiesDropChanceFactor));
            OnPropertyChanged(nameof(HealthGainedPerLifestock));
            OnPropertyChanged(nameof(AllowSpoilerLog));
            OnPropertyChanged(nameof(SpawnLocationsViewModel));
            OnPropertyChanged(nameof(ShuffleTrees));
            OnPropertyChanged(nameof(ShopPricesFactor));
            OnPropertyChanged(nameof(EnemyJumpingInLogic));
            OnPropertyChanged(nameof(DamageBoostingInLogic));
            OnPropertyChanged(nameof(TreeCuttingGlitchInLogic));
            OnPropertyChanged(nameof(AllowWhistleUsageBehindTrees));
            OnPropertyChanged(nameof(EnsureEkeEkeInShops));
            OnPropertyChanged(nameof(FillerItem));
            OnPropertyChanged(nameof(ItemsDistributionViewModel));
            OnPropertyChanged(nameof(RegionRequirement));
            OnPropertyChanged(nameof(ItemRequirement));
            OnPropertyChanged(nameof(ItemLocation));
            OnPropertyChanged(nameof(DarkRegion));
            OnPropertyChanged(nameof(Joke));
        }
    }
}
