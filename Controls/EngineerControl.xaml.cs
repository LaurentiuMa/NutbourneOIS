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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NutbourneOIS.Controls
{
    /// <summary>
    /// Interaction logic for EngineerControl.xaml
    /// </summary>
    public partial class EngineerControl : UserControl
    {



        public Engineer Engineer
        {
            get { return (Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Engineer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("Engineer", typeof(Engineer), typeof(EngineerControl), new PropertyMetadata(new Engineer (){ ID = 0, Email= "Place@holder.com", FirstName="Andy", Surname="White", Password="sagf546", AccountStatus="Active" }, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EngineerControl control = d as EngineerControl;

            if (control != null)
            {
                control.engineerID.Text = (e.NewValue as Engineer).ID.ToString();
                control.engineerName.Text = (e.NewValue as Engineer).FirstName + " " + (e.NewValue as Engineer).Surname;
                control.email.Text = (e.NewValue as Engineer).Email;
                control.accountType.Text = (e.NewValue as Engineer).AccountType;
                control.accountStatus.Text = (e.NewValue as Engineer).AccountStatus;
            }

        }

        public EngineerControl()
        {
            InitializeComponent();
        }
    }
}
