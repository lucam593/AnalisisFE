﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FactuCR.Models
{
    public partial class Client
    {
       
        public int IdClient { get; set; }//no se muestra

        public int IdType { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public sbyte Status { get; set; }

        public DateTime AdmissionDate { get; set; }

        public IdentificationType IdTypeNavigation { get; set; }
    }
}

