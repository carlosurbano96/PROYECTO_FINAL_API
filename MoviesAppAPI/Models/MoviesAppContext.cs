using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoviesAppAPI.Models;

public partial class MoviesAppContext : DbContext
{
    public MoviesAppContext()
    {
    }

    public MoviesAppContext(DbContextOptions<MoviesAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Configuración para cargar la cadena de conexión desde appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("CadenaSql");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.IdMovie).HasName("PK__Movie__DC0DD0EDFE44EF85");

            entity.ToTable("Movie");

            entity.Property(e => e.Autor)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Descripccion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaLanzamiento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreImagenOriginal)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreImagenServidor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RutaImagen)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__PERSONA__2EC8D2AC6062D389");

            entity.ToTable("PERSONA");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_IdUsuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIOS__5B65BF979C4B5A0A");

            entity.ToTable("USUARIOS");

            entity.Property(e => e.Pass).IsUnicode(false);
            entity.Property(e => e.Usuario1)
                .IsUnicode(false)
                .HasColumnName("Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
