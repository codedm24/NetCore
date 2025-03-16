using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    public class BooksContext : DbContext
    {
        private const string ConnectionString = @"Server=HP-TUTAI\HPHOMESQLSRVR;Database=Books;integrated security=SSPI;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

    }
}
