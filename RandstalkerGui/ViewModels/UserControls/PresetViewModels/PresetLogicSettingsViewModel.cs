using CommunityToolkit.Mvvm.ComponentModel;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.UserControls.SubPresets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RandstalkerGui.ViewModels.UserControls.PresetViewModels
{
    public class PresetLogicSettingsViewModel : ObservableObject
    {
        private Preset preset;

        public int JewelCount
        {
            get => preset.GameSettings.JewelCount;
            set => SetProperty(preset.GameSettings.JewelCount, value, preset.GameSettings, (gs, jc) => gs.JewelCount = jc);
        }

        public Dictionary<string, string> GoalList { get; private set; }

        public string Goal
        {
            get => preset.GameSettings.Goal;
            set => SetProperty(preset.GameSettings.Goal, value, preset.GameSettings, (gs, g) => gs.Goal = g);
        }

        public bool ArmorUpgrades
        {
            get => preset.GameSettings.ArmorUpgrades;
            set => SetProperty(preset.GameSettings.ArmorUpgrades, value, preset.GameSettings, (gs, au) => gs.ArmorUpgrades = au);
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

        public bool ShuffleTrees
        {
            get => preset.RandomizerSettings.ShuffleTrees;
            set => SetProperty(preset.RandomizerSettings.ShuffleTrees, value, preset.RandomizerSettings, (rs, st) => rs.ShuffleTrees = st);
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

        public bool ChristmasEvent
        {
            get => preset.ChristmasEvent;
            set => SetProperty(preset.ChristmasEvent, value, preset, (p, ce) => p.ChristmasEvent = ce);
        }

        public ObservableCollection<string> Items { get; set; }

        public string FillerItem
        {
            get => preset.RandomizerSettings.FillerItem;
            set => SetProperty(preset.RandomizerSettings.FillerItem, value, preset.RandomizerSettings, (rs, fi) => rs.FillerItem = fi);
        }

        public ItemsCounterViewModel ItemsDistributionViewModel { get; set; }

        public ItemsListViewModel FiniteGroundItemsViewModel { get; set; }

        public ItemsListViewModel FiniteShopItemsViewModel { get; set; }

        public PresetLogicSettingsViewModel(Preset preset, ItemDefinitions itemDefinitions, EventHandler<StatusBarMessageEventArgs> setStatusBarMessage)
        {
            this.preset = preset;

            GoalList = new Dictionary<string, string>
            {
                { "beat_gola", "Beat Gola" },
                { "reach_kazalt", "Reach Kazat" },
                { "beat_dark_nole", "Beat Dark Nole" }
            };

            Items = new ObservableCollection<string>(itemDefinitions.Items.Select(i => i.Name));

            ItemsDistributionViewModel = new ItemsCounterViewModel(preset.RandomizerSettings.ItemsDistribution, itemDefinitions, setStatusBarMessage);
            FiniteGroundItemsViewModel = new ItemsListViewModel(preset.GameSettings.FiniteGroundItems, itemDefinitions);
            FiniteShopItemsViewModel = new ItemsListViewModel(preset.GameSettings.FiniteShopItems, itemDefinitions);
        }

        public void UpdateProperties()
        {
            OnPropertyChanged(nameof(JewelCount));
            OnPropertyChanged(nameof(Goal));
            OnPropertyChanged(nameof(ArmorUpgrades));
            OnPropertyChanged(nameof(ConsumableRecordBook));
            OnPropertyChanged(nameof(ConsumableSpellBook));
            OnPropertyChanged(nameof(ShuffleTrees));
            OnPropertyChanged(nameof(EnemyJumpingInLogic));
            OnPropertyChanged(nameof(DamageBoostingInLogic));
            OnPropertyChanged(nameof(TreeCuttingGlitchInLogic));
            OnPropertyChanged(nameof(AllowWhistleUsageBehindTrees));
            OnPropertyChanged(nameof(ChristmasEvent));
            OnPropertyChanged(nameof(FillerItem));

            OnPropertyChanged(nameof(ItemsDistributionViewModel));
            OnPropertyChanged(nameof(FiniteGroundItemsViewModel));
            OnPropertyChanged(nameof(FiniteShopItemsViewModel));
        }
    }
}
