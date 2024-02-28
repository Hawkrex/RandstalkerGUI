using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using RandstalkerGui.Models;
using RandstalkerGui.Properties;
using RandstalkerGui.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace RandstalkerGui.ViewModels.UserControls
{
    public class HomepageViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        const string permalinkMark = "Permalink: ";

        private readonly RandstalkerApp randstalkerApp;

        public FileTreeViewModel PresetTreeViewModel { get; set; }
        public FileTreeViewModel PersonalSettingsTreeViewModel { get; set; }

        private string outputLog;
        public string OutputLog
        {
            get => outputLog;
            set => SetProperty(ref outputLog, value);
        }

        private int progress;
        public int Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        private bool bingo;
        public bool Bingo
        {
            get => bingo;
            set => SetProperty(ref bingo, value);
        }

        private string permalinkToGenerateFrom;
        public string PermalinkToGenerateFrom
        {
            get => permalinkToGenerateFrom;
            set => SetProperty(ref permalinkToGenerateFrom, value);
        }

        private string outputRomFileName;
        public string OutputRomFileName
        {
            get => outputRomFileName;
            set => SetProperty(ref outputRomFileName, value);
        }

        private string permalinkToCopy;
        public string PermalinkToCopy
        {
            get => permalinkToCopy;
            set => SetProperty(ref outputRomFileName, value);
        }

        private bool canGenerateRom;
        public bool CanGenerateRom
        {
            get => canGenerateRom;
            set => SetProperty(ref canGenerateRom, value);
        }

        public RelayCommand GenerateRom => new(GenerateRomHandler);

        private void GenerateRomHandler()
        {
            Progress = 0;
            OutputLog = randstalkerApp.GenerateSeed(UserConfig.Instance.InputRomFilePath,
                UserConfig.Instance.OutputRomDirectoryPath,
                PresetTreeViewModel.SelectedFileRelativePath,
                Path.Combine(UserConfig.Instance.PersonalSettingsDirectoryPath, PersonalSettingsTreeViewModel.SelectedFileRelativePath),
                PermalinkToGenerateFrom,
                OutputRomFileName,
                Bingo);

            PermalinkToCopy = Regex.Match(OutputLog, @"Permalink: (.*)").Groups[1].Value;
            Progress = 100;

            UserConfig.Instance.LastUsedPresetFilePath = PresetTreeViewModel.SelectedFileRelativePath;
            UserConfig.Instance.LastUsedPersonalSettingsFilePath = PersonalSettingsTreeViewModel.SelectedFileRelativePath;

            UserConfig.SaveFile();
        }

        public RelayCommand CopyPermalink => new(CopyPermalinkHandler, () => !string.IsNullOrEmpty(PermalinkToCopy));

        private void CopyPermalinkHandler()
        {
            try
            {
                Clipboard.SetText(PermalinkToCopy);
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

            CanGenerateRom = string.IsNullOrEmpty(UserConfig.Instance.CheckParametersValidity());
            UserConfig.OnSavedValidUserConfig += OnSavedValidUserConfig;
        }

        public void OnSavedValidUserConfig(object sender, StatusBarMessageEventArgs args)
        {
            CanGenerateRom = string.IsNullOrEmpty(UserConfig.Instance.CheckParametersValidity());
        }
    }
}
