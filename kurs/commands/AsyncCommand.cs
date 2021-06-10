using System;
using System.Threading.Tasks;

namespace kurs.commands
{
    public class AsyncCommand : AsyncCommandBase
    {
        // Асинхронный эквивалент Action
        private readonly Func<Task> _command;
        public AsyncCommand(Func<Task> command)
        {
            _command = command;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override Task ExecuteAsync(object parameter)
        {
            return _command();
        }
    }
}