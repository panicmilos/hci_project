using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Service;
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
    /// Interaction logic for SpacePreviewView.xaml
    /// </summary>
    public partial class SpacePreviewView : UserControl
    {
        private readonly SpacePreviewMode _mode;
        private readonly IList<Image> _images;
        private readonly IDictionary<UIElement, PlaceableEntity> _imageWithPlaceableEntities;

        private readonly IDialogService _dialogService;

        public SpacePreviewView()
        {
            InitializeComponent();

            _mode = GlobalStore.ReadAndRemoveObject<SpacePreviewMode>("spacePreviewMode");
            _images = new List<Image>();
            _imageWithPlaceableEntities = new Dictionary<UIElement, PlaceableEntity>();
            _dialogService = new DialogService();

            foreach (var placeableEntity in GlobalStore.ReadAndRemoveObject<List<PlaceableEntity>>("placeableEntities"))
            {
                var image = AddImageToCanvas(placeableEntity.ImageName);
                AddBindings(placeableEntity, image);
                _imageWithPlaceableEntities.Add(image, placeableEntity);
            }
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

        private Image AddImageToCanvas(string imageName)
        {
            var image = new Image
            {
                Source = new BitmapImage(new Uri($"pack://siteoforigin:,,,/Resources/{imageName}")),
                Width = 130,
                Height = 80
            };
            if (_mode == SpacePreviewMode.Edit)
            {
                image.Cursor = Cursors.Hand;
                image.PreviewMouseLeftButtonDown += Image_PreviewMouseLeftButtonDown;
            }
            image.MouseLeftButtonDown += Image_MouseLeftButtonDown;

            Canvas.SetLeft(image, (MainCanvas.ActualWidth / 2) - (image.Width / 2));
            Canvas.SetTop(image, (MainCanvas.ActualHeight / 2) - (image.Height / 2));

            _images.Add(image);
            MainCanvas.Children.Add(image);

            return image;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
                return;

            if (_imageWithPlaceableEntities.TryGetValue(sender as UIElement, out var placeableEntity) && placeableEntity is DinningTable dinningTable)
            {
                var dinningTableCopy = dinningTable.Clone() as DinningTable;
                var editedDinningTable = _dialogService.OpenDialog(new PlacingGuestsDialogViewModel(dinningTableCopy, _dialogService, _mode));
                if (editedDinningTable != null)
                {
                    dinningTable.Guests = editedDinningTable.Guests;
                }
                dragObject = null;
            }
        }

        #region DragAndDrop

        private UIElement dragObject = null;
        private Point offset;

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_imageWithPlaceableEntities.TryGetValue(sender as UIElement, out var placeableEntity))
            {
                if (!placeableEntity.Movable)
                {
                    return;
                }
            }

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