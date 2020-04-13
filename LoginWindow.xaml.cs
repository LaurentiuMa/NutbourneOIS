using NutbourneOIS.Classes;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        List<Engineer> engineers;

        public LoginWindow()
        {
            InitializeComponent();

            engineers = new List<Engineer>();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) 
            { 
                DragMove(); 
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {


            string username = UsernameField.Text;
            string password = PasswordField.Password.ToString();
            string hashedPasswordString = "";
            string saltString = "";

            using (SQLiteConnection conn = new SQLiteConnection(App.engineerDatabasePath))
            {
                engineers = conn.Table<Engineer>().ToList();

                var hashedpasswordlist = from c in engineers
                                     where c.Email == username
                                     select c.Password;

                foreach (var item in hashedpasswordlist)
                {
                    hashedPasswordString = item;
                }

                var saltlist = from c in engineers
                               where c.Email == username
                               select c.Salt;
                foreach (var item in saltlist)
                {
                    saltString = item;
                }

                if ( GenerateSHA256Hash(password, saltString) == hashedPasswordString) 
                {
                    MessageBoxResult deleteConfirmation = MessageBox.Show("IT WORKS!", "Delete item", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }

            }

            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }


        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
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

    }
}
