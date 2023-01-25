using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using RandstalkerGui.ValidationRules;
using System;
using System.Linq;

namespace RandstalkerGui.ViewModels.UserControls.SubPresets
{
    public class ItemCountViewModel : ValidationViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ItemsCounterViewModel parent;
        private ItemCount item;

        public string Name
        {
            get
            {
                return item.Name;
            }
        }

        public int Count
        {
            get
            {
                return item.Count;
            }
            set
            {
                if (item.Count != value)
                {
                    Log.Debug($"{nameof(item.Count)} => <{item.Count}> will change to <{value}>");
                    item.Count = value;

                    Validate(true);
                    OnPropertyChanged();
                }
            }
        }

        public ItemCountViewModel(string name, int count, ItemsCounterViewModel parent)
        {
            this.parent = parent;
            item = new ItemCount(name, count);
        }

        public event EventHandler<StatusBarMessageEventArgs> OnError;

        public void Validate(bool notify = false)
        {
            string error = string.Empty;

            ClearErrors(Name);

            if (parent.ItemCounters.Sum(vm => vm.Count) > 291)
            {
                error = (string) App.Instance.TryFindResource("ItemsDistibutionError");
                AddError(Name, error); // Little cheat as we don't use the propertyName (which is Count) but rather the name of the item
            }

            if(notify)
            {
                OnError?.Invoke(this, new StatusBarMessageEventArgs() { Message = error, Sender = "ItemCount" });
            }
        }
    }
}
