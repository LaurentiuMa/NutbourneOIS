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
using System.Windows.Shapes;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for ItemDetailsWindow.xaml
    /// </summary>
    public partial class ItemDetailsWindow : Window
    {

        Item item;

        public ItemDetailsWindow(Item item)
        {
            InitializeComponent();

            this.item = item;

            ticketNumberTextBox.Text = item.TicketNumber;
            itemTypeTextBox.Text = item.ItemType;
            itemDescriptionTextBox.Text = item.ItemDescription;
            engineerTextBox.Text = item.Engineer;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            item.TicketNumber = ticketNumberTextBox.Text;
            item.ItemType = itemTypeTextBox.Text;
            item.ItemDescription = itemDescriptionTextBox.Text;
            item.Engineer = engineerTextBox.Text;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Item>();
                connection.Update(item);
            }

            Close();
        }
    }
}
