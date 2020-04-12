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
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {

        Engineer engineer;

        public UserDetailsWindow(Engineer engineer)
        {
            InitializeComponent();

            this.engineer = engineer;

            firstNameTextBox.Text = engineer.FirstName;
            surnameTextBox.Text = engineer.Surname;
            emailTextBox.Text = engineer.Email;
            accountTypeComboBox.Text = engineer.AccountType;
            accountStatusComboBox.Text = engineer.AccountStatus;

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            engineer.FirstName = firstNameTextBox.Text;
            engineer.Surname = surnameTextBox.Text;
            engineer.Email = emailTextBox.Text;
            engineer.AccountType = accountTypeComboBox.Text;
            engineer.AccountStatus = accountStatusComboBox.Text;

            using (SQLiteConnection connection = new SQLiteConnection(App.engineerDatabasePath))
            {
                connection.CreateTable<Engineer>();
                connection.Update(engineer);
            }

            Close();

        }
    }
}
