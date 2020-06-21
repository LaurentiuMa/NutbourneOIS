using NOIS.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for ItemDetailsWindow.xaml
    /// </summary>
    public partial class ItemDetailsWindow : Window
    {
        readonly Item item;
        readonly List<TextBox> textBoxes = new List<TextBox>();
        public ItemDetailsWindow(Item item)
        {
            InitializeComponent();

            this.item = item;

            // Fills the appropriate fields with their respective values in the DB
            ticketNumberTextBox.Text = item.TicketNumber.ToString();
            itemTypeTextBox.Text = item.ItemType;
            itemDescriptionTextBox.Text = item.ItemDescription;
            engineerTextBox.Text = item.EngineerID.ToString();
            locationTextBox.Text = item.Location;
            itemStatusComboBox.Text = item.ItemStatus.ToString();

            // Adds all of the text boxes to the list 
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
            // Checks to make sure that no textboxes are left empty
            
            if (!Utilities.InitialiseTextBoxes(textBoxes))
            {
                MessageBox.Show("All boxes must be filled in", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Writes all of the fields back to the DB, can be changed so that only fields that had gotten updated will be written.
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
