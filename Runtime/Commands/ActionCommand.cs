using System;

namespace MVVM.Commands {
    public class ActionCommand : ICommand {
        private readonly Action _executeAction;

        public ActionCommand(Action executeAction) {
            _executeAction = executeAction;
        }
        
        public void Execute() {
            _executeAction?.Invoke();
        }
    }
}