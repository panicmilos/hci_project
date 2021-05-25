using System.ComponentModel.DataAnnotations.Schema;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class ServingTable : PlaceableEntity
    {
        [NotMapped]
        public override string ImageName { get => "empty.png"; }

        public override PlaceableEntity Clone()
        {
            return new ServingTable
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