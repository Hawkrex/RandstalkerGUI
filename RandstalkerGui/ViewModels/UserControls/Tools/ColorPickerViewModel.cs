using CommunityToolkit.Mvvm.ComponentModel;

namespace RandstalkerGui.ViewModels.UserControls.Tools
{
    public partial class ColorPickerViewModel : ObservableObject
    {
        [ObservableProperty]
        private char redValue;

        [ObservableProperty]
        private char greenValue;

        [ObservableProperty]
        private char blueValue;

        public ColorPickerViewModel(string color)
        {
            RedValue = color.ToCharArray()[1];
            GreenValue = color.ToCharArray()[2];
            BlueValue = color.ToCharArray()[3];
        }

        public string FormatSettings() => $"#{RedValue}{GreenValue}{BlueValue}";
    }
}