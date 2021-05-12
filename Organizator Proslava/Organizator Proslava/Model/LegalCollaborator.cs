namespace Organizator_Proslava.Model
{
    public class LegalCollaborator : Collaborator
    {
        private string _identificationNumber;
        public string IdentificationNumber { get => _identificationNumber; set => OnPropertyChanged(ref _identificationNumber, value); }

        private string _pib;
        public string PIB { get => _pib; set => OnPropertyChanged(ref _pib, value); }
    }
}