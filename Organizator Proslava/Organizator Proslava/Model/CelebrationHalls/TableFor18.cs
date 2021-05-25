using System.ComponentModel.DataAnnotations.Schema;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class TableFor18 : DinningTable
    {
        [NotMapped]
        public override string ImageName { get => "18people.png"; }

        public override PlaceableEntity Clone()
        {
            return new TableFor18
            {
                Id = Id,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                CelebrationHallId = CelebrationHallId,
                Movable = Movable,
                PositionX = PositionX,
                PositionY = PositionY,
                Seats = Seats,
                Type = Type
            };
        }
    }
}