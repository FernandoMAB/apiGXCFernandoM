using apiGXCFernandoM.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure.Annotations;
using System.Reflection;
using System.Threading;

namespace apiGXCFernandoM.Context
{
    public class ColegioContext : DbContext
    {
        public DbSet<Calificaciones> Calificaciones { get; set; }
        public DbSet<Colegio> Colegios { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public ColegioContext(DbContextOptions<ColegioContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            InitialData initialData = new InitialData();

            modelBuilder.Entity<Calificaciones>(calificacion =>
            {
                calificacion.ToTable("Calificacion");

                calificacion.HasKey(p => p.Id);
                calificacion.Property(p => p.Id).HasColumnType("int").ValueGeneratedOnAdd();

                calificacion.HasOne(p => p.Colegio).WithMany(p => p.Calificaciones).HasForeignKey(p => p.idColegio).OnDelete(DeleteBehavior.ClientSetNull);

                calificacion.HasOne(p => p.Materia).WithMany(p => p.Calificaciones).HasForeignKey(p => p.idMateria).OnDelete(DeleteBehavior.ClientSetNull);

                calificacion.HasOne(p => p.Usuario).WithMany(p => p.Calificaciones).HasForeignKey(p => p.idUsuario).OnDelete(DeleteBehavior.ClientSetNull);

                calificacion.Property(p => p.calificacion).HasColumnType("decimal").HasPrecision(10,2);

                calificacion.HasData(initialData.addCalificaciones());
            });

            modelBuilder.Entity<Colegio>(colegio =>
            {
                colegio.ToTable("Colegio");

                colegio.HasKey(p => p.id);
                colegio.Property(p => p.id).HasColumnType("int").ValueGeneratedOnAdd();

                colegio.Property(p => p.nombre).HasColumnType("varchar").HasMaxLength(256);
                colegio.Property(p => p.tipoColegio).HasColumnType("varchar").HasMaxLength(64);

                colegio.HasData(initialData.addColegios());
                
                //colegio.Ignore(p => p.Materias);
                //colegio.Ignore(p => p.Calificaciones);

            });

            modelBuilder.Entity<Materia>(materia =>
            {
                materia.ToTable("Materia");

                materia.HasKey(p => p.Id);
                materia.Property(p => p.Id).HasColumnType("int").ValueGeneratedOnAdd();

                materia.HasOne(p => p.Colegio).WithMany(p => p.Materias).HasForeignKey(p => p.idColegio).OnDelete(DeleteBehavior.ClientSetNull);

                materia.Property(p => p.nombre).HasColumnType("varchar").HasMaxLength(256);
                materia.Property(p => p.area).HasColumnType("varchar").HasMaxLength(128);
                materia.Property(p => p.numeroEstudiantes).HasColumnType("int").HasDefaultValue(0);
                materia.Property(p => p.docenteAsignado).HasColumnType("varchar").HasMaxLength(512);
                materia.Property(p => p.curso).HasColumnType("varchar").HasMaxLength(64);
                materia.Property(p => p.paralelo).HasColumnType("varchar").HasMaxLength(16);

                materia.HasData(initialData.addMaterias());

                //materia.Ignore(p => p.Calificaciones);
            });


            modelBuilder.Entity<Usuario>(usuario =>
            {
                usuario.ToTable("Usuario");

                usuario.HasKey(p => p.Id);
                usuario.Property(p => p.Id).ValueGeneratedOnAdd();

                usuario.Property(p => p.nombreCompleto).HasColumnType("varchar").HasMaxLength(256);
                usuario.Property(p => p.username).HasColumnType("varchar").HasMaxLength(128);
                usuario.Property(p => p.password).HasColumnType("varchar").HasMaxLength(128);
                usuario.Property(p => p.correoElectronico).HasColumnType("varchar").HasMaxLength(256);
                usuario.Property(p => p.rol).HasColumnType("varchar").HasMaxLength(32);

                usuario.HasData(initialData.addUsuarios());
                //usuario.Ignore(p => p.Calificaciones);
            });
        }
    }
}
