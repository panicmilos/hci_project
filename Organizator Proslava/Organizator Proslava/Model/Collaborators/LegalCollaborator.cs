using Organizator_Proslava.Model.CelebrationHalls;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Model.Collaborators
{
    public class LegalCollaborator : Collaborator
    {
        private string _identificationNumber;
        public string IdentificationNumber { get => _identificationNumber; set => OnPropertyChanged(ref _identificationNumber, value); }

        private string _pib;
        public string PIB { get => _pib; set => OnPropertyChanged(ref _pib, value); }

        public override Collaborator Clone()
        {
            return new LegalCollaborator
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
                IdentificationNumber = IdentificationNumber,
                PIB = PIB,
                Address = Address?.Clone(),
                Images = new List<string>(Images),
                CollaboratorServiceBook = CollaboratorServiceBook.Clone(),
                CelebrationHalls = new List<CelebrationHall>(CelebrationHalls.Select(ch => ch.Clone()))
            };
        }
    }
}