using EventManagementSystemFinal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystemFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Event configuration
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
                entity.Property(e => e.EventDate).IsRequired();
                entity.Property(e => e.Capacity).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("datetime('now')");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            // Registration configuration
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(r => r.RegistrationId);
                entity.Property(r => r.EventId).IsRequired();
                entity.Property(r => r.UserId).IsRequired();
                entity.Property(r => r.RegistrationDate).HasDefaultValueSql("datetime('now')");
                entity.Property(r => r.Status).HasDefaultValue(RegistrationStatus.Confirmed);

                // Foreign key relationships
                entity.HasOne(r => r.Event)
                      .WithMany(e => e.Registrations)
                      .HasForeignKey(r => r.EventId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.User)
                      .WithMany()
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Category configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Description).HasMaxLength(500);
                entity.Property(c => c.CreatedAt).HasDefaultValueSql("datetime('now')");
                entity.Property(c => c.IsActive).HasDefaultValue(true);
            });

            // Event-Category relationship
            modelBuilder.Entity<Event>()
                .HasOne<Category>()
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
