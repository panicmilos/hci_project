using Organizator_Proslava.Data;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class SpaceViewModel
    {
        public CelebrationHall Hall { get; set; }
        public ICommand Add { get; set; }
        public ICommand Remove { get; set; }

        public ICommand Save { get; set; }
        public ICommand Load { get; set; }
        public ICommand Back { get; set; }

        public SpaceViewModel(DatabaseContext context)
        {
            Hall = new CelebrationHall();
            Add = new RelayCommand<PlaceableEntity>(AddEntity);
            Remove = new RelayCommand<int>(RemoveEntity);

            Save = new RelayCommand(() =>
            {
                foreach (var table in Hall.PlaceableEntities)
                {
                    Trace.WriteLine($"{table.PositionX} {table.PositionY} {table.Movable}");
                }

                context.Add(Hall);

                //context.SaveChanges();
            });

            Load = new RelayCommand(() =>
            {
                var entities = context.PlaceableEntities.ToList();
                foreach (var table in entities)
                {
                    Trace.WriteLine($"{table.PositionX} {table.PositionY}");
                }

                EventBus.FireEvent("ShowPlaceableEntities", entities);
                Hall.PlaceableEntities.AddRange(entities);
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
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