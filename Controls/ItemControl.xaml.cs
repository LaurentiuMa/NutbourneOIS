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
            DependencyProperty.Register("Item", typeof(Item), typeof(ItemControl), new PropertyMetadata(new Item() { ItemNumber = 0, TicketNumber = 0, ItemType = "Laptop", ItemDescription = "lorem ipsum dolor", EngineerID = 0 }, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ItemControl control)
            {
                control.idTextBlock.Text = "Item ID: " + ((e.NewValue as Item).ItemNumber).ToString();
                control.ticketNumberTextBlock.Text = "Ticket #" + (e.NewValue as Item).TicketNumber;
                control.itemTypeTextBlock.Text = "Item Type: " + (e.NewValue as Item).ItemType;
                control.descriptionTextBlock.Text = "Description: " + (e.NewValue as Item).ItemDescription;
                control.engineerTextBlock.Text = "Engineer ID: " + ((e.NewValue as Item).EngineerID).ToString();
                control.dateTextBlock.Text = "Last Update: " + ((e.NewValue as Item).LastUpdated).ToString();
                control.locationTextBlock.Text = "Location: " + ((e.NewValue as Item).Location);
            }
        }

        public ItemControl()
        {
            InitializeComponent();
        }
    }
}
