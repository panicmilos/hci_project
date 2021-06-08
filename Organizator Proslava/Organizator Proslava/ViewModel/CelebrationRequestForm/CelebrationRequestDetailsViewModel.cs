using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestDetailsViewModel
    {
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }
        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }

        public ObservableCollection<CelebrationDetail> CelebrationDetails { get; set; }
        public CelebrationDetail SelectedCelebrationDetail { get; set; }
        private readonly IDialogService _dialogService;

        public CelebrationRequestDetailsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Add = new RelayCommand(() =>
            {
                var celebrationDetail = _dialogService.OpenDialog(new AddCelebrationDetailViewModel(new CelebrationDetail { Title = $"Zahtev #{CelebrationDetails.Count + 1}" }, _dialogService));
                if (celebrationDetail != null)
                    CelebrationDetails.Add(celebrationDetail);
            });

            Edit = new RelayCommand(() =>
            {
                var celebrationDetail = _dialogService.OpenDialog(new AddCelebrationDetailViewModel(SelectedCelebrationDetail, _dialogService));
                if (celebrationDetail == null) return;
                var index = CelebrationDetails.IndexOf(SelectedCelebrationDetail);
                CelebrationDetails[index].Content = celebrationDetail.Content;
                CelebrationDetails[index].Title = celebrationDetail.Title;
            });

            Delete = new RelayCommand(() =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da uklonite ovaj zahtev?")) == DialogResults.Yes)
                {
                    CelebrationDetails.Remove(SelectedCelebrationDetail);
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCelebrationRequestInfo"));
            Next = new RelayCommand(() => EventBus.FireEvent("NextToLongViewCelebration"));
        }

        public void ForAdd()
        {
            CelebrationDetails = new ObservableCollection<CelebrationDetail>();
        }

        public void ForUpdate(IEnumerable<CelebrationDetail> celebrationDetails)
        {
            CelebrationDetails = new ObservableCollection<CelebrationDetail>(celebrationDetails);
        }
    }
}