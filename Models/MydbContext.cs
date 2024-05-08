using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace registromercy_developers.Models;

public partial class MydbContext : DbContext
{
    public MydbContext()
    {
    }

    public MydbContext(DbContextOptions<MydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        { }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("idCliente");
            entity.Property(e => e.Correo).HasMaxLength(45);
            entity.Property(e => e.Direccion).HasMaxLength(45);
            entity.Property(e => e.FechaRegistro).HasColumnName("Fecha_registro");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Numero).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

            entity.ToTable("servicio");

            entity.Property(e => e.IdServicio)
                .HasColumnType("int(11)")
                .HasColumnName("idServicio");
            entity.Property(e => e.CostoServicio)
                .HasColumnType("int(11)")
                .HasColumnName("Costo_servicio");
            entity.Property(e => e.Descripcion).HasMaxLength(45);
            entity.Property(e => e.FechaServicio).HasColumnName("Fecha_servicio");
            entity.Property(e => e.TipoServicio)
                .HasMaxLength(45)
                .HasColumnName("Tipo_servicio");

            entity.HasMany(d => d.ClienteIdClientes).WithMany(p => p.ServicioIdServicios)
                .UsingEntity<Dictionary<string, object>>(
                    "ServicioHasCliente",
                    r => r.HasOne<Cliente>().WithMany()
                        .HasForeignKey("ClienteIdCliente")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Servicio_has_Cliente_Cliente1"),
                    l => l.HasOne<Servicio>().WithMany()
                        .HasForeignKey("ServicioIdServicio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Servicio_has_Cliente_Servicio"),
                    j =>
                    {
                        j.HasKey("ServicioIdServicio", "ClienteIdCliente")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("servicio_has_cliente");
                        j.HasIndex(new[] { "ClienteIdCliente" }, "fk_Servicio_has_Cliente_Cliente1_idx");
                        j.HasIndex(new[] { "ServicioIdServicio" }, "fk_Servicio_has_Cliente_Servicio_idx");
                        j.IndexerProperty<int>("ServicioIdServicio")
                            .HasColumnType("int(11)")
                            .HasColumnName("Servicio_idServicio");
                        j.IndexerProperty<int>("ClienteIdCliente")
                            .HasColumnType("int(11)")
                            .HasColumnName("Cliente_idCliente");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
