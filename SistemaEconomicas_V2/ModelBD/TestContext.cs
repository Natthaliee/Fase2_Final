namespace ModelBD
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestContext : DbContext
    {
        public TestContext()
            : base("name=TestContext")
        {
        }

        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<Jornada> Jornada { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Notas> Notas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrera>()
                .Property(e => e.CarreraNombre)
                .IsUnicode(false);

            modelBuilder.Entity<Jornada>()
                .Property(e => e.JornadaNombre)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.RolNombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            //modelBuilder.Entity<Notas>()
            //    .Property(e => e.IdNota)
            //.is(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.NombreCurso)
            .IsUnicode(false);

        }
    }
}
