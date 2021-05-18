namespace Organizator_Proslava.Utility
{
    public class NavigabileViewModel : ObservableEntity
    {
        private object _current;
        public object Current { get => _current; set => OnPropertyChanged(ref _current, value); }

        public NavigabileViewModel()
        {
        }

        public void Switch(object viewModel)
        {
            Current = viewModel;
        }
    }
}