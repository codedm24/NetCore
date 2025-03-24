using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static EFCoreDemo.ColumnNames;

namespace EFCoreDemo
{
    public class BooksContext1 : DbContext
    {
        public BooksContext1(DbContextOptions<BooksContext1> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");

            modelBuilder.Entity<Book1>().Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Book>().Property(c => c.Publisher)
                .HasField("_publisher")
                .IsRequired(false)
                .HasMaxLength(30);

            modelBuilder.Entity<Book>().Property<int>(BookId)
                .HasField("_bookId")
                .IsRequired();

            modelBuilder.Entity<Book>().HasKey(BookId);

            //shadow properties
            modelBuilder.Entity<Book>().Property<bool>(IsDeleted);
            modelBuilder.Entity<Book>().Property<DateTime>(LastUpdated);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries<Book>()
                .Where(e => e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted))
            {
                item.CurrentValues[LastUpdated] = DateTime.Now;

                if (item.State == EntityState.Deleted)
                {
                    item.State = EntityState.Modified;
                    item.CurrentValues[IsDeleted] = true;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
