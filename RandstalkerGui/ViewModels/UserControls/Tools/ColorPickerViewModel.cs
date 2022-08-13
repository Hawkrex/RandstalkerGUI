using RandstalkerGui.Tools;

namespace RandstalkerGui.ViewModels.UserControls.Tools
{
    public class ColorPickerViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private char redValue;
        public char RedValue
        {
            get
            {
                return redValue;
            }
            set
            {
                if (redValue != value)
                {
                    Log.Debug($"{nameof(RedValue)} => <{RedValue}> will change to <{value}>");
                    redValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private char greenValue;
        public char GreenValue
        {
            get
            {
                return greenValue;
            }
            set
            {
                if (greenValue != value)
                {
                    Log.Debug($"{nameof(GreenValue)} => <{GreenValue}> will change to <{value}>");
                    greenValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private char blueValue;
        public char BlueValue
        {
            get
            {
                return blueValue;
            }
            set
            {
                if (blueValue != value)
                {
                    Log.Debug($"{nameof(BlueValue)} => <{BlueValue}> will change to <{value}>");
                    blueValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public ColorPickerViewModel(string color)
        {
            RedValue = color.ToCharArray()[1];
            GreenValue = color.ToCharArray()[2];
            BlueValue = color.ToCharArray()[3];
        }

        public string FormatSettings()
        {
            return $"#{RedValue}{GreenValue}{BlueValue}";
        }
    }
}