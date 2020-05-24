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
        //private static readonly string itemDatabaseName = "Items.db";
        //private static readonly string itemDatabaseFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //public static string itemDatabasePath = System.IO.Path.Combine(itemDatabaseFolderPath, itemDatabaseName);

        //private static readonly string engineerDatabaseName = "Users.db";
        //private static readonly string engineerDatabaseFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //public static string engineerDatabasePath = System.IO.Path.Combine(engineerDatabaseFolderPath, engineerDatabaseName);

        private static readonly string DatabaseName = "FinalNutbourneDB.db";
        private static readonly string DatabaseFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string DatabasePath = System.IO.Path.Combine(DatabaseFolderPath, DatabaseName);

    }
}
