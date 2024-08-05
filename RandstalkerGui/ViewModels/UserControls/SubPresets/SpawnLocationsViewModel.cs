using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace RandstalkerGui.ViewModels.UserControls.SubPresets
{
    public partial class SpawnLocationsViewModel : ObservableObject
    {
        private const string MassanName = "massan";
        private const string GumiName = "gumi";
        private const string KadoName = "kado";
        private const string WaterfallName = "waterfall";
        private const string RyumaName = "ryuma";
        private const string MercatorName = "mercator";
        private const string VerlaName = "verla";
        private const string GreenmazeName = "greenmaze";
        private const string DestelName = "destel";

        [ObservableProperty]
        private bool massan;

        [ObservableProperty]
        private bool gumi;

        [ObservableProperty]
        private bool kado;

        [ObservableProperty]
        private bool waterfall;

        [ObservableProperty]
        private bool ryuma;

        [ObservableProperty]
        private bool mercator;

        [ObservableProperty]
        private bool verla;

        [ObservableProperty]
        private bool greenmaze;

        [ObservableProperty]
        private bool destel;

        public SpawnLocationsViewModel(List<string> spawnLocations)
        {
            foreach (string spawnLocation in spawnLocations)
            {
                switch (spawnLocation)
                {
                    case MassanName:
                        Massan = true;
                        break;
                    case GumiName:
                        Gumi = true;
                        break;
                    case KadoName:
                        Kado = true;
                        break;
                    case WaterfallName:
                        Waterfall = true;
                        break;
                    case RyumaName:
                        Ryuma = true;
                        break;
                    case MercatorName:
                        Mercator = true;
                        break;
                    case VerlaName:
                        Verla = true;
                        break;
                    case GreenmazeName:
                        Greenmaze = true;
                        break;
                    case DestelName:
                        Destel = true;
                        break;
                }
            }
        }

        public List<string> FormatSettings()
        {
            var spawnLocations = new List<string>();

            if (Massan) spawnLocations.Add(MassanName);
            if (Gumi) spawnLocations.Add(GumiName);
            if (Kado) spawnLocations.Add(KadoName);
            if (Waterfall) spawnLocations.Add(WaterfallName);
            if (Ryuma) spawnLocations.Add(RyumaName);
            if (Mercator) spawnLocations.Add(MercatorName);
            if (Verla) spawnLocations.Add(VerlaName);
            if (Greenmaze) spawnLocations.Add(GreenmazeName);
            if (Destel) spawnLocations.Add(DestelName);

            return spawnLocations;
        }
    }
}
