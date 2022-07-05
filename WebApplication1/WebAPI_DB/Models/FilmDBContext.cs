using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI_DB.Models
{
    public partial class FilmDBContext : DbContext
    {
        public FilmDBContext()
        {
        }

        public FilmDBContext(DbContextOptions<FilmDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Filmler> Filmlers { get; set; } = null!;
        public virtual DbSet<Kategoriler> Kategorilers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data source=.;initial catalog=FilmDB;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filmler>(entity =>
            {
                entity.HasKey(e => e.FilmId);

                entity.ToTable("Filmler");

                entity.HasIndex(e => e.KatId, "IX_Filmler_KatID");

                entity.Property(e => e.FilmId).HasColumnName("FilmID");

                entity.Property(e => e.FilmAdi)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.KatId).HasColumnName("KatID");

                entity.HasOne(d => d.Kat)
                    .WithMany(p => p.Filmlers)
                    .HasForeignKey(d => d.KatId);
            });

            modelBuilder.Entity<Kategoriler>(entity =>
            {
                entity.HasKey(e => e.KatId);

                entity.ToTable("Kategoriler");

                entity.Property(e => e.KatId).HasColumnName("KatID");

                entity.Property(e => e.KategoriAdi)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
