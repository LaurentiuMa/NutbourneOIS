using NutbourneOIS.Classes;
using SQLite;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        readonly Engineer engineer;
        readonly List<TextBox> textBoxes = new List<TextBox>();

        public UserDetailsWindow(Engineer engineer)
        {
            InitializeComponent();

            this.engineer = engineer;

            firstNameTextBox.Text = engineer.FirstName;
            surnameTextBox.Text = engineer.Surname;
            emailTextBox.Text = engineer.Email;
            accountTypeComboBox.Text = engineer.AccountType;
            accountStatusComboBox.Text = engineer.AccountStatus;

            textBoxes.Add(firstNameTextBox);
            textBoxes.Add(surnameTextBox);
            textBoxes.Add(emailTextBox);

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
                if (Utilities.IsValidEmail(emailTextBox.Text))
                {
                    engineer.FirstName = firstNameTextBox.Text;
                    engineer.Surname = surnameTextBox.Text;
                    engineer.Email = emailTextBox.Text;
                    engineer.AccountType = accountTypeComboBox.Text;
                    engineer.AccountStatus = accountStatusComboBox.Text;

                    using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                    {
                        connection.CreateTable<Engineer>();
                        connection.Update(engineer);
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("The Email address is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
