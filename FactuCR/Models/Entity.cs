using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FactuCR.Models
{
    public partial class Entity
    {
        public Entity()
        {
            Address = new HashSet<Address>();
            TelephoneContact = new HashSet<TelephoneContact>();
        }

        public string IdEntity { get; set; }
        public string Name { get; set; }
        public string TypeId { get; set; }
        public string Email { get; set; }

        public Client Client { get; set; }
        public CompanyInformation CompanyInformation { get; set; }
        public Provider Provider { get; set; }
        public User User { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<TelephoneContact> TelephoneContact { get; set; }

    }
}
