using System;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class Guest : BaseObservableEntity, ICloneable<Guest>
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private double _positionX;
        public double PositionX { get => _positionX; set => OnPropertyChanged(ref _positionX, value); }

        private double _positionY;
        public double PositionY { get => _positionY; set => OnPropertyChanged(ref _positionY, value); }

        private Guid _dinningTableId;
        public Guid DinningTableId { get => _dinningTableId; set => OnPropertyChanged(ref _dinningTableId, value); }

        private DinningTable _dinningTable;
        public virtual DinningTable DinningTable { get => _dinningTable; set => OnPropertyChanged(ref _dinningTable, value); }

        public Guest Clone()
        {
            return new Guest
            {
                Id = Id,
                CreatedAt = CreatedAt,
                IsActive = IsActive,
                DinningTableId = DinningTableId,
                PositionX = PositionX,
                PositionY = PositionY
            };
        }
    }
}