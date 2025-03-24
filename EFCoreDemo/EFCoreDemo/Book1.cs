using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    public class Book1
    {
        private Book1()
        { 
        
        }

        public Book1(string title, string publisher)
        {
            Title = title;
            _publisher = publisher;
        }

        private int _bookId = 0;
        public string Title { get; set; }
        private string _publisher;
        public string Publisher => _publisher;

        public override string ToString() =>
            $"id: {_bookId}, title: {Title}, publisher: {_publisher}";
    }
}
