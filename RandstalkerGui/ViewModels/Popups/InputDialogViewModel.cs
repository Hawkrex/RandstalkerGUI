using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace RandstalkerGui.ViewModels.Popups
{
    public partial class InputDialogViewModel : ObservableObject
    {
        public string Input { get; set; }

        [RelayCommand]
        private void Validate(Window dialog)
        {
            if (dialog != null)
            {
                dialog.DialogResult = !string.IsNullOrEmpty(Input);
            }
        }

        [RelayCommand]
        private void Cancel(Window dialog)
        {
            if (dialog != null)
            {
                dialog.DialogResult = false;
            }
        }
    }
}
