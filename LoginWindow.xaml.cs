using NutbourneOIS.Classes;
using SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

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
            LogMeIn();
        }



        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LogMeIn();
            }
        }

        private void LogMeIn()
        {
            //Establishes connection with the database.
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                //Queries the Database for the password hash, salt and account type associated with the email entered by the user.
                var saltedPassword = (from c in conn.Table<Engineer>()
                                      where c.Email == UsernameField.Text
                                      select new { c.Password, c.Salt, c.AccountType }).SingleOrDefault();

                //Ensures that the field entered is not empty to avoid unnecessary computation or random errors
                if (saltedPassword != null)
                {
                    //Uses the public class Utilities.cs to call the validate method that matches the password entered against the user's salt and hashes it all together. 
                    if (Utilities.ValidateSHA256Hash(PasswordField.Password.ToString(), saltedPassword.Salt) == saltedPassword.Password)
                    {
                        MainWindow MainWindow = new MainWindow(saltedPassword.AccountType);
                        MainWindow.Show();
                        this.Close();
                    }
                    else 
                    {
                        MessageBox.Show("Incorrect credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Account does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Information);


                }

            }
        }


    }
}
