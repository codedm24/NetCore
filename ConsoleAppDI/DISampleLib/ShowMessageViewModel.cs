using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DISampleLib
{
    public class ShowMessageViewModel
    {
        private readonly IMessageService _messageService;

        public ShowMessageViewModel(IMessageService messageService)
        {
            _messageService = messageService ??
                throw new ArgumentNullException(nameof(messageService));
            ShowMessageCommand = new RelayCommand(ShowMessage);
        }

        public ICommand ShowMessageCommand { get; }

        public void ShowMessage()
        {
            _messageService.ShowMessageAsync("A message from the view-model");
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        //this code is for.netframework 
        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() { 
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
