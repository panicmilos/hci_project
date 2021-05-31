﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestPreviewViewModel
    {
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }
        
        public Celebration Celebration { get; set; }

        private readonly IDialogService _dialogService;

        public CelebrationRequestPreviewViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCelebrationRequestDetails"));
        }

        public void ForAdd(Celebration celebration)
        {
            Celebration = celebration;
            Next = new RelayCommand(() =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje",
                    "Da li ste sigurni da želite da dodate novu proslavu?")) == DialogResults.Yes)
                    EventBus.FireEvent("FinishAddCelebrationRequest");
            });
        }
        
        public void ForUpdate(Celebration celebration)
        {
            Celebration = celebration;
            Next = new RelayCommand(() =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje",
                    "Da li ste sigurni da želite da sačuvate sve izmene?")) == DialogResults.Yes)
                    EventBus.FireEvent("FinishUpdateCelebrationRequest");
            });
        }
    }
}