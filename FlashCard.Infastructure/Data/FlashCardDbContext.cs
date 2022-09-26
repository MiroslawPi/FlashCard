using FlashCard.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Infastructure.Data
{
    public partial class FlashCardDbContext : DbContext
    {
        public FlashCardDbContext()
        {
        }

        public FlashCardDbContext(DbContextOptions<FlashCardDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");

                entity.Property(e => e.BackDescription).HasMaxLength(1000);

                entity.Property(e => e.BackWord).HasMaxLength(200);

                entity.Property(e => e.Created).HasDefaultValueSql("getdate()");

                entity.Property(e => e.FrontDescription).HasMaxLength(1000);

                entity.Property(e => e.FrontWord).HasMaxLength(200);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Cards_Courses");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");

                entity.Property(e => e.Created).HasDefaultValueSql("getdate()");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
