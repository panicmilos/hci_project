using System.Windows.Input;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestInfoViewModel
    {
        public Celebration Celebration { get; set; }
        
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public CelebrationRequestInfoViewModel()
        {
            Celebration = new Celebration();
            
            Back = new RelayCommand(() => EventBus.FireEvent("BackToClientPage"));
            Next = new RelayCommand(() => EventBus.FireEvent("NextToCelebrationRequestDetails"));
        }
    }
}