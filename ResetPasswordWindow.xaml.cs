using NutbourneOIS.Classes;
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
    /// Interaction logic for ResetPasswordWindow.xaml
    /// </summary>
    public partial class ResetPasswordWindow : Window
    {

        Engineer engineer;

        public ResetPasswordWindow(Engineer engineer)
        {
            InitializeComponent();
            this.engineer = engineer;
        }

        private void resetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RegexUtilities.ValidatePassword(newPasswordConfirmationTextBox.Password))
                {

                }
                else 
                {
                    MessageBox.Show("The password must be between 8-30 characters, must contain at least ONE number and at least ONE symbol (%$^*)", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("No password entered", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
