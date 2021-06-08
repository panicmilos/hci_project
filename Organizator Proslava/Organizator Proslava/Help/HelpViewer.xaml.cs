using Organizator_Proslava.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.View;
using Organizator_Proslava.ViewModel.CelebrationProposals;
using Organizator_Proslava.ViewModel.CelebrationRequestForm;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using Organizator_Proslava.ViewModel.CollaboratorForm;
using Organizator_Proslava.ViewModel.OrganizatorHome;
using Organizator_Proslava.ViewModel.UsersView;

namespace Organizator_Proslava.Help
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {
        private JavaScriptControlHelper ch;

        public HelpViewer(string helpKey, Window originator)
        {
            InitializeComponent();

            Uri u = new Uri($"http://proslavio-doc.bjelicaluka.com/{GetUrlForHelpKey(helpKey)}");
            ShowHelp(u, originator);
        }

        public HelpViewer(Type dataContextType, Window originator)
        {
            InitializeComponent();

            Uri u = new Uri($"http://proslavio-doc.bjelicaluka.com/{GetUrlForContext(dataContextType)}");
            ShowHelp(u, originator);
        }

        private void ShowHelp(Uri u, Window originator)
        {
            ch = new JavaScriptControlHelper(originator);
            wbHelp.ObjectForScripting = ch;
            wbHelp.Navigate(u);
        }

        private string GetUrlForHelpKey(string helpKey)
        {
            var keysToUrlDictionary = new Dictionary<string, string>()
            {
                { "Login", "user/auth.html#prijava-na-sistem" },
                { "Address", "user/additional.html#odabir-adrese" },
                { "DateTimePicker", "user/additional.html#odabir-datuma" },
            };

            return keysToUrlDictionary.ContainsKey(helpKey) ? keysToUrlDictionary[helpKey] : "";
        }

        private string GetUrlForContext(Type dataContextType)
        {
            var contextsToUrlDictionary = new Dictionary<Type, string>
            {
                [typeof(RegisterViewModel)] = "user/auth.html#kreiranje-naloga",
                
                // Client Home
                [typeof(ClientHomeViewModel)] = "user/home-page.html#pocetna-stranica",
                // Client Create Celeb Req
                [typeof(CelebrationRequestFormViewModel)] = "user/celebration-requests.html#kreiranje-zahteva-za-proslavu",
                [typeof(CelebrationRequestInfoViewModel)] = "user/celebration-requests.html#definisanje-opstih-informacija-proslave",
                [typeof(CelebrationRequestDetailsViewModel)] = "user/celebration-requests.html#definisanje-detalja-proslave",
                [typeof(CelebrationRequestPreviewViewModel)] = "user/celebration-requests.html#pregled-proslave",
                // Client Celeb Proposals
                [typeof(CelebrationProposalsViewModel)] = "user/celebration-proposals.html#ponude-organizatora",
                [typeof(CelebrationDetailsTableViewModel)] = "user/celebration-proposals.html#pregled-detalja-proslave",
                [typeof(CelebrationProposalsTableViewModel)] = "user/celebration-proposals.html#pregled-ponuda-proslave",
                
                // Organizer Home
                [typeof(CurrentOrganizatorCelebrationsTableViewModel)] = "organizer/home-page.html#pregled-preuzetih-proslava",
                [typeof(AcceptCelebrationRequestTableViewModel)] = "organizer/home-page.html#pregled-neprihvacenih-zahteva",
                [typeof(RequestDetailsTableForOrganizerViewModel)] = "organizer/celebration-proposals.html#pregled-detalja-proslave",
                [typeof(ProposalsTableForOrganizerViewModel)] = "organizer/celebration-proposals.html#pregled-ponuda-proslave",
                [typeof(CelebrationProposalDialogViewModel)] = "organizer/celebration-proposals.html#dodavanje-nove-ponude",
                
                
                // Admin Home
                [typeof(AdminHomeViewModel)] = "admin/home-page.html#pocetna-stranica",
                [typeof(UsersTableViewModel)] = "admin/users.html#pregled-korisnika",
                [typeof(OrganziersTableViewModel)] = "admin/organizers.html#pregled-organizatora",
                [typeof(CollaboratorsTableViewModel)] = "admin/collaborators.html#pregled-saradnika",
                [typeof(CreateOrganizerViewModel)] = "admin/organizers.html#kreiranje-organizatora",
                // Celebrations HERE
                [typeof(CollaboratorFormViewModel)] = "admin/collaborators.html#kreiranje-saradnika",
                [typeof(SelectCollaboratorTypeViewModal)] = "admin/collaborators.html#odabir-tipa-saradnika",
                [typeof(LegalCollaboratorInformationsViewModel)] = "admin/collaborators.html#definisanje-informacija-o-saradniku",
                [typeof(IndividualCollaboratorInformationsViewModel)] = "admin/collaborators.html#definisanje-informacija-o-saradniku",
                [typeof(CollaboratorServicesViewModel)] = "admin/collaborators.html#definisanje-usluga-koje-saradnik-nudi",
                [typeof(CollaboratorImagesViewModel)] = "admin/collaborators.html#dodavanje-fotografija",
                [typeof(CollaboratorHallsViewModel)] = "admin/collaborators.html#definisanje-postojecih-sala",
                [typeof(SpaceModelingViewModel)] = "admin/collaborators.html#dodavanje-sale",
                [typeof(NonDinningTableDialogViewModel)] = "admin/collaborators.html#dodavanje-objekta",
                [typeof(PlacingGuestsDialogViewModel)] = "admin/collaborators.html#dodavanje-objekta",
                [typeof(DinningTableDialogViewModel)] = "admin/collaborators.html#dodavanje-objekta",
                
                // Proposal Comment
                [typeof(ProposalCommentsViewModel)] = "user/celebration-proposals.html#pregled-pojedinacne-ponude",
                // Address
                [typeof(MapDialogViewModel)] = "user/additional.html#odabir-adrese",
                // Space Preview
                [typeof(SpacePreviewDialogViewModel)] = "organizer/celebration-proposals.html#pregled-sale",
            };

            return contextsToUrlDictionary.ContainsKey(dataContextType) ? contextsToUrlDictionary[dataContextType] : "";
        }

        private void BrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (wbHelp != null) && wbHelp.CanGoBack;
        }

        private void BrowseBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (wbHelp != null) && wbHelp.CanGoForward;
        }

        private void BrowseForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoForward();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void wbHelp_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
    }
}