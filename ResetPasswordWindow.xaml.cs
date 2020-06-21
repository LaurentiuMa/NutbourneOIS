using NutbourneOIS.Classes;
using SQLite;
using System;
using System.Windows;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for ResetPasswordWindow.xaml
    /// </summary>
    public partial class ResetPasswordWindow : Window
    {
        readonly Engineer engineer;

        public ResetPasswordWindow(Engineer engineer)
        {
            InitializeComponent();
            this.engineer = engineer;
        }

        private void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Checks to see if the password is valid and makes a hash of it
                if (Utilities.ValidatePassword(newPasswordConfirmationTextBox.Password))
                {
                    Tuple<string, string> passwordSaltPair = Utilities.GenerateSHA256Hash(newPasswordConfirmationTextBox.Password, 10);
                    string hashedpassword = passwordSaltPair.Item1;
                    string salt = passwordSaltPair.Item2;

                    // Updates the passwrod and hash in the DB
                    engineer.Password = hashedpassword;
                    engineer.Salt = salt;

                        using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
                        {
                            connection.CreateTable<Engineer>();
                            connection.Update(engineer);
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
            catch (ArgumentNullException)
            {
                MessageBox.Show("No password entered", 
                                "Error", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Information);
            }
        }
    }
}
