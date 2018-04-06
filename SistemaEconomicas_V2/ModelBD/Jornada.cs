namespace ModelBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Jornada")]
    public partial class Jornada
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Jornada()
        {
            //se crea la instancia de usuario
            Usuario = new HashSet<Usuario>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JornadaId { get; set; }

        [Required]
        [StringLength(50)]
        public string JornadaNombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
