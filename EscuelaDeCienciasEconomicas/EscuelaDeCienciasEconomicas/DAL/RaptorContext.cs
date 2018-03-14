using EscuelaDeCienciasEconomicas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EscuelaDeCienciasEconomicas.DAL
{
    public class RaptorContext: DbContext
    {

        public RaptorContext() : base(nameOrConnectionString: "DBRaptor") { }

       
        public DbSet<User> User { get; set; }
        public DbSet<Base> Base { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Reportecandidato> Reportecandidato { get; set; }
        public DbSet<Candidato> Candidato { get; set; }
        public DbSet<Hurto> Hurto { get; set; }
        public DbSet<ImagenCandidato> ImagenCandidato { get; set; }
        public DbSet<Ubala> Ubala { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }
        public DbSet<Notificacionmunicipio> Notificacionmunicipio { get; set; }
        public DbSet<Reporteincidencia> Reporteincidencia { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public static String hostImages = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        public static String imagesAnexo = "/projects/cundinamarca100/anexo/imagen/";
        public static String imagesUbala = "/projects/cundinamarca100/ubala/imagen/";
        public static String imagesHurto = "/projects/reporte_hurtos/hurtos/hurto/";

    }
}