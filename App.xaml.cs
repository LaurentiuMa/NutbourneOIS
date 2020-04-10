using NOIS.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string itemDatabaseName = "Items.db";
        public static string itemFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string itemDatabasePath = System.IO.Path.Combine(itemFolderPath, itemDatabaseName);

        public static string userDatabaseName = "Users.db";
        public static string userDatabaseFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string userDatabasePath = System.IO.Path.Combine(userDatabaseFolderPath, userDatabaseName);
    }
}
