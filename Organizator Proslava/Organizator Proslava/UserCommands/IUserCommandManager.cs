using System.Diagnostics;

namespace Organizator_Proslava.UserCommands
{
    public interface IUserCommandManager
    {
        void Add(IUserCommand command);

        void ExecuteUndo();

        void ExecuteRedo();
    }
}