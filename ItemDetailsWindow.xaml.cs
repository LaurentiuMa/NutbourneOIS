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
        List<TextBox> textBoxes = new List<TextBox>();
        public ItemDetailsWindow(Item item)
        {
            InitializeComponent();

            this.item = item;

            ticketNumberTextBox.Text = item.TicketNumber.ToString();
            itemTypeTextBox.Text = item.ItemType;
            itemDescriptionTextBox.Text = item.ItemDescription;
            engineerTextBox.Text = item.EngineerID.ToString();
            locationTextBox.Text = item.Location;
            itemStatusComboBox.Text = item.ItemStatus.ToString();

            textBoxes.Add(ticketNumberTextBox);
            textBoxes.Add(itemTypeTextBox);
            textBoxes.Add(itemDescriptionTextBox);
            textBoxes.Add(engineerTextBox);

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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            bool noEmptyTextBoxes = true;
            foreach (TextBox textBox in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    noEmptyTextBoxes = false;
                    break;
                }
            }
            if (!noEmptyTextBoxes)
            {
                MessageBox.Show("All boxes must be filled in", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                item.TicketNumber = int.Parse(ticketNumberTextBox.Text);
                item.ItemType = itemTypeTextBox.Text;
                item.ItemDescription = itemDescriptionTextBox.Text;
                item.EngineerID = int.Parse(engineerTextBox.Text);
                item.LastUpdated = DateTime.Now;
                item.Location = locationTextBox.Text;
                item.ItemStatus = itemStatusComboBox.Text;

                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Item>();
                    connection.Update(item);
                }

                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
