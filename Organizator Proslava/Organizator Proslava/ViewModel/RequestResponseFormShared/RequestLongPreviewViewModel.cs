using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.RequestResponseFormShared
{
    public class RequestLongPreviewViewModel : ObservableEntity
    {
        private Celebration _celebration;

        public Celebration Celebration { get => _celebration; set => OnPropertyChanged(ref _celebration, value); }

        public RequestLongPreviewViewModel()
        {
        }
    }
}