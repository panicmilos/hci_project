using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class SpaceViewModel
    {
        public CelebrationHall Hall { get; set; }
        public ICommand Add { get; set; }
        public ICommand Remove { get; set; }

        public SpaceViewModel() :
            this(new CelebrationHall())
        {
        }

        public SpaceViewModel(CelebrationHall celebrationHall)
        {
            Hall = celebrationHall;
            Add = new RelayCommand<PlaceableEntity>(AddEntity);
            Remove = new RelayCommand<int>(RemoveEntity);
            GlobalStore.AddObject("placeableEntities", Hall.PlaceableEntities);
        }

        public void AddEntity(PlaceableEntity placeableEntity)
        {
            Hall.PlaceableEntities.Add(placeableEntity);
        }

        public void RemoveEntity(int entityNo)
        {
            Hall.PlaceableEntities.RemoveAt(entityNo);
        }
    }
}