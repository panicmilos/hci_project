using System.ComponentModel.DataAnnotations.Schema;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class TableFor18 : DinningTable
    {
        [NotMapped]
        public override string ImageName { get => "18people.png"; }
    }
}