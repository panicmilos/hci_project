﻿using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class MoreAboutCelebrationsDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public Celebration Celebration { get; set; }
        public ICommand Back { get; set; }
        public ICommand Details { get; set; }

        private readonly IDialogService _dialogService;

        public MoreAboutCelebrationsDialogViewModel(Celebration celebration):
            base("Pregled proslave", 550, 450)
        {
            _dialogService = new DialogService();

            Celebration = celebration;
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));

            Details = new RelayCommand(() =>
            {
                CelebrationsDetailsTableDialogViewModel cdtdvm = new CelebrationsDetailsTableDialogViewModel(_dialogService, celebration);
                _dialogService.OpenDialog(new DetailsDialogViewModel(cdtdvm));
            });
        }
    }
}
