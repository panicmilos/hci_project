using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Organizator_Proslava.View
{
    /// <summary>
    /// Interaction logic for Space.xaml
    /// </summary>
    public partial class Space : UserControl
    {
        private SpaceViewModel spaceViewModel { get => DataContext as SpaceViewModel; }
        private IList<Image> _images;

        public Space()
        {
            InitializeComponent();

            _images = new List<Image>();

            EventBus.RegisterHandler("ShowPlaceableEntities", (entities) =>
            {
                var placebleEntities = entities as List<PlaceableEntity>;
                foreach (var placeableEntity in placebleEntities)
                {
                    var image = AddImageToCanvas(null, null, placeableEntity.ImageName);
                    AddBindings(placeableEntity, image);
                }
            });
        }

        private void People6_Click(object sender, RoutedEventArgs e)
        {
            var image = AddImageToCanvas(sender, e, "6people.png");
            var tableFor6 = new TableFor6
            {
                Type = PlaceableEntityType.TableFor6,
                PositionX = Canvas.GetLeft(image),
                PositionY = Canvas.GetTop(image)
            };

            AddBindings(tableFor6, image);
            spaceViewModel.Add.Execute(tableFor6);
        }

        private void People18_Click(object sender, RoutedEventArgs e)
        {
            var image = AddImageToCanvas(sender, e, "18people.png");
            var tableFor18 = new TableFor18
            {
                Type = PlaceableEntityType.TableFor18,
                PositionX = Canvas.GetLeft(image),
                PositionY = Canvas.GetTop(image)
            };

            AddBindings(tableFor18, image);
            spaceViewModel.Add.Execute(tableFor18);
        }

        private void Music_Click(object sender, RoutedEventArgs e)
        {
            var image = AddImageToCanvas(sender, e, "music.png");
            var music = new Music
            {
                Type = PlaceableEntityType.Music,
                PositionX = Canvas.GetLeft(image),
                PositionY = Canvas.GetTop(image)
            };

            AddBindings(music, image);
            spaceViewModel.Add.Execute(music);
        }

        private void Empty_Click(object sender, RoutedEventArgs e)
        {
            var image = AddImageToCanvas(sender, e, "empty.png");
            var servingTable = new ServingTable
            {
                Type = PlaceableEntityType.Empty,
                PositionX = Canvas.GetLeft(image),
                PositionY = Canvas.GetTop(image)
            };

            AddBindings(servingTable, image);
            spaceViewModel.Add.Execute(servingTable);
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

        private Image AddImageToCanvas(object sender, RoutedEventArgs e, string imageName)
        {
            Image image = new Image
            {
                Source = new BitmapImage(new Uri($"pack://siteoforigin:,,,/Resources/{imageName}")),
                Width = 130,
                Height = 80
            };
            image.PreviewMouseLeftButtonDown += Ellipse_PreviewMouseLeftButtonDown;

            Canvas.SetLeft(image, (MainCanvas.ActualWidth / 2) - (image.Width / 2));
            Canvas.SetTop(image, (MainCanvas.ActualHeight / 2) - (image.Height / 2));

            var contextMenu = new ContextMenu();
            var deleteMenuItem = new MenuItem()
            {
                Header = "Ukloni",
            };

            deleteMenuItem.Click += (object deleteSender, RoutedEventArgs deleteE) =>
            {
                var index = _images.IndexOf(image);
                _images.RemoveAt(index);
                spaceViewModel.Remove.Execute(index);
                MainCanvas.Children.Remove(image);
            };

            contextMenu.Items.Add(deleteMenuItem);
            image.ContextMenu = contextMenu;

            _images.Add(image);
            MainCanvas.Children.Add(image);

            return image;
        }

        #region DragAndDrop

        private UIElement dragObject = null;
        private Point offset;

        private void Ellipse_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragObject = sender as UIElement;
            offset = e.GetPosition(MainCanvas);
            offset.Y -= Canvas.GetTop(dragObject);
            offset.X -= Canvas.GetLeft(dragObject);
        }

        private void MainCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (dragObject == null)
            {
                return;
            }

            var position = e.GetPosition(sender as IInputElement);
            Canvas.SetTop(dragObject, position.Y - offset.Y);
            Canvas.SetLeft(dragObject, position.X - offset.X);
        }

        private void MainCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dragObject = null;
        }

        #endregion DragAndDrop
    }
}