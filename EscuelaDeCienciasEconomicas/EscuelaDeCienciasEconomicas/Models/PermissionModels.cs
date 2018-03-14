using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaDeCienciasEconomicas.Models
{
    [Table("auth_permissions", Schema = "publico")]
    public class Permission
    {
        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Controller")]
        public String Controller { get; set; }

        [Display(Name = "Action")]
        public String Action { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "Order")]
        public String Order { get; set; }

        [ForeignKey("permission_id")]
        public ICollection<RolePermission> RolePermission { get; set; }

    }
}