using RandstalkerGui.ViewModels.UserControls;
using System.Windows.Controls;

namespace RandstalkerGui.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour PresetView.xaml
    /// </summary>
    public partial class PresetView : UserControl
    {
        public PresetView()
        {
            InitializeComponent();

            ((PresetViewModel)DataContext).StartingLife = 0;
        }
    }
}
