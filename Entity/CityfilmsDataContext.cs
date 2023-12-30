using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CityFlims.Entity;

public partial class CityfilmsDataContext : DbContext
{
    public CityfilmsDataContext()
    {
    }

    public CityfilmsDataContext(DbContextOptions<CityfilmsDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Image> Images { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-RI2D4VO;Database=CITYFILMS_DATA;user=sa;password=1234;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.ImageName).HasMaxLength(500);
            entity.Property(e => e.ImageLocation).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
