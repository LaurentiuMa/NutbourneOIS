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

            
                //establishes connection
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                {

                    var saltedPassword = (from c in conn.Table<Engineer>()
                                          where c.Email == UsernameField.Text
                                          select new { c.Password, c.Salt, c.AccountType }).SingleOrDefault();

                    if (saltedPassword != null)
                    {
                        if (GenerateSHA256Hash(PasswordField.Password.ToString(), saltedPassword.Salt) == saltedPassword.Password)
                        {
                            MessageBox.Show("Correct credentials, click ok to continue", "Access Granted", MessageBoxButton.OK);
                            MainWindow MainWindow = new MainWindow(saltedPassword.AccountType);
                            MainWindow.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Information);

                    
                }

                }
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
