using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System;
using System.Linq;

namespace RandstalkerGui.ViewModels.UserControls.SubPresets
{
    public class ItemCountViewModel : ValidationViewModel
    {
        private readonly ItemsCounterViewModel parent;
        private readonly ItemCount item;

        public string Name => item.Name;

        public int Count
        {
            get => item.Count;
            set
            {
                if (SetProperty(item.Count, 0, item, (i, c) => i.Count = c))
                {
                    Validate(true);
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
                error = (string)App.Instance.TryFindResource("ItemsDistibutionError");
                AddError(Name, error); // Little cheat as we don't use the propertyName (which is Count) but rather the name of the item
            }

            if (notify)
            {
                OnError?.Invoke(this, new StatusBarMessageEventArgs() { Message = error, Sender = "ItemCount" });
            }
        }
    }
}
