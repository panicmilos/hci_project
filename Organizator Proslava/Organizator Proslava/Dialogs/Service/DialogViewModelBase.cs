using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Service
{
    public abstract class DialogViewModelBase<T> : ObservableEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }

        private double _windowWidth;
        public double WindowWidth { get { return _windowWidth; } set { OnPropertyChanged(ref _windowWidth, value); OnPropertyChanged("ContentWidth"); } }
        private double _windowHeight;
        public double WindowHeight { get { return _windowHeight; } set { OnPropertyChanged(ref _windowHeight, value); OnPropertyChanged("ContentHeight"); } }
        public double ContentWidth { get => WindowWidth - 60; }
        public double ContentHeight { get => WindowHeight - 60; }

        public T DialogResult { get; set; }

        public ICommand OpenDemo { get; set; }

        public DialogViewModelBase() :
            this(string.Empty, string.Empty, 360, 160)
        {
        }

        public DialogViewModelBase(string title) :
            this(title, string.Empty, 360, 160)
        {
        }

        public DialogViewModelBase(string title, string message) :
            this(title, message, 360, 160)
        {
        }

        public DialogViewModelBase(string title, int windowWidht, int windowHeight) :
           this(title, string.Empty, windowWidht, windowHeight)
        {
        }

        public DialogViewModelBase(string title, string message, int windowWidht, int windowHeight)
        {
            Title = title;
            Message = message;
            WindowWidth = windowWidht;
            WindowHeight = windowHeight;
            OpenDemo = new RelayCommand(() => new DemoService().OpenDemo(GetType()));
        }

        public void CloseDialogWithResult(IDialogWindow dialog, T result)
        {
            DialogResult = result;

            if (dialog != null)
            {
                dialog.DialogResult = true;
            }
        }
    }
}