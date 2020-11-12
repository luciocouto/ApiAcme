using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiAcme.Models
{
    public partial class AcmeContext : DbContext
    {
        public AcmeContext()
        {
        }

        public AcmeContext(DbContextOptions<AcmeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Rates> Rates { get; set; }

        //Deve ser tirado do codigo por seguranca
        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=localhost,1433\\\\Catalog=Acme;Database=Acme;User=sa;Password=P4ssw0rd;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>(entity =>
            {
                entity.ToTable("authors");

                entity.HasIndex(e => e.Email)
                    .HasName("email_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Added)
                    .IsRequired()
                    .HasColumnName("added")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id");

                entity.Property(e => e.Contentspost)
                    .IsRequired()
                    .HasColumnName("contentspost")
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Datepost)
                    .HasColumnName("datepost")
                    .HasColumnType("datetime");

                entity.Property(e => e.Descriptionpost)
                    .IsRequired()
                    .HasColumnName("descriptionpost")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__posts__author_id__32E0915F");
            });

            modelBuilder.Entity<Rates>(entity =>
            {
                entity.ToTable("rates");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Daterate)
                    .HasColumnName("daterate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Noterate)
                    .IsRequired()
                    .HasColumnName("noterate")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rates__author_id__35BCFE0A");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rates__post_id__36B12243");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
