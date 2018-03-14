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
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = " Debe ingresar una contraseña válida")]
        //[StringLength(15, ErrorMessage = " La contraseña debe tener entre 8 y 15 carácteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,15})$", ErrorMessage = " * No válida")]
        public String password { get; set; }

        public bool is_superuser { get; set; } = false;

        [Required(ErrorMessage = " Debe ingresar un usuario")]
        [StringLength(150, ErrorMessage = " El usuario no pueden tener mas de 150 caracteres")]
        public String username { get; set; }

        [Required(ErrorMessage = " Debe ingresar al menos un nombre")]
        [StringLength(30, ErrorMessage = " Los nombres no pueden tener mas de 30 caracteres")]
        public String first_name { get; set; } = "";

        [Required(ErrorMessage = " Debe ingresar al menos un apellido")]
        [StringLength(30, ErrorMessage = " Los apellidos no pueden tener mas de 30 caracteres")]
        public String last_name { get; set; } = "";

        [Display(Name = "Dirección de Email")]
        [Required(ErrorMessage = " Debe ingresar un correo electrónico")]
        [EmailAddress(ErrorMessage = " Correo electrónico no válido")]
        public String email { get; set; }

        public bool is_staff { get; set; } = false;

        public bool is_active { get; set; } = true;

        public DateTime date_joined { get; set; } = DateTime.Now;

        [Display(Name = "Rol")]
        [Required(ErrorMessage = " Debe seleccionar un rol")]
        public int role_id { get; set; }

        //[ForeignKey("usuario_id")]
        //public ICollection<Reportecandidato> Reportecandidato { get; set; }

        //[ForeignKey("usuario_id")]
        //public ICollection<Hurto> Hurto { get; set; }

        //[ForeignKey("usuario_id")]
        //public ICollection<Ubala> Ubala { get; set; }

        //[ForeignKey("usuario_id")]
        //public ICollection<Notificacion> Notificacion { get; set; }

        //[ForeignKey("usuario_id")]
        //public ICollection<Reporteincidencia> Reporteincidencia { get; set; }

        //[ForeignKey("role_id")]
        //public ICollection<Role> Role { get; set; }
    }
}