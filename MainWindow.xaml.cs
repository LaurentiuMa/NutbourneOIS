using NOIS.Classes;
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

        public MainWindow()
        {
            InitializeComponent();

            ReadDatabase();
        }

        private void NewItemButton_Click(object sender, RoutedEventArgs e)
        {
            NewItemWindow newItemWindow = new NewItemWindow();
            newItemWindow.ShowDialog();

            ReadDatabase();

        }



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
                    using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
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

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Item>();
                items = (conn.Table<Item>().ToList()).OrderBy(c => c.ItemNumber).ToList();

            }

            if(items != null)
            {
                itemsListView.ItemsSource = items;
            }
        }

        
    }
}
