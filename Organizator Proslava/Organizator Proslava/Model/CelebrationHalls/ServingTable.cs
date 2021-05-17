using System.ComponentModel.DataAnnotations.Schema;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class ServingTable : PlaceableEntity
    {
        [NotMapped]
        public override string ImageName { get => "empty.png"; }
    }
}