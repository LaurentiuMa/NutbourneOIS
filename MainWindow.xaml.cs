﻿using NOIS.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DateTime dateTimeOfInitialisation;
        List<int> listOfOutdatedItems;
        string combinedStringOfOutdatedItems;
        bool displayActiveItems = true;
        public MainWindow(string accountType)
        {
            InitializeComponent();

            dateTimeOfInitialisation = DateTime.Now;
            listOfOutdatedItems = new List<int>();

            if (accountType == "User")
            {
                UsersButton.Visibility = Visibility.Hidden;
            }

            ReadDatabase();

            int listSize = listOfOutdatedItems.Count;
            combinedStringOfOutdatedItems = string.Join(", ", listOfOutdatedItems);

            if (listSize != 0) 
            {
                MessageBox.Show("The following items have not been updated for longer than a week and require attention: " + combinedStringOfOutdatedItems, 
                                "OutdatedItems", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Information);
            }

        }

        private void NewItemButton_Click(object sender, RoutedEventArgs e)
        {
            NewItemWindow newItemWindow = new NewItemWindow();
            newItemWindow.ShowDialog();

            ReadDatabase();

        }


        // Update button
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Item selectedItem = (Item)itemsListView.SelectedItem;

            if (selectedItem != null)
            {
                ItemDetailsWindow ItemDetailsWindow = new ItemDetailsWindow(selectedItem);
                ItemDetailsWindow.ShowDialog();
            }
            ReadDatabase();
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            Item selectedItem = (Item)itemsListView.SelectedItem;

            if (selectedItem != null)
            {
                 MessageBoxResult deleteConfirmation = MessageBox.Show("Are you sure that you want to delete this item?", "Delete item", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (deleteConfirmation == MessageBoxResult.Yes)
                {
                    using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                    {
                        connection.CreateTable<Item>();
                        connection.Delete(selectedItem);
                        ReadDatabase();
                    }
                }
            }
        }

        public void ReadDatabase()
        {
            List<Item> items;
            DateTime dateTimeOfItem;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                if (displayActiveItems)
                {
                   items = PullItems(conn, "Active");
                }
                else 
                {
                   items = PullItems(conn, "Inactive");
                }

                foreach (Item item in items)
                {
                    dateTimeOfItem = item.LastUpdated.AddDays(7);
                    if (DateTime.Compare(dateTimeOfItem, dateTimeOfInitialisation) <= 0)
                    {
                        listOfOutdatedItems.Add(item.ItemNumber);
                    }
                }
            }

            if(items != null)
            {
                itemsListView.ItemsSource = items;
            }
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow usersWindow = new UsersWindow();
            usersWindow.ShowDialog();
        }

        private void ItemActivityButton_Click(object sender, RoutedEventArgs e)
        {
            displayActiveItems ^= true;
            ReadDatabase();
            DisplayAppropriateActivityLabel();
        }

        public List<Item> PullItems(SQLiteConnection connection, string activity) 
        {
            connection.CreateTable<Item>();
            var items = (from c in connection.Table<Item>()
                     where c.ItemStatus == activity
                     select c).ToList();
            return items;
        }

        private void DisplayAppropriateActivityLabel()
        {
            if (displayActiveItems)
            {
                ItemActivityButton.Content = "Inctive items";
            }
            else
            {
                ItemActivityButton.Content = "Active items";
            }
        }

    }
}
