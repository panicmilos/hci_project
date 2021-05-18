namespace Organizator_Proslava.Model.Collaborators
{
    public class IndividualCollaborator : Collaborator
    {
        private string _personalId;
        public string PersonalId { get => _personalId; set => OnPropertyChanged(ref _personalId, value); }

        private string _jmbg;
        public string JMBG { get => _jmbg; set => OnPropertyChanged(ref _jmbg, value); }
    }
}