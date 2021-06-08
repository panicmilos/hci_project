namespace Organizator_Proslava.UserCommands
{
    public interface IUserCommand
    {
        void Undo();

        void Redo();
    }
}