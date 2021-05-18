namespace Organizator_Proslava.Utility
{
    public class NavigabileModelView : ObservableEntity
    {
        private object _current;
        public object Current { get => _current; set => OnPropertyChanged(ref _current, value); }

        public NavigabileModelView()
        {
        }

        public void Switch(object nextViewModel)
        {
            Current = nextViewModel;
        }
    }
}