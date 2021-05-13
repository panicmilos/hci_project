using Organizator_Proslava.Data;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class SpaceViewModel
    {
        public List<PlaceableEntity> PlaceableEntities { get; set; }
        public ICommand AddTableFor6 { get; set; }
        public ICommand AddTableFor18 { get; set; }
        public ICommand AddMusic { get; set; }
        public ICommand AddServingTable { get; set; }
        public ICommand Remove { get; set; }

        public ICommand Save { get; set; }
        public ICommand Back { get; set; }

        public SpaceViewModel(DatabaseContext context)
        {
            PlaceableEntities = new List<PlaceableEntity>();
            AddTableFor6 = new RelayCommand<Image>(AddTableFor6Entity);
            AddTableFor18 = new RelayCommand<Image>(AddTableFor18Entity);
            AddMusic = new RelayCommand<Image>(AddMusicEntity);
            AddServingTable = new RelayCommand<Image>(AddServingTableEntity);
            Remove = new RelayCommand<int>(RemoveEntity);

            Save = new RelayCommand(() =>
            {
                foreach (var table in PlaceableEntities)
                {
                    Trace.WriteLine($"{table.PositionX} {table.PositionY}");
                }

                context.Add(new CelebrationHall
                {
                    Name = "Test",
                    NumberOfGuests = 5,
                    PlaceableEntities = PlaceableEntities
                });

                context.SaveChanges();
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
        }

        public void AddTableFor6Entity(Image image)
        {
            var tableFor6 = new TableFor6
            {
                Type = PlaceableEntityType.TableFor6
            };

            AddBindings(tableFor6, image);
            PlaceableEntities.Add(tableFor6);
        }

        public void AddTableFor18Entity(Image image)
        {
            var tableFor18 = new TableFor18
            {
                Type = PlaceableEntityType.TableFor18
            };

            AddBindings(tableFor18, image);
            PlaceableEntities.Add(tableFor18);
        }

        public void AddMusicEntity(Image image)
        {
            var music = new Music
            {
                Type = PlaceableEntityType.Music
            };

            AddBindings(music, image);
            PlaceableEntities.Add(music);
        }

        public void AddServingTableEntity(Image image)
        {
            var servingTable = new ServingTable
            {
                Type = PlaceableEntityType.Empty
            };

            AddBindings(servingTable, image);
            PlaceableEntities.Add(servingTable);
        }

        private void AddBindings(PlaceableEntity placeableEntity, Image image)
        {
            Binding positionX = new Binding
            {
                Source = placeableEntity,
                Path = new PropertyPath("PositionX"),
                Mode = BindingMode.TwoWay
            };
            image.SetBinding(Canvas.LeftProperty, positionX);

            Binding positionY = new Binding
            {
                Source = placeableEntity,
                Path = new PropertyPath("PositionY"),
                Mode = BindingMode.TwoWay
            };
            image.SetBinding(Canvas.TopProperty, positionY);
        }

        public void RemoveEntity(int entityNo)
        {
            PlaceableEntities.RemoveAt(entityNo);
            Trace.WriteLine(PlaceableEntities.Count);
        }
    }
}