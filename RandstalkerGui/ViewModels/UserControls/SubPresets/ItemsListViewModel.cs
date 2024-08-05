using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RandstalkerGui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RandstalkerGui.ViewModels.UserControls.SubPresets
{
    public partial class ItemsListViewModel : ObservableObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ObservableCollection<string> ItemsAvailable { get; set; }

        public ObservableCollection<string> ItemsChosen { get; set; }

        [ObservableProperty]
        private string itemToAdd;

        [RelayCommand]
        private void AddItem()
        {
            if (string.IsNullOrEmpty(ItemToAdd))
            {
                throw new InvalidOperationException("Cannot add empty item !");
            }

            ItemsChosen.Add(ItemToAdd);
            Log.Info($"{nameof(AddItem)}() => Added item <{ItemToAdd}> to the list of items");

            if (!ItemsAvailable.Remove(ItemToAdd))
            {
                throw new InvalidOperationException($"The item <{ItemToAdd}> has not been removed from the available items list !");
            }

            ItemToAdd = ItemsAvailable?.FirstOrDefault();
            if (ItemToAdd == null)
            {
                ItemToAdd = string.Empty;
            }
        }

        [RelayCommand]
        private void DeleteItem(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Cannot remove empty item !");
            }

            if (ItemsChosen.Remove(name))
            {
                Log.Info($"{nameof(DeleteItem)}() => Removed item <{name}> from the list of items");
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
