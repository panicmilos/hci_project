using System.ComponentModel.DataAnnotations.Schema;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class Music : PlaceableEntity
    {
        [NotMapped]
        public override string ImageName { get => "music.png"; }

        public override PlaceableEntity Clone()
        {
            return new Music
            {
                Id = Id,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                CelebrationHallId = CelebrationHallId,
                Movable = Movable,
                PositionX = PositionX,
                PositionY = PositionY,
                Type = Type
            };
        }
    }
}