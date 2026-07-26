using System.Threading.Tasks;

namespace MVVM.Commands
{
    public interface IAsyncCommand
    {
        Task Execute();
    }
    
    public interface IAsyncCommand<T>
    {
        Task Execute(T parameter);
    }
}