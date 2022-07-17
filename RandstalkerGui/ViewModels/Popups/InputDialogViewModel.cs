using RandstalkerGui.Tools;
using System.Windows;

namespace RandstalkerGui.ViewModels.Popups
{
    public class InputDialogViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string Input { get; set; }

        public RelayCommand<Window> Validate { get { return new RelayCommand<Window>(dialog => ValidateHandler(dialog)); } }
        private void ValidateHandler(Window dialog)
        {
            Log.Debug($"{nameof(ValidateHandler)}() => Command requested ...");

            if (dialog != null)
            {
                dialog.DialogResult = string.IsNullOrEmpty(Input) ? false : true;
            }

            Log.Debug($"{nameof(ValidateHandler)}() => Command executed");
        }

        public RelayCommand<Window> Cancel { get { return new RelayCommand<Window>(dialog => CancelHandler(dialog)); } }
        private void CancelHandler(Window dialog)
        {
            Log.Debug($"{nameof(CancelHandler)}() => Command requested ...");

            if (dialog != null)
            {
                dialog.DialogResult = false;
            }

            Log.Debug($"{nameof(CancelHandler)}() => Command executed");
        }
    }
}
