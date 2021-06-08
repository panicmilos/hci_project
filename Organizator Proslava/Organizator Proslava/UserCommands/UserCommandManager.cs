using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.UserCommands
{
    public class UserCommandManager : IUserCommandManager
    {
        private readonly Stack<IUserCommand> _undoStack;

        private readonly Stack<IUserCommand> _redoStack;

        public UserCommandManager()
        {
            _undoStack = new Stack<IUserCommand>();
            _redoStack = new Stack<IUserCommand>();
        }

        public void Add(IUserCommand command)
        {
            _undoStack.Push(command);
            _redoStack.Clear();
        }

        public void ExecuteRedo()
        {
            if (!_redoStack.Any())
            {
                return;
            }

            var lastCommand = _redoStack.Pop();
            lastCommand.Redo();

            _undoStack.Push(lastCommand);
        }

        public void ExecuteUndo()
        {
            if (!_undoStack.Any())
            {
                return;
            }

            var lastCommand = _undoStack.Pop();
            lastCommand.Undo();

            _redoStack.Push(lastCommand);
        }
    }
}