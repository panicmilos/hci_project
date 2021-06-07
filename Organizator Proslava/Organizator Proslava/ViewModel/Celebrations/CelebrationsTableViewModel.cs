using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.Celebrations
{
    public class CelebrationsTableViewModel
    {
        public ObservableCollection<Celebration> Celebrations { get; set; }

        public ICommand Preview { get; set; }

        public ICommand Back { get; set; }

        private readonly ICelebrationService _celebrationService;

        public CelebrationsTableViewModel(ICelebrationService celebrationService)
        {
            _celebrationService = celebrationService;

            Celebrations = new ObservableCollection<Celebration>(_celebrationService.Read());

            Back = new RelayCommand(() => EventBus.FireEvent("AdminLogin"));
        }
    }
}
