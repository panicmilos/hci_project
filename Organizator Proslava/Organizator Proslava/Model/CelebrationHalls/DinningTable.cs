using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class DinningTable : PlaceableEntity
    {
        private int _seats;
        public int Seats { get => _seats; set => OnPropertyChanged(ref _seats, value); }
    }
}