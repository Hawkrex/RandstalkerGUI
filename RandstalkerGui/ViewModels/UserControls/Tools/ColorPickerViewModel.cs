using CommunityToolkit.Mvvm.ComponentModel;

namespace RandstalkerGui.ViewModels.UserControls.Tools
{
    public class ColorPickerViewModel : ObservableObject
    {
        private char redValue;
        public char RedValue
        {
            get => redValue;
            set => SetProperty(ref redValue, value);
        }

        private char greenValue;
        public char GreenValue
        {
            get => greenValue;
            set => SetProperty(ref greenValue, value);
        }

        private char blueValue;
        public char BlueValue
        {
            get => blueValue;
            set => SetProperty(ref blueValue, value);
        }

        public ColorPickerViewModel(string color)
        {
            RedValue = color.ToCharArray()[1];
            GreenValue = color.ToCharArray()[2];
            BlueValue = color.ToCharArray()[3];
        }

        public string FormatSettings() => $"#{RedValue}{GreenValue}{BlueValue}";
    }
}