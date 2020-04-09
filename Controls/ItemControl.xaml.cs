using NOIS.Classes;
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
    /// Interaction logic for ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {

        public Item Item
        {
            get { return (Item)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }


        // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc... Currently this is only being used for binding
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(Item), typeof(ItemControl), new PropertyMetadata(new Item() { ItemNumber = 0, TicketNumber = "######", ItemType = "Laptop", ItemDescription = "lorem ipsum dolor", Engineer = "Laurentiu Maties" }, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemControl control = d as ItemControl;

            if (control != null)
            {
                control.idTextBlock.Text = ((e.NewValue as Item).ItemNumber).ToString();
                control.ticketNumberTextBlock.Text = (e.NewValue as Item).TicketNumber;
                control.itemTypeTextBlock.Text = (e.NewValue as Item).ItemType;
                control.descriptionTextBlock.Text = (e.NewValue as Item).ItemDescription;
                control.engineerTextBlock.Text = (e.NewValue as Item).Engineer;
            }
        }

        public ItemControl()
        {
            InitializeComponent();
        }
    }
}
