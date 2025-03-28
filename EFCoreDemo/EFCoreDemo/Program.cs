﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using System.ComponentModel;

namespace EFCoreDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //AddLogging();
            //await CreateTheDatabaseAsync();
            //await DeleteTheDatabaseAsync();
            //await AddBookAsync("Professional ASP.NET MVC 5", "Wrox Press");
            //await AddBookAsync("Professional C# 7", "Wrox Press");
            //await AddBooksAsync();
            //await AddBooksAsync1();
            //await ReadBooksAsync();
            //await QueryBooksAsync();
            //await UpdateBookAsync();
            //await DeleteBooksAsync();

            var p = new Program();
            p.InitializeServices();
            p.ConfigureLogging();
            var service = p.Container!.GetService<BooksService>();
            //await service!.AddBooksAsync();
            await service!.ReadBooksAsync();
            await p.QueryAllBooksAsync();
        }

        private static void AddLogging()
        {
            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddLogging(builder => {
            //    builder.AddConsole();
            //    builder.SetMinimumLevel(LogLevel.Information);
            //});

            //using (var serviceProvider = serviceCollection.BuildServiceProvider())
            //{
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //    var logger = loggerFactory!.CreateLogger<Program>();
            //    logger.LogInformation("Logging is configured");
            //}

            //using (var context = new BooksContext())
            //{ 
            //    IServiceProvider? provider = context.GetInfrastructure<IServiceProvider>();
            //    ILoggerFactory? loggerFactory = provider.GetService<ILoggerFactory>();
            //    loggerFactory.AddConsole(LogLevel.Information);
            //}
        }

        private  void InitializeServices()
        { 
            const string ConnectionString = @"Server=HP-TUTAI\HPHOMESQLSRVR;Database=Books;integrated security=SSPI;Trusted_Connection=True;TrustServerCertificate=True;";
            var services = new ServiceCollection();
            services.AddTransient<BooksService>()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<BooksContext1>(options => options.UseSqlServer(ConnectionString));
            services.AddLogging();

            Container = services.BuildServiceProvider();
        }

        public IServiceProvider? Container { get; private set; }

        private void ConfigureLogging()
        { 
            //var loggerFactory = Container!.GetService<ILoggerFactory>();
            //loggerFactory.AddConsole();
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Logging is configured");
        }

        private static async Task CreateTheDatabaseAsync()
        {
            using (var context = new BooksContext())
            {
                bool created = await context.Database.EnsureCreatedAsync();
                string creationInfo = created ? "created" : "exists";
                Console.WriteLine($"database {creationInfo}");
            }
        }

        private static async Task DeleteTheDatabaseAsync()
        {
            Console.Write("Delete the database? ");
            string? input = Console.ReadLine();
            if (input?.ToLower() == "y")
            {
                using (var context = new BooksContext())
                {
                    bool deleted = await context.Database.EnsureDeletedAsync();
                    string deletionInfo = deleted ? "deleted" : "not deleted";
                    Console.WriteLine($"database {deletionInfo}");
                }
            }
        }

        private static async Task AddBookAsync(string title, string publisher)
        {
            using (var context = new BooksContext())
            {
                var book = new Book { Title = title, Publisher = publisher };
                await context.Books.AddAsync(book);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} record added");
            }
            Console.WriteLine();
        }

        private static async Task AddBooksAsync()
        {
            using (var context = new BooksContext())
            { 
                var b1 = new  Book { Title = "beginning-visual-c-2010", Publisher = "Wrox Press" };
                var b2 = new Book { Title = "Professional-AngularJS", Publisher = "Wrox Press" };
                var b3 = new Book { Title = "professional-wcf-4-windows-communication-foundation-with-net-4-wrox-programmer-to-programmer", Publisher = "Wrox Press" };
                var b4 = new Book {  Title = "Programming.ML.NET", Publisher = "Microsoft Press" };
                await context.AddRangeAsync(b1, b2, b3, b4);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }

        private static async Task AddBooksAsync1()
        {
            using (var context = new BooksContext())
            {
                var b1 = new Book { Title = "Professional C# 5 and .NET 4.5.1", Publisher = "Wrox Press" };
                var b2 = new Book { Title = "Professional C# 2012 and .NET 4.5", Publisher = "Wrox Press" };
                var b3 = new Book { Title = "Javascript for Kids", Publisher = "Wrox Press" };
                var b4 = new Book { Title = "Web Design with HTML and CSS", Publisher = "Wrox Press" };
                var b5 = new Book { Title = "Professional C# 6 and .NET 5", Publisher = "Wrox Press" };
                var b6 = new Book { Title = "Conflict Handling", Publisher = "Entity FrameWork" };
                var b7 = new Book { Title = "SQL Server Bible", Publisher = "John Wiley & Sons" };
                var b8 = new Book { Title = "Professional ASP.NET MVC 5", Publisher = "Wrox Press" };
                var b9 = new Book { Title = "Beginning-visual-c-2010\t", Publisher = "Wrox Press" };
                var b10 = new Book { Title = "Professional-AngularJS", Publisher = "Wrox Press" };
                var b11 = new Book { Title = "professional-wcf-4-windows-communication-foundation-with-net-4-wrox-programmer-to-programmer", Publisher = "Wrox Press" };
                var b12 = new Book { Title = "Programming.ML.NET", Publisher = "Microsoft Press" };
                var b13 = new Book { Title = "Professional C# 7", Publisher = "Wrox Press" };

                await context.AddRangeAsync(b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} records added");

            }
            Console.WriteLine();
        }

        private static async Task ReadBooksAsync()
        {
            using (var context = new BooksContext())
            { 
                List<Book> books = await context.Books.ToListAsync();
                foreach (var book in books)
                { 
                    Console.WriteLine($"{book.Title} {book.Publisher}");    
                }
            }
            Console.WriteLine();
        }

        private static async Task QueryBooksAsync()
        {
            using (var context = new BooksContext())
            {
                //List<Book> wroxBooks = await context.Books!.Where(b => b.Publisher == "Wrox Press").ToListAsync<Book>();
                List<Book> wroxBooks = await context.Books!.Where(b => b.Title == "Professional C# 7").ToListAsync<Book>();

                foreach (var b in wroxBooks)
                {
                    Console.WriteLine($"{b.Title} {b.Publisher}");
                }
            }
            Console.WriteLine();
        }

        private static async Task UpdateBookAsync()
        {
            using (var context = new BooksContext()) 
            {
                int records = 0;
                Book? book = await context.Books!.Where(b => b.Title == "Professional C# 7").FirstOrDefaultAsync();
                if (book != null)
                {
                    book.Title = "Professional C# 7 and .NET Core 2.0";
                    records = await context.SaveChangesAsync();
                }
                Console.WriteLine($"{records} record updated");
            }
            Console.WriteLine();
        }

        private static async Task DeleteBooksAsync()
        {
            using (var context = new BooksContext())
            {
                var books = context.Books;
                context.Books.RemoveRange(books);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} records deleted");
            }
            Console.WriteLine();
        }

        private async Task QueryAllBooksAsync()
        { 
            Console.WriteLine(nameof(QueryAllBooksAsync));
            using (var context = new BooksContext())
            {
                List<Book> books = await context.Books.ToListAsync();
                foreach (var b in books)
                {
                    Console.WriteLine(b);
                }
            }
            Console.WriteLine();
        }

        private async Task RawSqlQuery(string publisher)
        { 
            Console.WriteLine(nameof (RawSqlQuery));
            using (var context = new BooksContext())
            { 
                IList<Book> books = await context.Books.FromSql($"SELECT * FROM Books WHERE Publisher={publisher}")
                    .ToListAsync();

                foreach (var b in books)
                {
                    Console.WriteLine($"{b.Title} {b.Publisher}");
                }
            }

            Console.WriteLine();
        }

    }
}
