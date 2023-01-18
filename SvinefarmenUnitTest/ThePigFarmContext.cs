using Microsoft.EntityFrameworkCore;
using SvinefarmenUnitTest.Model;

namespace SvinefarmenUnitTest;

public partial class ThePigFarmContext : DbContext
{
    public ThePigFarmContext()
    {
    }

    public ThePigFarmContext(DbContextOptions<ThePigFarmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lightlog> Lightlogs { get; set; }

    public virtual DbSet<Temperaturelog> Temperaturelogs { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=1505;Database=ThePigFarm");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lightlog>(entity =>
        {
            entity
                .ToTable("lightlog");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Leveloflight).HasColumnName("leveloflight");
            entity.Property(e => e.Lightlevelinstable).HasColumnName("lightlevelinstable");
            entity.Property(e => e.Timeoflog)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timeoflog");
        });

        modelBuilder.Entity<Temperaturelog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temperaturelog");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Lighton).HasColumnName("lighton");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.Timeoflog)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timeoflog");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
