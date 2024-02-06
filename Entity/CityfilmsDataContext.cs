using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CityFilms.Entity;

public partial class CityfilmsDataContext : DbContext
{
    public CityfilmsDataContext()
    {
    }

    public CityfilmsDataContext(DbContextOptions<CityfilmsDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CompanyProfile> CompanyProfiles { get; set; }

    public virtual DbSet<ContactLog> ContactLogs { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<ImageType> ImageTypes { get; set; }

    public virtual DbSet<MailLog> MailLogs { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-RI2D4VO;Database=CITYFILMS_DATA;user=sa;password=1234;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyProfile>(entity =>
        {
            entity.HasKey(e => e.CompanyProfileId).HasName("PK__CompanyP__0B8C9D497A89F59D");

            entity.ToTable("CompanyProfile");

            entity.Property(e => e.CompanyAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CompanyMail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyTagline)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ContactLog>(entity =>
        {
            entity.HasKey(e => e.ContactLogId).HasName("PK__ContactL__BCDA0461836269D0");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Message)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.ImageLocation).HasMaxLength(500);
            entity.Property(e => e.ImageName).HasMaxLength(500);

            entity.HasOne(d => d.ImageType).WithMany(p => p.Images)
                .HasForeignKey(d => d.ImageTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Images_ImageTypes");
        });

        modelBuilder.Entity<ImageType>(entity =>
        {
            entity.HasKey(e => e.ImageTypeId).HasName("PK__ImageTyp__B9E9E876F9642A1B");

            entity.Property(e => e.ImageTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MailLog>(entity =>
        {
            entity.HasKey(e => e.MailLogId).HasName("PK__MailLogs__6D9B35E5B94A3C6E");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.SentBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SentTo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.PartnerId).HasName("PK__Partners__39FD631240551AD6");

            entity.Property(e => e.ParnterDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PartnerEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PartnerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PartnerPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PartnerWebsite)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.PartnerImage).WithMany(p => p.Partners)
                .HasForeignKey(d => d.PartnerImageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Partners_Images");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
