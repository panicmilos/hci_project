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
        public List<PlaceableEntity> PlaceableEntities { get; set; }
        public ICommand Add { get; set; }
        public ICommand Remove { get; set; }

        public ICommand Save { get; set; }
        public ICommand Load { get; set; }
        public ICommand Back { get; set; }

        public SpaceViewModel(DatabaseContext context)
        {
            PlaceableEntities = new List<PlaceableEntity>();
            Add = new RelayCommand<PlaceableEntity>(AddEntity);
            Remove = new RelayCommand<int>(RemoveEntity);

            Save = new RelayCommand(() =>
            {
                foreach (var table in PlaceableEntities)
                {
                    Trace.WriteLine($"{table.PositionX} {table.PositionY} {table.Movable}");
                }

                context.Add(new CelebrationHall
                {
                    Name = "Test",
                    NumberOfGuests = 5,
                    PlaceableEntities = PlaceableEntities
                });

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
                PlaceableEntities.AddRange(entities);
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
        }

        public void AddEntity(PlaceableEntity placeableEntity)
        {
            PlaceableEntities.Add(placeableEntity);
        }

        public void RemoveEntity(int entityNo)
        {
            PlaceableEntities.RemoveAt(entityNo);
        }
    }
}