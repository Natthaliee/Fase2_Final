using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaDeCienciasEconomicas.Models
{
    [Table("auth_role", Schema = "publico")]
    public class Role
    {
        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required(ErrorMessage = " Debe ingresar un nombre")]
        [StringLength(75, ErrorMessage = " El nombre no puede tener mas de 128 caracteres")]
        [Display(Name = "Nombre")]
        public String name { get; set; }

        [Required(ErrorMessage = " Debe ingresar una descripción")]
        [StringLength(75, ErrorMessage = " La descripción no pueden tener mas de 256 caracteres")]
        [Display(Name = "Descripción")]
        public String description { get; set; }

        [Display(Name = "Es Administrador")]
        public bool isAdmin { get; set; } = false;

        [ForeignKey("role_id")]
        public ICollection<RolePermission> RolePermission { get; set; }

        [ForeignKey("role_id")]
        public ICollection<User> User { get; set; }

    }
}