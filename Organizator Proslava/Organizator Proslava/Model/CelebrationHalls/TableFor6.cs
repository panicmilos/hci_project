using System.ComponentModel.DataAnnotations.Schema;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class TableFor6 : DinningTable
    {
        [NotMapped]
        public override string ImageName { get => "6people.png"; }
    }
}