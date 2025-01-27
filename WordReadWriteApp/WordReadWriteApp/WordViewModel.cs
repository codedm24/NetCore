using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.Runtime.CompilerServices;

namespace WordReadWriteApp
{
    internal class WordViewModel : INotifyPropertyChanged
    {
        private string selectedFilePath = string.Empty;

        private ObservableCollection<ListItem> listViewItemSource;

        public WordViewModel()
        {
            listViewItemSource = new ObservableCollection<ListItem>();
            SelectFileCommand = new RelayCommand(SelectFile);
            ShowContentCommand = new RelayCommand(ShowContent);
        }

        public ICommand SelectFileCommand { get; }

        public ICommand ShowContentCommand { get; }

        public string SelectedFilePath
        {
            get => selectedFilePath;
            set
            {
                selectedFilePath = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ListItem> ListViewItemSource
        { 
            get => listViewItemSource;
            set
            {
                listViewItemSource = value;
                OnPropertyChanged();
            }

        }

        public void SelectFile(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word Files(*.docx)| *.docx| All files (*.*)|(*.*)";
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;
            }
        }

        private void ShowContent(object parameter)
        { 
            ProcessWordFile(SelectedFilePath);
        }

        public void ProcessWordFile(string selectedFilePath)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(selectedFilePath, false))
            {
                Body? body = wordDoc.MainDocumentPart?.Document.Body;
                if (body == null)
                {
                    MessageBox.Show("The document does not contain a body.");
                    return;
                }

                string? docText = body.InnerText;

                StringBuilder sb = new StringBuilder();
                string searchphrase = "hello copilot";

                foreach (OpenXmlElement paragraph in body.ChildElements)
                {
                    string wordParagraph = paragraph.InnerText.ToString();

                    if (wordParagraph.ToLower().StartsWith(searchphrase))
                    {
                        if (sb.Length > 0)
                        {
                            listViewItemSource.Add(new ListItem { TextData = $"Answer: {sb.ToString()}" });
                            sb = new StringBuilder();
                        }

                        listViewItemSource.Add(new ListItem { TextData = $"Question: {wordParagraph}" });
                    }
                    else
                    {
                        sb.Append(wordParagraph);
                    }
                }

                if (sb.Length > 0)
                    listViewItemSource.Add(new ListItem { TextData = $"Answer: {sb.ToString()}" });
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
