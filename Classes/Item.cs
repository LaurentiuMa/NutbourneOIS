using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOIS.Classes
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ItemNumber { get; set; }
        public string ItemType { get; set; }
        public string ItemDescription { get; set; }
        public string Engineer { get; set; }
        public string TicketNumber { get; set; }

        public override string ToString()
        {
            return $"{ItemNumber} - {ItemType} - {ItemDescription} - {Engineer} - {TicketNumber}";
        }

    }


}
