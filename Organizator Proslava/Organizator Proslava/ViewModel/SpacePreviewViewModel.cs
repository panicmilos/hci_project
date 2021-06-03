using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel
{
    public class SpacePreviewViewModel : ObservableEntity
    {
        public CelebrationHall Hall { get; set; }

        public SpacePreviewViewModel(CelebrationHall celebrationHall)
        {
            Hall = celebrationHall;
            GlobalStore.AddObject("placeableEntities", Hall.PlaceableEntities);
        }
    }
}