using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class RequestLongPreviewViewModel : ObservableEntity
    {
        private Celebration _celebration;

        public Celebration Celebration { get => _celebration; set => OnPropertyChanged(ref _celebration, value); }

        public ICommand Back { get; set; }
        public ICommand Details { get; set; }

        public RequestLongPreviewViewModel()
        {
            Back = new RelayCommand(() => EventBus.FireEvent("OrganizerLogin"));
            Details = new RelayCommand(() => EventBus.FireEvent("NextToRequestDetailsForOrganizer"));
        }
    }
}