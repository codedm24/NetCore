using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CollectionConceptApp
{
    internal class PriorityDocumentManager
    {
        private readonly LinkedList<Document> _documentList;
        private readonly List<LinkedListNode<Document>> _prioritynodes;

        public PriorityDocumentManager()
        {
            _documentList = new LinkedList<Document>();
            _prioritynodes = new List<LinkedListNode<Document>>(10);
            for (int i = 0; i < 10; i++)
            {
                _prioritynodes.Add(new LinkedListNode<Document>(null));
            }
        }

        public void AddDocument(Document d)
        {
            if(d == null) throw new ArgumentNullException(nameof(d), "Document cannot be null");
            AddDocumentToPriorityNode(d, d.Priority);
        }

        private void AddDocumentToPriorityNode(Document d, int priority)
        { 
            if(priority > 9 || priority < 0) throw new ArgumentException("Priority must be between 0 and 9");

            if (_prioritynodes[priority].Value == null)
            {
                --priority;
                if (priority >= 0)
                {
                    AddDocumentToPriorityNode(d, priority);
                }
                else
                {
                    _documentList.AddLast(d);
                    _prioritynodes[d.Priority] = _documentList.Last;
                }
                return;
            }
            else
            { 
                LinkedListNode<Document> prioNode = _prioritynodes[priority];
                if(priority == d.Priority)
                {
                    _documentList.AddAfter(prioNode, d);
                    _prioritynodes[d.Priority] = prioNode.Next;
                }
                else
                {
                    LinkedListNode<Document> firstPrioNode = prioNode;
                    while (firstPrioNode.Previous != null && firstPrioNode.Previous.Value.Priority == prioNode.Value.Priority)
                    {
                        firstPrioNode = firstPrioNode.Previous;
                    }
                    _documentList.AddBefore(firstPrioNode, d);
                    _prioritynodes[d.Priority] = firstPrioNode.Previous;
                }
            }
        }

        public void DisplayAllNodes(Action<string> displayDocument)
        { 
            foreach(Document doc in _documentList)
            {
                Console.WriteLine("Title: {0}, Content: {1}, Priority: {2}", doc.Title, doc.Content, doc.Priority);
                displayDocument(string.Format("Title: {0}, Content: {1}, Priority: {2}", doc.Title, doc.Content, doc.Priority));
            }
        }

        public Document GetDocument()
        {
            Document doc = _documentList.First.Value;
            _documentList.RemoveFirst();
            return doc;
        }

    }
}
