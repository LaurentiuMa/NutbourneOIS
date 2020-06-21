using NutbourneOIS.Classes;
using SQLite;
using System;
using System.Windows;

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
            if (Utilities.ValidatePassword(passwordTextBox.Password))
            {
                if (passwordTextBox.Password == passwordConfirmationBox.Password && Utilities.IsValidEmail(emailTextBox.Text))
                {
                    /* Once the system has established that the password is sufficient and passes all of the checks, it generates a hash of it.
                     * In this tuple, Item1 is ALWAYS hashedpassword and tuple2 is ALWAYS salt.   
                     */ 
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
                }
                else
                {
                    MessageBox.Show("Passwords do not match or the email is in the incorrect format", 
                                    "Error", 
                                    MessageBoxButton.OK, 
                                    MessageBoxImage.Information);
                }
                Close();
            }
            else
            {
                MessageBox.Show("The password must be between 9-30 characters, must contain at least ONE number and at least ONE symbol (%$^*)",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
