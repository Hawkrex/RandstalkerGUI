using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace RandstalkerGui.ViewModels.UserControls.SubPresets
{
    public partial class ItemsCounterViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ObservableCollection<string> Items { get; set; }

        public ObservableCollection<ItemCountViewModel> ItemCounters { get; set; }

        [ObservableProperty]
        private string itemToAdd;

        [RelayCommand]
        private void AddItem()
        {
            if (string.IsNullOrEmpty(ItemToAdd))
            {
                throw new InvalidOperationException("Cannot add empty item !");
            }

            ItemCounters.Add(new ItemCountViewModel(ItemToAdd, 1, this));
            Log.Info($"{nameof(AddItem)}() => Added item <{ItemToAdd}> to the list of items");

            if (!Items.Remove(ItemToAdd))
            {
                throw new InvalidOperationException($"The item <{ItemToAdd}> has not been removed from the available items list !");
            }

            ItemToAdd = Items?.FirstOrDefault();
            if (ItemToAdd == null)
            {
                ItemToAdd = string.Empty;
            }
        }

        private void ItemCounters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var itemCount in e.NewItems.OfType<ItemCountViewModel>())
                {
                    itemCount.OnError += setStatusBarMessage;
                    itemCount.OnError += ItemCount_OnError;

                    itemCount.Validate(true);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var itemCount in e.OldItems.OfType<ItemCountViewModel>())
                {
                    itemCount.OnError -= setStatusBarMessage;
                    itemCount.OnError -= ItemCount_OnError;

                    itemCount.Validate(true);
                }
            }
        }

        private void ItemCount_OnError(object sender, StatusBarMessageEventArgs e)
        {
            foreach (var itemCounter in ItemCounters)
            {
                itemCounter.Validate();
            }
        }

        [RelayCommand]
        private void DeleteItem(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Cannot remove empty item !");
            }

            if (ItemCounters.Remove(ItemCounters.First(x => x.Name == name)))
            {
                Log.Info($"{nameof(DeleteItem)}() => Removed item <{name}> from the list of items");
            }
            else
            {
                throw new InvalidOperationException($"The item <{name}> has not been removed from the list of items");
            }

            // Refresh validation state of all items
            ItemCounters.FirstOrDefault()?.Validate(true);

            Items.Add(name);

            ItemToAdd = Items?.FirstOrDefault();
            if (ItemToAdd == null)
            {
                ItemToAdd = string.Empty;
            }
        }

        private EventHandler<StatusBarMessageEventArgs> setStatusBarMessage;

        public ItemsCounterViewModel(Dictionary<string, int> items, ItemDefinitions itemDefinitions, EventHandler<StatusBarMessageEventArgs> setStatusBarMessage = null)
        {
            this.setStatusBarMessage = setStatusBarMessage;

            Items = new ObservableCollection<string>();

            foreach (var itemDefinition in itemDefinitions.Items)
            {
                Items.Add(itemDefinition.Name);
            }

            ItemCounters = new ObservableCollection<ItemCountViewModel>();
            foreach (var item in items)
            {
                var itemCount = new ItemCountViewModel(item.Key, item.Value, this);
                itemCount.OnError += setStatusBarMessage;
                itemCount.OnError += ItemCount_OnError;

                ItemCounters.Add(itemCount);
                Items.Remove(item.Key);
            }

            ItemToAdd = Items.FirstOrDefault();

            ItemCounters.CollectionChanged += ItemCounters_CollectionChanged;
        }

        public Dictionary<string, int> FormatSettings()
        {
            var itemsCount = new Dictionary<string, int>();
            foreach (var itemCounter in ItemCounters)
            {
                itemsCount.Add(itemCounter.Name, itemCounter.Count);
            }

            return itemsCount;
        }
    }
}
