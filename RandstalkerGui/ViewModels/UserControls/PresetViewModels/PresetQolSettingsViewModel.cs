using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System;

namespace RandstalkerGui.ViewModels.UserControls.PresetViewModels
{
    public class PresetQolSettingsViewModel : ValidationViewModel
    {
        private Preset preset;

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

        public int ShopPricesFactor
        {
            get => preset.RandomizerSettings.ShopPricesFactor;
            set => SetProperty(preset.RandomizerSettings.ShopPricesFactor, value, preset.RandomizerSettings, (rs, spf) => rs.ShopPricesFactor = spf);
        }

        public bool EnsureEkeEkeInShops
        {
            get => preset.RandomizerSettings.EnsureEkeEkeInShops;
            set => SetProperty(preset.RandomizerSettings.EnsureEkeEkeInShops, value, preset.RandomizerSettings, (rs, eeis) => rs.EnsureEkeEkeInShops = eeis);
        }

        public bool AllowSpoilerLog
        {
            get => preset.RandomizerSettings.AllowSpoilerLog;
            set => SetProperty(preset.RandomizerSettings.AllowSpoilerLog, value, preset.RandomizerSettings, (rs, asl) => rs.AllowSpoilerLog = asl);
        }

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

        public PresetQolSettingsViewModel(Preset preset)
        {
            this.preset = preset;
        }

        private void ValidateRequirements(string propertyName)
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

        public void UpdateProperties()
        {
            OnPropertyChanged(nameof(FixArmletSkip));
            OnPropertyChanged(nameof(RemoveTreeCuttingGlitchDrops));
            OnPropertyChanged(nameof(RemoveGumiBoulder));
            OnPropertyChanged(nameof(RemoveTiborRequirement));
            OnPropertyChanged(nameof(AllTreesVisitedAtStart));
            OnPropertyChanged(nameof(FastMenuTransitions));
            OnPropertyChanged(nameof(EkeekeAutoRevive));
            OnPropertyChanged(nameof(EnemiesDamageFactor));
            OnPropertyChanged(nameof(EnemiesHealthFactor));
            OnPropertyChanged(nameof(EnemiesArmorFactor));
            OnPropertyChanged(nameof(EnemiesGoldsFactor));
            OnPropertyChanged(nameof(EnemiesDropChanceFactor));
            OnPropertyChanged(nameof(HealthGainedPerLifestock));
            OnPropertyChanged(nameof(ShopPricesFactor));
            OnPropertyChanged(nameof(EnsureEkeEkeInShops));
            OnPropertyChanged(nameof(AllowSpoilerLog));
            OnPropertyChanged(nameof(RegionRequirement));
            OnPropertyChanged(nameof(ItemRequirement));
            OnPropertyChanged(nameof(ItemLocation));
            OnPropertyChanged(nameof(DarkRegion));
            OnPropertyChanged(nameof(Joke));
        }
    }
}
