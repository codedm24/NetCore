using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    public class BooksService
    {
        private readonly BooksContext1 _context;

        public BooksService(BooksContext1 context) => _context = context;

        public async Task AddBooksAsync()
        {
            var b1 = new Book {
                Title = "WPF",
                Publisher = "Wrox Press"
            };

            var b2 = new Book
            {
                Title = "Pro.CSharp.10.with.NET.6.",
                Publisher = "Apress"
            };

            _context.AddRange(b1, b2);
            int records = await _context.SaveChangesAsync();
            Console.WriteLine($"{records} records added");
        }

        public async Task ReadBooksAsync()
        {
            List<Book> books = await _context.Books.ToListAsync<Book>();
            foreach (var b in books)
            { 
                Console.WriteLine($"{b.Title} {b.Publisher}");
            }
            Console.WriteLine();
        }
    }
}
