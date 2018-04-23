using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CasinoData.Db
{
    public partial class casinodbContext : DbContext
    {
        public virtual DbSet<Chips> Chips { get; set; }
        public virtual DbSet<Pockets> Pockets { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"data source=pizzastoredb.crpcv0ktk5tm.us-east-2.rds.amazonaws.com;initial catalog=casinodb; user id =jnr0303;password=Silversword1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chips>(entity =>
            {
                entity.ToTable("Chips", "casinodb");

                entity.Property(e => e.ChipsId).HasColumnName("ChipsID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pockets>(entity =>
            {
                entity.HasKey(e => e.PocketId);

                entity.ToTable("Pockets", "casinodb");

                entity.Property(e => e.PocketId).HasColumnName("PocketID");

                entity.Property(e => e.Cash).HasColumnType("money");

                entity.Property(e => e.ChipsId).HasColumnName("ChipsID");

                entity.HasOne(d => d.Chips)
                    .WithMany(p => p.Pockets)
                    .HasForeignKey(d => d.ChipsId)
                    .HasConstraintName("FK__Pockets__ChipsID__534D60F1");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "casinodb");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserPocketId).HasColumnName("UserPocketID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.UserPocket)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserPocketId)
                    .HasConstraintName("FK__Users__UserPocke__5629CD9C");
            });
        }
    }
}
