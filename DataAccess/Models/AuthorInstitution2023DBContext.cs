﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataAccess.Models
{
    public partial class AuthorInstitution2023DBContext : DbContext
    {
        public AuthorInstitution2023DBContext()
        {
        }

        public AuthorInstitution2023DBContext(DbContextOptions<AuthorInstitution2023DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CorrespondingAuthor> CorrespondingAuthors { get; set; } = null!;
        public virtual DbSet<InstitutionInformation> InstitutionInformations { get; set; } = null!;
        public virtual DbSet<MemberAccount> MemberAccounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString())
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            var strConn = config["ConnectionStrings:SystemDBContext"];

            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CorrespondingAuthor>(entity =>
            {
                entity.HasKey(e => e.AuthorId)
                    .HasName("PK__Correspo__70DAFC14B17D3A1E");

                entity.ToTable("CorrespondingAuthor");

                entity.Property(e => e.AuthorId)
                    .HasMaxLength(20)
                    .HasColumnName("AuthorID");

                entity.Property(e => e.AuthorBirthday).HasColumnType("datetime");

                entity.Property(e => e.AuthorName).HasMaxLength(100);

                entity.Property(e => e.Bio).HasMaxLength(250);

                entity.Property(e => e.InstitutionId).HasColumnName("InstitutionID");

                entity.Property(e => e.Skills).HasMaxLength(200);

                entity.HasOne(d => d.Institution)
                    .WithMany(p => p.CorrespondingAuthors)
                    .HasForeignKey(d => d.InstitutionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Correspon__Insti__286302EC");
            });

            modelBuilder.Entity<InstitutionInformation>(entity =>
            {
                entity.HasKey(e => e.InstitutionId)
                    .HasName("PK__Institut__8DF6B94D8ADBF60B");

                entity.ToTable("InstitutionInformation");

                entity.Property(e => e.InstitutionId)
                    .ValueGeneratedNever()
                    .HasColumnName("InstitutionID");

                entity.Property(e => e.Area).HasMaxLength(150);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.InstitutionDescription).HasMaxLength(250);

                entity.Property(e => e.InstitutionName).HasMaxLength(120);

                entity.Property(e => e.TelephoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<MemberAccount>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK__MemberAc__0CF04B38B0F6DEC0");

                entity.ToTable("MemberAccount");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(50)
                    .HasColumnName("MemberID");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.MemberPassword).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
