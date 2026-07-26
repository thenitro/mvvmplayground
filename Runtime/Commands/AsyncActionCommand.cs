using System;
using System.Threading.Tasks;

namespace MVVM.Commands
{
    public class AsyncActionCommand : IAsyncCommand
    {
        private readonly Func<Task> _executeAction;

        public AsyncActionCommand(Func<Task> executeAction)
        {
            _executeAction = executeAction;
        }

        public Task Execute()
        {
            return _executeAction.Invoke();
        }
    }
}