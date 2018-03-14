using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaDeCienciasEconomicas.Models
{
    [Table("auth_role_permissions", Schema = "publico")]
    public class RolePermission
    {
        [Key]
        [Column(Order = 1)]
        public int role_id { get; set; }
        public virtual Role Role { get; set; }

        [Key]
        [Column(Order = 2)]
        public int permission_id { get; set; }
        public virtual Permission Permission { get; set; }

    }
}