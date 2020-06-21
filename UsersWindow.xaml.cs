using NutbourneOIS.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
            ReadDatabase();
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow();
            newUserWindow.ShowDialog();

            ReadDatabase();
        }

        private void UpdateDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Engineer selectedEngineer = (Engineer)engineersListView.SelectedItem;

            if (selectedEngineer != null)
            {
                UserDetailsWindow userDetailsWindow = new UserDetailsWindow(selectedEngineer);
                userDetailsWindow.ShowDialog();
            }
            ReadDatabase();
        }

        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            Engineer selectedEngineer = (Engineer)engineersListView.SelectedItem;

            if (selectedEngineer != null)
            {
                ResetPasswordWindow resetPasswordWindow = new ResetPasswordWindow(selectedEngineer);
                resetPasswordWindow.ShowDialog();
            }
        }
        
        // Pulls out all of the engineers and their respective details
        public void ReadDatabase() 
        {
            List<Engineer> engineers;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Engineer>();
                engineers = (conn.Table<Engineer>().ToList().ToList());
            }

            if(engineers != null)
            {
                engineersListView.ItemsSource = engineers;
            }

        }

    }
}
