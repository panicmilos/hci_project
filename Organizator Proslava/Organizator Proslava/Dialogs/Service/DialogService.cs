namespace Organizator_Proslava.Dialogs.Service
{
    public class DialogService : IDialogService
    {
        public T OpenDialog<T>(DialogViewModelBase<T> viewModel)
        {
            IDialogWindow window = new DialogWindow
            {
                DataContext = viewModel
            };
            window.ShowDialog();

            return viewModel.DialogResult;
        }

        public T OpenDialog<T>(IDialogWindow window, DialogViewModelBase<T> viewModel)
        {
            window.DataContext = viewModel;

            window.ShowDialog();

            return viewModel.DialogResult;
        }
    }
}