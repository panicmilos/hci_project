namespace Organizator_Proslava.Dialogs.Service
{
    public interface IDialogService
    {
        T OpenDialog<T>(DialogViewModelBase<T> viewModel);

        T OpenDialog<T>(IDialogWindow window, DialogViewModelBase<T> viewModel);
    }
}