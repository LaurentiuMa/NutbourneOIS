using SQLite;

namespace NutbourneOIS.Classes
{
    public class Engineer
    {
        [PrimaryKey, AutoIncrement]
        public int EngineerID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
        public string Salt { get; set; }


    }

    
}
