using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FactuCR.Models.Login
{
    public class Login
    {
        [Required(ErrorMessage = "Debe ingresar un usuario")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }
    }
}