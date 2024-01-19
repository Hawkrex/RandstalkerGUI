using RandstalkerGui.Models;
using RandstalkerGui.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RandstalkerGui.ViewModels.UserControls.SubPresets
{
    public class ItemsListViewModel : BaseViewModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ObservableCollection<string> ItemsAvailable { get; set; }

        public ObservableCollection<string> ItemsChosen { get; set; }

        private string itemToAdd;

        public string ItemToAdd
        {
            get
            {
                return itemToAdd;
            }
            set
            {
                if (itemToAdd != value)
                {
                    Log.Debug($"{nameof(itemToAdd)} => <{itemToAdd}> will change to <{value}>");
                    itemToAdd = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand AddItem { get { return new RelayCommand(_ => AddItemHandler()); } }

        private void AddItemHandler()
        {
            Log.Debug($"{nameof(AddItemHandler)}() => Command requested ...");

            if (string.IsNullOrEmpty(ItemToAdd))
            {
                throw new InvalidOperationException("Cannot add empty item !");
            }

            ItemsChosen.Add(ItemToAdd);
            Log.Info($"{nameof(AddItemHandler)}() => Added item <{ItemToAdd}> to the list of items");

            if (!ItemsAvailable.Remove(ItemToAdd))
            {
                throw new InvalidOperationException($"The item <{ItemToAdd}> has not been removed from the available items list !");
            }

            ItemToAdd = ItemsAvailable?.FirstOrDefault();
            if (ItemToAdd == null)
            {
                ItemToAdd = string.Empty;
            }

            Log.Debug($"{nameof(AddItemHandler)}() => Command executed");
        }

        public RelayCommand<string> DeleteItem { get { return new RelayCommand<string>(param => DeleteItemHandler(param)); } }
        private void DeleteItemHandler(string name)
        {
            Log.Debug($"{nameof(DeleteItemHandler)}() => Command requested ...");

            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Cannot remove empty item !");
            }

            if (ItemsChosen.Remove(name))
            {
                Log.Info($"{nameof(AddItemHandler)}() => Removed item <{name}> from the list of items");
            }
            else
            {
                throw new InvalidOperationException($"The item <{name}> has not been removed from the list of items");
            }

            ItemsAvailable.Add(name);

            ItemToAdd = ItemsAvailable?.FirstOrDefault();
            if (ItemToAdd == null)
            {
                ItemToAdd = string.Empty;
            }

            Log.Debug($"{nameof(DeleteItemHandler)}() => Command executed");
        }

        public ItemsListViewModel(IList<string> items, ItemDefinitions itemDefinitions)
        {
            ItemsAvailable = new ObservableCollection<string>();

            foreach (var itemDefinition in itemDefinitions.Items)
            {
                ItemsAvailable.Add(itemDefinition.Name);
            }

            ItemsChosen = new ObservableCollection<string>();
            foreach (var item in items)
            {
                ItemsChosen.Add(item);
                ItemsAvailable.Remove(item);
            }

            ItemToAdd = ItemsAvailable.FirstOrDefault();
        }

        public List<string> FormatSettings()
        {
            return ItemsChosen.ToList();
        }
    }
}
