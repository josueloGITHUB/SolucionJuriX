using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JuriX.Server.Models;

public partial class JurixContext : DbContext
{
    public JurixContext()
    {
    }

    public JurixContext(DbContextOptions<JurixContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abogado> Abogados { get; set; }

    public virtual DbSet<Caso> Casos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Despacho> Despachos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abogado>(entity =>
        {
            entity.HasKey(e => e.AbogadoId).HasName("PK__Abogados__D2729FD04B8180CF");

            entity.Property(e => e.AbogadoId).HasColumnName("AbogadoID");
            entity.Property(e => e.DespachoId).HasColumnName("DespachoID");
            entity.Property(e => e.Especialidad).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Despacho).WithMany(p => p.Abogados)
                .HasForeignKey(d => d.DespachoId)
                .HasConstraintName("FK__Abogados__Despac__4BAC3F29");
        });

        modelBuilder.Entity<Caso>(entity =>
        {
            entity.HasKey(e => e.CasoId).HasName("PK__Casos__692E7573487B277D");

            entity.Property(e => e.CasoId).HasColumnName("CasoID");
            entity.Property(e => e.AbogadoAsignadoId).HasColumnName("AbogadoAsignadoID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.TipoCaso).HasMaxLength(100);

            entity.HasOne(d => d.AbogadoAsignado).WithMany(p => p.Casos)
                .HasForeignKey(d => d.AbogadoAsignadoId)
                .HasConstraintName("FK__Casos__AbogadoAs__5165187F");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Casos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Casos__ClienteID__5070F446");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A7D9188330");

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Despacho>(entity =>
        {
            entity.HasKey(e => e.DespachoId).HasName("PK__Despacho__9037EE4386E603A8");

            entity.Property(e => e.DespachoId).HasColumnName("DespachoID");
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7988E24C7FB");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.AbogadoId).HasColumnName("AbogadoID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Contrasena).HasMaxLength(50);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
            entity.Property(e => e.Rol).HasMaxLength(20);

            entity.HasOne(d => d.Abogado).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.AbogadoId)
                .HasConstraintName("FK__Usuarios__Abogad__5441852A");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Usuarios__Client__5535A963");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
