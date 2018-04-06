namespace ModelBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Curso")]
    public partial class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCurso { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCurso { get; set; }

        [Required]
        [StringLength(50)]
        public string Semestre { get; set; }

        [Required]
        [StringLength(50)]
        public string Laboratorio { get; set; }

    }
}
