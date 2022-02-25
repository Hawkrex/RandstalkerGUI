using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RandstalkerGui.ViewModels.UserControls.SubPresets
{
    public class ItemsCounterViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ObservableCollection<string> Items { get; set; }

        public ObservableCollection<ItemCount> ItemCounters { get; set; }

        private string _itemToAdd;
        public string ItemToAdd
        {
            get
            {
                return _itemToAdd;
            }
            set
            {
                if (_itemToAdd != value)
                {
                    Log.Debug($"{nameof(_itemToAdd)} => <{_itemToAdd}> will change to <{value}>");
                    _itemToAdd = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand AddItem { get { return new RelayCommand(_ => AddItemHandler()); } }
        private void AddItemHandler()
        {
            Log.Debug($"{nameof(AddItemHandler)}() => Command requested ...");

            if (string.IsNullOrEmpty(ItemToAdd))
                throw new InvalidOperationException("Cannot add empty item !");

            ItemCounters.Add(new ItemCount(ItemToAdd, 1));
            Log.Info($"{nameof(AddItemHandler)}() => Added item <{ItemToAdd}> to the list of items");

            if (!Items.Remove(ItemToAdd))
                throw new InvalidOperationException($"The item <{ItemToAdd}> has not been removed from the available items list !");

            ItemToAdd = Items?.FirstOrDefault();
            if (ItemToAdd == null)
                ItemToAdd = string.Empty;

            Log.Debug($"{nameof(AddItemHandler)}() => Command executed");
        }

        public RelayCommand<string> DeleteItem { get { return new RelayCommand<string>(param => DeleteItemHandler(param)); } }
        private void DeleteItemHandler(string name)
        {
            Log.Debug($"{nameof(DeleteItemHandler)}() => Command requested ...");

            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("Cannot remove empty item !");

            if (ItemCounters.Remove(ItemCounters.First(x => x.Name == name)))
                Log.Info($"{nameof(AddItemHandler)}() => Removed item <{name}> from the list of items");
            else
                throw new InvalidOperationException($"The item <{name}> has not been removed from the list of items");

            Items.Add(name);

            ItemToAdd = Items?.FirstOrDefault();
            if (ItemToAdd == null)
                ItemToAdd = string.Empty;

            Log.Debug($"{nameof(DeleteItemHandler)}() => Command executed");
        }

        public ItemsCounterViewModel(Dictionary<string, int> items, ItemDefinitions itemDefinitions)
        {
            Items = new ObservableCollection<string>();

            foreach (var itemDefinition in itemDefinitions.Items)
                Items.Add(itemDefinition.Name);

            ItemCounters = new ObservableCollection<ItemCount>();
            foreach (var item in items)
            {
                ItemCounters.Add(new ItemCount(item.Key, item.Value));
                Items.Remove(item.Key);
            }

            ItemToAdd = Items.First();
        }

        public Dictionary<string, int> ComputePresetInfos()
        {
            Dictionary<string, int> itemsCount = new Dictionary<string, int>();
            foreach (var itemCounter in ItemCounters)
                itemsCount.Add(itemCounter.Name, itemCounter.Count);

            return itemsCount;
        }
    }
}
