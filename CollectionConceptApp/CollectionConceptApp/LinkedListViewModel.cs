using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CollectionConceptApp
{
    internal class LinkedListViewModel : INotifyPropertyChanged
    {
        private PriorityDocumentManager _documentManager;
        private ObservableCollection<string> _documentLog;

        public LinkedListViewModel()
        {
            _documentManager = new PriorityDocumentManager();
            _documentLog = new ObservableCollection<string>();

        }

        public ICommand AddDocumentCommand => new RelayCommand(AddDocument);

        public ICommand ShowAllDocumentCommand => new RelayCommand(ShowAllDocument);

        public ObservableCollection<string> DocumentLog { get => _documentLog; set => _documentLog = value; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void AddDocument(object? parameter)
        { 
            Random random = new Random();
            var random1 = (byte)new Random().Next(1, 9);
            var random2 = (byte)new Random().Next(1, 9);
            string title = $"Title {random1}";
            string content = $"Document Content {random2}";
            var document = new Document(title, content, random1);
            _documentManager.AddDocument(document);
            AddToLog($"Document Add:{document}");
        }

        private void ShowAllDocument(object? parameter)
        { 
            _documentManager.DisplayAllNodes(AddToLog);
        }

        private void AddToLog(string s)
        {
            _documentLog.Add(s);
            OnPropertyChanged(nameof(DocumentLog));
        }

        private void OnPropertyChanged([CallerMemberName] object? paramter=null)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramter?.ToString()));
        }
    }
}
