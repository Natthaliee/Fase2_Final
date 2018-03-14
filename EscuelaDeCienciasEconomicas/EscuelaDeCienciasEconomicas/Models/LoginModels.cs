using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace EscuelaDeCienciasEconomicas.Models
{
    [Table("auth_user", Schema = "publico")]
    public class Login
    {
        [Key]
        public int id { get; set; }

        //[Required(ErrorMessage = " Debe ingresar un usuario")]
        //[StringLength(150, ErrorMessage = " El usuario no pueden tener mas de 15 caracteres")]
        public String username { get; set; }

        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,15})$", ErrorMessage = " * No válida")]
        public String password { get; set; }

        //[Required(ErrorMessage = " Debe ingresar un correo valido")]
        public String email { get; set; }

    }
}