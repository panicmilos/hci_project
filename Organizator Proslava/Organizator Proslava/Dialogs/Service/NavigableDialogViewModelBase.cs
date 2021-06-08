using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizator_Proslava.Dialogs.Service
{
    public class NavigableDialogViewModelBase<T> : DialogViewModelBase<T>
    {
        private object _current;
        public object Current { get => _current; set => OnPropertyChanged(ref _current, value); }

        public NavigableDialogViewModelBase()
        {
        }

        public NavigableDialogViewModelBase(string title, int height, int width):
            base(title, height, width)
        {
        }

        public void Switch(object nextViewModel)
        {
            Current = nextViewModel;
        }
    }
}
