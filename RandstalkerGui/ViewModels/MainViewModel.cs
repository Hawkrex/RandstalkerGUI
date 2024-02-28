using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using RandstalkerGui.ViewModels.UserControls;
using RandstalkerGui.Views.Popups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace RandstalkerGui.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string CurrentCulture => Thread.CurrentThread.CurrentCulture.Name;

        private readonly Dictionary<object, List<string>> statusBarMessages = new Dictionary<object, List<string>>();

        public RelayCommand OnClose => new(OnCloseHandler);

        public void OnCloseHandler()
        {
            Log.Info($"{nameof(OnCloseHandler)}() => Closing window");

            Application.Current.Shutdown();
        }

        public RelayCommand Config => new(ConfigHandler);

        private void ConfigHandler()
        {
            var userConfigPopup = new UserConfigPopup();
            userConfigPopup.ShowDialog();
        }

        public RelayCommand<string> SwitchLanguage => new(SwitchLanguageHandler);

        private void SwitchLanguageHandler(string languageRegion)
        {
            if (Thread.CurrentThread.CurrentCulture.Name.Equals(languageRegion))
            {
                Log.Info($"{nameof(SwitchLanguageHandler)}() => Language is already {languageRegion}");
            }
            else
            {
                App.Instance.SwitchLanguage(languageRegion);
                OnPropertyChanged(nameof(CurrentCulture));
                Log.Info($"{nameof(SwitchLanguageHandler)}() => Language switched to {languageRegion}");
            }
        }

        public RelayCommand About => new(AboutHandler);

        private void AboutHandler()
        {
            MessageBox.Show((string)App.Instance.TryFindResource("AboutText"), (string)App.Instance.TryFindResource("AboutTitle"), MessageBoxButton.OK);
        }

        public PresetViewModel PresetViewModel { get; set; }

        private string statusBarMessage;
        public string StatusBarMessage
        {
            get => statusBarMessage;
            set => SetProperty(ref statusBarMessage, value);
        }

        public MainViewModel()
        {
            Log.Info($"--------------------------------------------------");
            Log.Info($"{nameof(MainViewModel)}() => Initialization");

            UserConfig.OnSavedValidUserConfig += SetStatusBarMessage;

            if (string.IsNullOrEmpty(UserConfig.Instance.CheckParametersValidity()))
            {
                PresetViewModel = new PresetViewModel(SetStatusBarMessage);
                PresetViewModel.OnError += SetStatusBarMessage;

                return;
            }

            string rootFolderErrorMessage = (string)App.Instance.TryFindResource("UserConfigNotValid") + Environment.NewLine + (string)App.Instance.TryFindResource("SelectRootFolder");
            MessageBox.Show(rootFolderErrorMessage, (string)App.Instance.TryFindResource("UserConfigNotValid"), MessageBoxButton.OK, MessageBoxImage.Error);

            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }

            UserConfig.Instance.RandstlakerExeFilePath = Path.Combine(dialog.FileName, "randstalker.exe");
            UserConfig.Instance.PresetsDirectoryPath = Path.Combine(dialog.FileName, "presets");
            UserConfig.Instance.PersonalSettingsDirectoryPath = dialog.FileName;
            UserConfig.Instance.InputRomFilePath = Path.Combine(dialog.FileName, "input.md");
            UserConfig.Instance.OutputRomDirectoryPath = Path.Combine(dialog.FileName, "seeds");

            string parametersInvalid = UserConfig.Instance.CheckParametersValidity();
            if (!string.IsNullOrEmpty(parametersInvalid))
            {
                MessageBox.Show(parametersInvalid, (string)App.Instance.TryFindResource("UserConfigNotValid"), MessageBoxButton.OK, MessageBoxImage.Warning);

                SetStatusBarMessage(this, new StatusBarMessageEventArgs() { Message = parametersInvalid, Sender = "UserConfig" });
            }
            else
            {
                try
                {
                    UserConfig.SaveFile();
                }
                catch (Exception ex)
                {
                    string fileErrorMessage = (string)App.Instance.TryFindResource("FileWriteErrorMessage");
                    Log.Error(fileErrorMessage, ex);
                    MessageBox.Show(fileErrorMessage, (string)App.Instance.TryFindResource("ErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            PresetViewModel = new PresetViewModel(SetStatusBarMessage);
            PresetViewModel.OnError += SetStatusBarMessage;
        }

        public void SetStatusBarMessage(object sender, StatusBarMessageEventArgs args)
        {
            if (statusBarMessages.ContainsKey(args.Sender))
            {
                if (string.IsNullOrEmpty(args.Message))
                {
                    statusBarMessages.Remove(args.Sender);
                }
                else
                {
                    statusBarMessages[args.Sender].Add(args.Message);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(args.Message))
                {
                    statusBarMessages.Add(args.Sender, new List<string>() { args.Message });
                }
            }

            StatusBarMessage = statusBarMessages?.FirstOrDefault().Value?.FirstOrDefault();
        }
    }
}
