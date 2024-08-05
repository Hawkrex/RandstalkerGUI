using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public partial class HomepageViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly RandstalkerApp randstalkerApp;

        public FileTreeViewModel PresetTreeViewModel { get; set; }
        public FileTreeViewModel PersonalSettingsTreeViewModel { get; set; }

        [ObservableProperty]
        private string outputLog;

        [ObservableProperty]
        private int progress;

        [ObservableProperty]
        private bool bingo;

        [ObservableProperty]
        private bool canCopyPermalink;

        [ObservableProperty]
        private string copyPermalinkText;

        [ObservableProperty]
        private string permalinkToGenerateFrom;

        [ObservableProperty]
        private string outputRomFileName;

        [ObservableProperty]
        private string permalinkToCopy;

        [ObservableProperty]
        private bool canGenerateRom;

        [RelayCommand]
        private void GenerateRom()
        {
            Progress = 0;        
            OutputLog = randstalkerApp.GenerateSeed(UserConfig.Instance.InputRomFilePath,
                UserConfig.Instance.OutputRomDirectoryPath,
                PresetTreeViewModel.SelectedFileRelativePath,
                Path.Combine(UserConfig.Instance.PersonalSettingsDirectoryPath, PersonalSettingsTreeViewModel.SelectedFileRelativePath),
                PermalinkToGenerateFrom,
                OutputRomFileName,
                Bingo);

            PermalinkToCopy = Regex.Match(OutputLog, @"Permalink: (.*)\r").Groups[1].Value;
            Progress = 100;

            UserConfig.Instance.LastUsedPresetFilePath = PresetTreeViewModel.SelectedFileRelativePath;
            UserConfig.Instance.LastUsedPersonalSettingsFilePath = PersonalSettingsTreeViewModel.SelectedFileRelativePath;

            UserConfig.SaveFile();
        }

        [RelayCommand]
        private async void CopyPermalink()
        {
            try
            {
                Clipboard.SetText(PermalinkToCopy);

                CopyPermalinkText = (string)App.Instance.TryFindResource("Copied");
                CanCopyPermalink = false;
                await Task.Delay(3000);
                CanCopyPermalink = true;
                CopyPermalinkText = (string)App.Instance.TryFindResource("CopyPermalink");
            }
            catch (Exception ex)
            {
                string errorMessage = (string)App.Instance.TryFindResource("ClipboardCopyFailed");
                Log.Warn(errorMessage, ex);
                MessageBox.Show(errorMessage, (string)App.Instance.TryFindResource("Warning"), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public HomepageViewModel()
        {
            PresetTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PresetsDirectoryPath, new Dictionary<string, string>() { { ".json", "json files (*.json)|*.json" } }, Resources.DefaultPreset, UserConfig.Instance.LastUsedPresetFilePath, false);
            PersonalSettingsTreeViewModel = new FileTreeViewModel(UserConfig.Instance.PersonalSettingsDirectoryPath, new Dictionary<string, string>() { { ".json", "json files (*.json)|*.json" } }, Resources.DefaultPersonalSettings, UserConfig.Instance.LastUsedPersonalSettingsFilePath, false);

            randstalkerApp = new RandstalkerApp();

            Progress = 0;
            CopyPermalinkText = (string)App.Instance.TryFindResource("CopyPermalink");

            CanGenerateRom = string.IsNullOrEmpty(UserConfig.Instance.CheckParametersValidity());
            UserConfig.OnSavedValidUserConfig += OnSavedValidUserConfig;
        }

        public void OnSavedValidUserConfig(object sender, StatusBarMessageEventArgs args)
        {
            CanGenerateRom = string.IsNullOrEmpty(UserConfig.Instance.CheckParametersValidity());
        }

        partial void OnPermalinkToCopyChanged(string? value)
        {
            CanCopyPermalink = !string.IsNullOrWhiteSpace(value);
        }
    }
}
