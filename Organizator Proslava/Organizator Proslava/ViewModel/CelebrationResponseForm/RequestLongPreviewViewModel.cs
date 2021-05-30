using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class RequestLongPreviewViewModel : ObservableEntity
    {
        private CelebrationResponse _celebrationResponse;

        public CelebrationResponse CelebrationResponse { get => _celebrationResponse; set => OnPropertyChanged(ref _celebrationResponse, value); }

        public ICommand Back { get; set; }
        public ICommand Details { get; set; }

        public RequestLongPreviewViewModel()
        {
            Back = new RelayCommand(() => EventBus.FireEvent("BackToCurrentCelebrationsForOrganizer"));
            Details = new RelayCommand(() => EventBus.FireEvent("NextToRequestDetailsForOrganizer"));
        }
    }
}