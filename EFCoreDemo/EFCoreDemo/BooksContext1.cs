using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    public class BooksContext1 : DbContext
    {
        public BooksContext1(DbContextOptions<BooksContext1> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

    }
}
