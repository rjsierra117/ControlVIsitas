using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIControlVisitas.Models;

public partial class ControlVisitasContext : DbContext
{
    public ControlVisitasContext()
    {
    }

    public ControlVisitasContext(DbContextOptions<ControlVisitasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Casa> Casas { get; set; }

    public virtual DbSet<Invitado> Invitados { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Residente> Residentes { get; set; }

    public virtual DbSet<Visita> Visitas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Casa>(entity =>
        {
            entity.HasKey(e => e.IdCasa);

            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Invitado>(entity =>
        {
            entity.HasKey(e => e.IdInvitado);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK_Persona");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NoIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Residente>(entity =>
        {
            entity.HasKey(e => e.IdResidente);
        });

        modelBuilder.Entity<Visita>(entity =>
        {
            entity.HasKey(e => e.IdVisitas);

            entity.Property(e => e.Observaciones)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
