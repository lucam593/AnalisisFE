using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Client
    {
        public Client()
        {
            Bill = new HashSet<Bill>();
        }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ClientType { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string EntityIdEntity { get; set; }

        public Entity EntityIdEntityNavigation { get; set; }
        public ICollection<Bill> Bill { get; set; }
    }
}