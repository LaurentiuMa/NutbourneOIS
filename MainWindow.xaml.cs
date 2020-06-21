using NOIS.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DateTime dateTimeOfInitialisation;
        readonly List<int> listOfOutdatedItems;
        readonly string combinedStringOfOutdatedItems;
        bool displayActiveItems = true;
        public MainWindow(string accountType)
        {
            InitializeComponent();

            dateTimeOfInitialisation = DateTime.Now;
            listOfOutdatedItems = new List<int>();

            // Ensures that only Admins can manage accounts.
            if (accountType == "User")
            {
                UsersButton.Visibility = Visibility.Hidden;
            }

            ReadDatabase();

            combinedStringOfOutdatedItems = string.Join(", ", listOfOutdatedItems);

            // Prompts the user for the number of items (and their respective ID) that have not been updated in the last 7 days.
            if (listOfOutdatedItems.Count != 0)
            {
                MessageBox.Show("The following items have not been updated for longer than a week and require attention: " + combinedStringOfOutdatedItems,
                    "OutdatedItems",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            CompleteButton.Visibility = Visibility.Hidden;
        }

        private void NewItemButton_Click(object sender, RoutedEventArgs e)
        {
            NewItemWindow newItemWindow = new NewItemWindow();
            newItemWindow.ShowDialog();
            ReadDatabase();
        }

        // Update button, opens ItemDetailsWindow
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

        // Button that allows for the deletion of the selected item.
        // Currently not in use, must be enabled from the constructor in the source code.
        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            Item selectedItem = (Item)itemsListView.SelectedItem;

            if (selectedItem != null)
            {
                MessageBoxResult deleteConfirmation = MessageBox.Show("Are you sure that you want to delete this item?", 
                    "Delete item", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning);
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

        // Subroutine that pulls out all of the relevat items stored in the database (relative to displayActiveItems)
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
                // Loops through every single item and checks if there are any items that have not updated within the last 7 days. If so, adds them to the list.
                foreach (Item item in items)
                {
                    dateTimeOfItem = item.LastUpdated.AddDays(7);
                    if (DateTime.Compare(dateTimeOfItem, dateTimeOfInitialisation) <= 0)
                    {
                        listOfOutdatedItems.Add(item.ItemNumber);
                    }
                }
            }
            if (items != null)
            {
                itemsListView.ItemsSource = items;
            }
        }

        // Opens a window displaying all of the registered users.
        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow usersWindow = new UsersWindow();
            usersWindow.ShowDialog();
        }

        //Allows the user to see any inactive items.
        private void ItemActivityButton_Click(object sender, RoutedEventArgs e)
        {
            displayActiveItems ^= true;
            ReadDatabase();
            DisplayAppropriateActivityLabel();
        }

        // Subroutine that pulls all of the items respective to the item status.
        public List<Item> PullItems(SQLiteConnection connection, string activity)
        {
            connection.CreateTable<Item>();
            var items = (from c in connection.Table<Item>()
                         where c.ItemStatus == activity
                         select c).ToList();
            return items;
        }

        // Ensures that the text written on the first button matches the function.
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
