using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace RandstalkerGui.ViewModels.UserControls.SubPresets
{
    public class SpawnLocationsViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string MassanName = "massan";
        private const string GumiName = "gumi";
        private const string KadoName = "kado";
        private const string WaterfallName = "waterfall";
        private const string RyumaName = "ryuma";
        private const string MercatorName = "mercator";
        private const string VerlaName = "verla";
        private const string GreenmazeName = "greenmaze";
        private const string DestelName = "destel";

        private bool massan;

        public bool Massan
        {
            get => massan;
            set => SetProperty(ref massan, value);
        }

        private bool gumi;

        public bool Gumi
        {
            get => gumi;
            set => SetProperty(ref gumi, value);
        }

        private bool kado;

        public bool Kado
        {
            get => kado;
            set => SetProperty(ref kado, value);
        }

        private bool waterfall;

        public bool Waterfall
        {
            get => waterfall;
            set => SetProperty(ref waterfall, value);

        }

        private bool ryuma;

        public bool Ryuma
        {
            get => ryuma;
            set => SetProperty(ref ryuma, value);
        }

        private bool mercator;

        public bool Mercator
        {
            get => mercator;
            set => SetProperty(ref mercator, value);
        }

        private bool verla;

        public bool Verla
        {
            get => verla;
            set => SetProperty(ref verla, value);
        }

        private bool greenmaze;

        public bool Greenmaze
        {
            get => greenmaze;
            set => SetProperty(ref greenmaze, value);
        }

        private bool destel;

        public bool Destel
        {
            get => destel;
            set => SetProperty(ref destel, value);
        }

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
