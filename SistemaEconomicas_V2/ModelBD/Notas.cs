namespace ModelBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Notas")]
    public partial class Notas
    {
        private TestContext context = new TestContext();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdNota { get; set; }

        [Required]
        [StringLength(10)]
        public string IdEstudiante { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCurso { get; set; }

        [Required]
        [StringLength(50)]
        public string Zona { get; set; }

        [Required]
        [StringLength(50)]
        public string Laboratorio { get; set; }

        [Required]
        [StringLength(50)]
        public string Final { get; set; }

        [Required]
        [StringLength(50)]
        public string IdCatedratico { get; set; }

    }
}
