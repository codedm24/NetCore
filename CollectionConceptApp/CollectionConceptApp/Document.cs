using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionConceptApp
{
    internal class Document
    {
        public string Title { get; }
        public string Content { get; }
        public byte Priority { get; }

        public Document(string title, string content, byte priority)
        {
            Title = title;
            Content = content;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"Priority: {Priority}, Title: {Title}, Content: {Content}";
        }
    }
}
