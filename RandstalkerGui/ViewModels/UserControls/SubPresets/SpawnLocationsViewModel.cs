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

        private bool _massan;
        public bool Massan
        {
            get
            {
                return _massan;
            }
            set
            {
                if (_massan != value)
                {
                    Log.Debug($"{nameof(_massan)} => <{_massan}> will change to <{value}>");
                    _massan = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _gumi;
        public bool Gumi
        {
            get
            {
                return _gumi;
            }
            set
            {
                if (_gumi != value)
                {
                    Log.Debug($"{nameof(_gumi)} => <{_gumi}> will change to <{value}>");
                    _gumi = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _kado;
        public bool Kado
        {
            get
            {
                return _kado;
            }
            set
            {
                if (_kado != value)
                {
                    Log.Debug($"{nameof(_kado)} => <{_kado}> will change to <{value}>");
                    _kado = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _waterfall;
        public bool Waterfall
        {
            get
            {
                return _waterfall;
            }
            set
            {
                if (_waterfall != value)
                {
                    Log.Debug($"{nameof(_waterfall)} => <{_waterfall}> will change to <{value}>");
                    _waterfall = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _ryuma;
        public bool Ryuma
        {
            get
            {
                return _ryuma;
            }
            set
            {
                if (_ryuma != value)
                {
                    Log.Debug($"{nameof(_ryuma)} => <{_ryuma}> will change to <{value}>");
                    _ryuma = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _mercator;
        public bool Mercator
        {
            get
            {
                return _mercator;
            }
            set
            {
                if (_mercator != value)
                {
                    Log.Debug($"{nameof(_mercator)} => <{_mercator}> will change to <{value}>");
                    _mercator = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _verla;
        public bool Verla
        {
            get
            {
                return _verla;
            }
            set
            {
                if (_verla != value)
                {
                    Log.Debug($"{nameof(_verla)} => <{_verla}> will change to <{value}>");
                    _verla = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _greenmaze;
        public bool Greenmaze
        {
            get
            {
                return _greenmaze;
            }
            set
            {
                if (_greenmaze != value)
                {
                    Log.Debug($"{nameof(_greenmaze)} => <{_greenmaze}> will change to <{value}>");
                    _greenmaze = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _destel;
        public bool Destel
        {
            get
            {
                return _destel;
            }
            set
            {
                if (_destel != value)
                {
                    Log.Debug($"{nameof(_destel)} => <{_destel}> will change to <{value}>");
                    _destel = value;
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
