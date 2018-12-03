using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactuCR.Models
{
    public partial class Users
    {
        public uint IdUser { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nombre completo")]
        [DisplayName("Nombre Completo")]
        
        public string FullName { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de usuario")]
        [DisplayName("Nombre de Usuario")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Debe ingresar un correo válido")]
        [DisplayName("Correo")]
        [EmailAddress]
        public string Email { get; set; }      
        public string About { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public uint Timestamp { get; set; }
        [DisplayName("Último acceso")]
        public uint LastAccess { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }
        [DisplayName("Confirmación de contraseña")]
        [Compare("Pwd", ErrorMessage = "Contraseñas no coinciden, intenta de nuevo.")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public string Avatar { get; set; }
        public string Settings { get; set; }
    }
}
