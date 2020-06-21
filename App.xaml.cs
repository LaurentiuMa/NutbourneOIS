using System;
using System.Windows;

namespace NutbourneOIS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Database path, this will be changed once implemented at work
        private static readonly string DatabaseName = "FinalNutbourneDB.db";
        private static readonly string DatabaseFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string DatabasePath = System.IO.Path.Combine(DatabaseFolderPath, DatabaseName);
    }
}
