using System.ComponentModel.DataAnnotations.Schema;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class Music : PlaceableEntity
    {
        [NotMapped]
        public override string ImageName { get => "music.png"; }
    }
}