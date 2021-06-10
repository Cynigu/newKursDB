using System.Threading.Tasks;
using System.Windows.Input;

namespace kurs.commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}