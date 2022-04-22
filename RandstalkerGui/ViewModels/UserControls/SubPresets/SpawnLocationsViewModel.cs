using RandstalkerGui.Tools;
using System.Collections.Generic;

namespace RandstalkerGui.ViewModels.UserControls.SubPresets
{
    public class SpawnLocationsViewModel : BaseViewModel
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
            get
            {
                return massan;

            }
            set
            {
                if (massan != value)

                {
                    Log.Debug($"{nameof(massan)} => <{massan}> will change to <{value}>");

                    massan = value;

                    OnPropertyChanged();
                }
            }
        }

        private bool gumi;

        public bool Gumi
        {
            get
            {
                return gumi;

            }
            set
            {
                if (gumi != value)

                {
                    Log.Debug($"{nameof(gumi)} => <{gumi}> will change to <{value}>");

                    gumi = value;

                    OnPropertyChanged();
                }
            }
        }

        private bool kado;

        public bool Kado
        {
            get
            {
                return kado;

            }
            set
            {
                if (kado != value)

                {
                    Log.Debug($"{nameof(kado)} => <{kado}> will change to <{value}>");

                    kado = value;

                    OnPropertyChanged();
                }
            }
        }

        private bool waterfall;

        public bool Waterfall
        {
            get
            {
                return waterfall;

            }
            set
            {
                if (waterfall != value)

                {
                    Log.Debug($"{nameof(waterfall)} => <{waterfall}> will change to <{value}>");

                    waterfall = value;

                    OnPropertyChanged();
                }
            }
        }

        private bool ryuma;

        public bool Ryuma
        {
            get
            {
                return ryuma;

            }
            set
            {
                if (ryuma != value)

                {
                    Log.Debug($"{nameof(ryuma)} => <{ryuma}> will change to <{value}>");

                    ryuma = value;

                    OnPropertyChanged();
                }
            }
        }

        private bool mercator;

        public bool Mercator
        {
            get
            {
                return mercator;

            }
            set
            {
                if (mercator != value)

                {
                    Log.Debug($"{nameof(mercator)} => <{mercator}> will change to <{value}>");

                    mercator = value;

                    OnPropertyChanged();
                }
            }
        }

        private bool verla;

        public bool Verla
        {
            get
            {
                return verla;

            }
            set
            {
                if (verla != value)

                {
                    Log.Debug($"{nameof(verla)} => <{verla}> will change to <{value}>");

                    verla = value;

                    OnPropertyChanged();
                }
            }
        }

        private bool greenmaze;

        public bool Greenmaze
        {
            get
            {
                return greenmaze;

            }
            set
            {
                if (greenmaze != value)

                {
                    Log.Debug($"{nameof(greenmaze)} => <{greenmaze}> will change to <{value}>");

                    greenmaze = value;

                    OnPropertyChanged();
                }
            }
        }

        private bool destel;

        public bool Destel
        {
            get
            {
                return destel;

            }
            set
            {
                if (destel != value)

                {
                    Log.Debug($"{nameof(destel)} => <{destel}> will change to <{value}>");

                    destel = value;

                    OnPropertyChanged();
                }
            }
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

        public List<string> ComputePresetInfos()
        {
            List<string> spawnLocations = new List<string>();

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
