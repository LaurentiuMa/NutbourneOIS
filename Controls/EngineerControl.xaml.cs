using NutbourneOIS.Classes;
using System.Windows;
using System.Windows.Controls;

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

        // Using a DependencyProperty as the backing store for Engineer. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("Engineer", typeof(Engineer), typeof(EngineerControl), new PropertyMetadata(new Engineer (){ EngineerID = 0, Email= "Place@holder.com", FirstName="Andy", Surname="White", Password="sagf546", AccountStatus="Active" }, SetText));

        // Sets the source for every single field in the template.
        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EngineerControl control)
            {
                control.engineerID.Text = (e.NewValue as Engineer).EngineerID.ToString();
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
