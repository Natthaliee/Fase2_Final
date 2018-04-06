namespace ModelBD
// Correccion de catedraticos 
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Usuario")]
    public partial class Usuario
    {
        private TestContext context = new TestContext();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserPass { get; set; }

        public int? RolId { get; set; }

        public int? CarreraId { get; set; }

        public int? JornadaId { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual Carrera Carrera { get; set; }

        public virtual Jornada Jornada { get; set; }

        public virtual Rol Rol { get; set; }

        public List<Usuario> ListarUsuarios(){
            var usuarios = new List<Usuario>();
            try
            {
                usuarios = context.Usuario.ToList();
            }
            catch(Exception) {
                throw;
            }
            return usuarios;
        }


        ////public ActionResult Index([Bind(Include = "username,password")] Login user)
        //public String ObtenerUsuario(String UserLogin)
        //{
        //    var usuarioLogin = new Usuario();

        //    try
        //    {
        //     usuarioLogin = context.Usuario.Where(c => c.UserId == UserLogin);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return usuarioLogin.ToString();
        //}


    }
}
