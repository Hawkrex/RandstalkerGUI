using RandstalkerGui.Tools;
using RandstalkerGui.Views.Popups;
using System;
using System.Threading;
using System.Windows;

namespace RandstalkerGui.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RelayCommand OnClose { get { return new RelayCommand(_ => OnCloseHandler()); } }
        public void OnCloseHandler()
        {
            Log.Info($"{nameof(OnCloseHandler)}() => Closing window");

            Application.Current.Shutdown();
        }

        public RelayCommand Config { get { return new RelayCommand(_ => ConfigHandler()); } }
        private void ConfigHandler()
        {
            Log.Debug($"{nameof(ConfigHandler)}() => Command requested ...");

            UserConfigPopup userConfigPopup = new UserConfigPopup();
            userConfigPopup.ShowDialog();

            Log.Debug($"{nameof(ConfigHandler)}() => Command executed");
        }

        public string CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.Name;
            }
        }

        public RelayCommand SwitchLanguage { get { return new RelayCommand(param => SwitchLanguageHandler(param)); } }
        private void SwitchLanguageHandler(object param)
        {
            Log.Debug($"{nameof(SwitchLanguageHandler)}() => Command requested ...");

            string languageRegion = param.ToString();
            if (languageRegion == null)
            {
                throw new ArgumentException("Parameter is not a string");
            }

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

            Log.Debug($"{nameof(SwitchLanguageHandler)}() => Command executed");
        }

        public RelayCommand About { get { return new RelayCommand(_ => AboutHandler()); } }
        private void AboutHandler()
        {
            Log.Debug($"{nameof(AboutHandler)}() => Command requested ...");

            MessageBox.Show((string)App.Instance.TryFindResource("AboutText"), (string)App.Instance.TryFindResource("AboutTitle"), MessageBoxButton.OK);

            Log.Debug($"{nameof(AboutHandler)}() => Command executed");
        }

        public MainViewModel()
        {
            Log.Info($"--------------------------------------------------");
            Log.Info($"{nameof(MainViewModel)}() => Initialization");
        }
    }
}
