using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class User
    {
        public User()
        {
            RecordActivity = new HashSet<RecordActivity>();
        }

        public string EntityIdEntity { get; set; }
        public string Rol { get; set; }
        public string Password { get; set; }

        public Entity EntityIdEntityNavigation { get; set; }
        public ICollection<RecordActivity> RecordActivity { get; set; }
    }
}
