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
            Engineer engineer = new Engineer()
            {
                FirstName = firstNameTextBox.Text,
                Surname = surnameTextBox.Text,
                Email = emailTextBox.Text,
                AccountType = accountTypeComboBox.Text,
                AccountStatus = "Active"

            };

            using (SQLiteConnection connection = new SQLiteConnection(App.engineerDatabasePath))
            {
                connection.CreateTable<Engineer>();
                connection.Insert(engineer);
            }

            Close();

        }
    }
}
