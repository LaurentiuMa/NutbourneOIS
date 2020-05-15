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
            if (passwordTextBox.Password == passwordConfirmationBox.Password && RegexUtilities.IsValidEmail(emailTextBox.Text))
            {
                string salt = CreateSalt(10);
                string hashedpassword = GenerateSHA256Hash(passwordTextBox.Password.ToString(), salt);


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

                using (SQLiteConnection connection = new SQLiteConnection(App.engineerDatabasePath))
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

        public string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashtring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashtring.ComputeHash(bytes);

            return ByteArrayToHexString(hash);
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);

            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        //public static byte[] HexStringToByteArray(string hex)
        //{
        //    int NumberChars = hex.Length;
        //    byte[] bytes = new byte[NumberChars / 2];
        //    for (int i = 0; i < NumberChars; i+= 2)
        //    {
        //        bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        //    }

        //    return bytes;
        //}


    }
}
