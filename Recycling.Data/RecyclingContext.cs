using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recycling.Model.Entities;

namespace Recycling.Data
{
    public class RecyclingContext : DbContext
    {
        public RecyclingContext(DbContextOptions options) : base(options) { }

        public DbSet<WasteManagement> WasteManagements { get; set; }
        public DbSet<Hub> Hubs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Fraction> Fractions { get; set; }
        public DbSet<UserHub> UserHubs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            optionsBuilder.UseSqlServer(@"Server=(localhost)\mssqllocaldb;Database=RecyclingNaestved;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Hub>().ToTable("Hub");
            modelBuilder.Entity<Hub>().Property(h => h.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Hub>().Property(h => h.CleanPercentage).HasDefaultValue(0);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Fraction>().ToTable("Fraction");
            modelBuilder.Entity<Fraction>().Property(f => f.UserId).IsRequired();
            modelBuilder.Entity<Fraction>().Property(f => f.HubId).IsRequired();

            modelBuilder.Entity<WasteManagement>().ToTable("WasteManagement");
            modelBuilder.Entity<WasteManagement>().Property(h => h.Name).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<UserHub>().ToTable("UserHub");
            modelBuilder.Entity<UserHub>().Property(uh => uh.HubId).IsRequired();
            modelBuilder.Entity<UserHub>().Property(uh => uh.UserId).IsRequired();
            modelBuilder.Entity<UserHub>().HasKey(t => new { t.UserId, t.HubId });
            modelBuilder.Entity<UserHub>().HasOne(uh => uh.Hub).WithMany(h => h.UserHubs);
            modelBuilder.Entity<UserHub>().HasOne(uh => uh.User).WithMany(h => h.UserHubs);
            modelBuilder.Entity<Fraction>().HasOne(f => f.Hub).WithMany(u => u.TotalFractions);
            modelBuilder.Entity<Fraction>().HasOne(f => f.User).WithMany(u => u.TrashDeliveries);
        }
    }
}
