using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace RandstalkerGui.ViewModels.Popups
{
    public class InputDialogViewModel : ObservableObject
    {
        public string Input { get; set; }

        public RelayCommand<Window> Validate => new(ValidateHandler);
        private void ValidateHandler(Window dialog)
        {
            if (dialog != null)
            {
                dialog.DialogResult = !string.IsNullOrEmpty(Input);
            }
        }

        public RelayCommand<Window> Cancel => new(CancelHandler);
        private void CancelHandler(Window dialog)
        {
            if (dialog != null)
            {
                dialog.DialogResult = false;
            }
        }
    }
}
