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
    /// Interaction logic for NewItemWindow.xaml
    /// </summary>
    public partial class NewItemWindow : Window
    {


        public NewItemWindow()
        {
            InitializeComponent();
        }

       

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item()
            {
                TicketNumber = int.Parse(ticketNumberTextBox.Text),
                Engineer = engineerTextBox.Text,
                ItemDescription = itemDescriptionTextBox.Text,
                ItemType = itemTypeTextBox.Text
            };


            using (SQLiteConnection connection = new SQLiteConnection(App.itemDatabasePath))
            {
                connection.CreateTable<Item>();
                connection.Insert(item);
            }

            Close();
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }


        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);
        }
    }
}
