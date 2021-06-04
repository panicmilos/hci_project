using Organizator_Proslava.Data;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.Services.Implementations
{
    public class CollaboratorCrudService : CrudService<Collaborator>, ICollaboratorService
    {
        public CollaboratorCrudService(DatabaseContext context) :
            base(context)
        {
        }

        public override Collaborator Update(Collaborator collaborator)
        {
            var existingCollaborator = Read(collaborator.Id);

            existingCollaborator.FirstName = collaborator.FirstName;
            existingCollaborator.LastName = collaborator.LastName;
            existingCollaborator.PhoneNumber = collaborator.PhoneNumber;
            existingCollaborator.MailAddress = collaborator.MailAddress;

            existingCollaborator.Address = collaborator.Address;
            existingCollaborator.CelebrationHalls = collaborator.CelebrationHalls;
            existingCollaborator.CollaboratorServiceBook = collaborator.CollaboratorServiceBook;
            existingCollaborator.Images = collaborator.Images;

            if (collaborator is LegalCollaborator legalCollaborator)
            {
                (existingCollaborator as LegalCollaborator).IdentificationNumber = legalCollaborator.IdentificationNumber;
                (existingCollaborator as LegalCollaborator).PIB = legalCollaborator.PIB;
            }

            if (collaborator is IndividualCollaborator individualCollaborator)
            {
                (existingCollaborator as IndividualCollaborator).JMBG = individualCollaborator.JMBG;
                (existingCollaborator as IndividualCollaborator).PersonalId = individualCollaborator.PersonalId;
            }

            return base.Update(existingCollaborator);
        }
    }
}