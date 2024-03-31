using Microsoft.EntityFrameworkCore;
using TokenAndRefresh.Models;

namespace TokenAndRefresh.Infra
{
    public partial class ApplicationDbContext : DbContext
    {    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public virtual DbSet<HistoryRefreshTokens> HistoryRefreshTokens { get; set; }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoryRefreshTokens>(entity =>
            {
                entity.HasKey(e => e.IdHistoryRefreshToken).HasName("PK__History");

                entity.ToTable("HistorialRefreshToken");

                entity.Property(e => e.isActive).HasComputedColumnSql("(case when [DateExpiration]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
                entity.Property(e => e.DateCreation).HasColumnType("datetime");
                entity.Property(e => e.DateExpiration).HasColumnType("datetime");
                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Token)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.HistoryRefreshTokens)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__History__IdUser");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser).HasName("PK__User");

                entity.ToTable("Usuario");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
