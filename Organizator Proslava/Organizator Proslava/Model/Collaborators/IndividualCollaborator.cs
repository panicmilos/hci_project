using Organizator_Proslava.Model.CelebrationHalls;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Model.Collaborators
{
    public class IndividualCollaborator : Collaborator
    {
        private string _personalId;
        public string PersonalId { get => _personalId; set => OnPropertyChanged(ref _personalId, value); }

        private string _jmbg;
        public string JMBG { get => _jmbg; set => OnPropertyChanged(ref _jmbg, value); }

        public override Collaborator Clone()
        {
            return new IndividualCollaborator
            {
                Id = Id,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                MailAddress = MailAddress,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Password = Password,
                PhoneNumber = PhoneNumber,
                Role = Role,
                PersonalId = PersonalId,
                JMBG = JMBG,
                Address = Address?.Clone(),
                Images = new List<string>(Images),
                CollaboratorServiceBook = CollaboratorServiceBook.Clone(),
                CelebrationHalls = new List<CelebrationHall>(CelebrationHalls.Select(ch => ch.Clone()))
            };
        }
    }
}