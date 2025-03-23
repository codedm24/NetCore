using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    public class MenusContext : DbContext
    {
        private const string connectionString = @"server=HP-TUTAI\HPHOMESQLSRVR;Database=MenuCards;Integrated Security=SSPI;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("mc");
            modelBuilder.Entity<MenuCard>().ToTable("MenuCards")
                .HasKey(c => c.MenuCardId);

            modelBuilder.Entity<Menu>().ToTable("Menus")
                .HasKey(c => c.MenuId); 

            modelBuilder.Entity<MenuCard>().
                Property(c => c.MenuCardId).
                ValueGeneratedOnAdd();

            modelBuilder.Entity<MenuCard>().HasMany(c => c.Menus)
                .WithOne(c => c.MenuCard);

            modelBuilder.Entity<Menu>().HasOne(m => m.MenuCard)
                .WithMany(c => c.Menus).HasForeignKey(m => m.MenuCardId);


        }
    }
}
