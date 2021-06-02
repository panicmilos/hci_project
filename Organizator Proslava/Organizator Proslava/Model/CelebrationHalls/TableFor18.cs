using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
                Guests = new List<Guest>(Guests.Select(g => g.Clone())),
                Type = Type
            };
        }
    }
}