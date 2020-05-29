using NutbourneOIS.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        public NewUserWindow()
        {
            InitializeComponent();
        }

        

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordTextBox.Password == passwordConfirmationBox.Password && Utilities.IsValidEmail(emailTextBox.Text))
            {
                Tuple<string, string> passwordSaltPair = Utilities.GenerateSHA256Hash(passwordTextBox.Password, 10);
                string hashedpassword = passwordSaltPair.Item1;
                string salt = passwordSaltPair.Item2;

                Engineer engineer = new Engineer()
                {
                    FirstName = firstNameTextBox.Text,
                    Surname = surnameTextBox.Text,
                    Email = emailTextBox.Text,
                    AccountType = accountTypeComboBox.Text,
                    AccountStatus = "Active",
                    Password = hashedpassword,
                    Salt = salt


                };

                using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                {
                    connection.CreateTable<Engineer>();
                    connection.Insert(engineer);
                }

                Close();
            }
            else 
            {
                MessageBox.Show("Passwords do not match or the email is in the incorrect format", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
