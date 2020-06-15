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
using System.Windows.Interop;
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

        List<TextBox> textBoxes = new List<TextBox>();
        
        public NewItemWindow()
        {
            InitializeComponent();
            // Adds every single object of type TextBox in the textBoxes list.
            textBoxes.Add(ticketNumberTextBox);
            textBoxes.Add(itemTypeTextBox);
            textBoxes.Add(itemDescriptionTextBox);
            textBoxes.Add(engineerTextBox);
        }



        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Foreach loop that checks for empty textboxes.
            bool noEmptyTextBoxes = true;
            foreach (TextBox textBox in textBoxes)
            {
                // If an empty textbox exists, noEmptyTextBoxes is set to false and the loop breaks.
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    noEmptyTextBoxes = false;
                    break;
                }
            }
            // Uses the previous boolean to throw an error if the user has not filled in all of the textboxes.
            if (!noEmptyTextBoxes)
            {
                MessageBox.Show("All boxes must be filled in", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Creates the new Item object that is to be entered into the database.
                Item item = new Item()
                {
                    TicketNumber = int.Parse(ticketNumberTextBox.Text),
                    EngineerID = int.Parse(engineerTextBox.Text),
                    ItemDescription = itemDescriptionTextBox.Text,
                    ItemType = itemTypeTextBox.Text,
                    ItemStatus = "Active",
                    LastUpdated = DateTime.Now,
                    Location = locationTextBox.Text
                };

                try
                {
                    using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                    {
                        connection.CreateTable<Item>();
                        connection.Insert(item);
                        Close();
                    }
                }
                catch (SQLiteException)
                {
                    MessageBox.Show("No Engineer exists with this ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
