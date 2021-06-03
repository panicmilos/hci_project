using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel
{
    public enum SpacePreviewMode
    {
        View,
        Edit
    }

    public class SpacePreviewViewModel : ObservableEntity
    {
        public CelebrationHall Hall { get; set; }

        public SpacePreviewViewModel(CelebrationHall celebrationHall, SpacePreviewMode mode = SpacePreviewMode.View)
        {
            Hall = celebrationHall;
            GlobalStore.AddObject("placeableEntities", Hall.PlaceableEntities);
            GlobalStore.AddObject("spacePreviewMode", mode);
        }
    }
}