using Organizator_Proslava.Utility;

namespace Organizator_Proslava.Dialogs.Service
{
    public abstract class DialogViewModelBase<T> : ObservableEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }
        public double ContentWidth { get; set; }
        public double ContentHeight { get; set; }

        public T DialogResult { get; set; }

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
            ContentWidth = windowWidht - 60;
            ContentHeight = windowHeight - 60;
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