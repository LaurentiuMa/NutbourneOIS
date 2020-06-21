using SQLite;
using System;

namespace NOIS.Classes
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ItemNumber { get; set; }
        public string ItemType { get; set; }
        public string ItemDescription { get; set; }
        public int EngineerID { get; set; }
        public string ItemStatus { get; set; }
        public DateTime LastUpdated { get; set; }
        public int TicketNumber { get; set; }
        public string Location { get; set; }
    }


}
