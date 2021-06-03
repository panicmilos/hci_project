using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private SpaceViewModel SpaceViewModel { get => DataContext as SpaceViewModel; }
        private readonly IList<Image> _images;

        private readonly IDialogService _dialogService;
        private IDictionary<UIElement, PlaceableEntity> _imageWithPlaceableEntities;

        public Space()
        {
            InitializeComponent();

            _dialogService = new DialogService();
            _images = new List<Image>();
            _imageWithPlaceableEntities = new Dictionary<UIElement, PlaceableEntity>();

            foreach (var placeableEntity in GlobalStore.ReadObject<List<PlaceableEntity>>("placeableEntities"))
            {
                var image = AddImageToCanvas(placeableEntity.ImageName);
                AddBindings(placeableEntity, image);
                _imageWithPlaceableEntities.Add(image, placeableEntity);
            }

            GlobalStore.RemoveObject("placeableEntities");
        }

        private void People6_Click(object sender, RoutedEventArgs e)
        {
            if (dragObject != null)
            {
                return;
            }

            var tableFor6 = _dialogService.OpenDialog(new DinningTableDialogViewModel(new TableFor6(), false));
            if (tableFor6 == null)
            {
                return;
            }

            var image = AddImageToCanvas("6people.png");
            tableFor6.Type = PlaceableEntityType.TableFor6;
            tableFor6.PositionX = Canvas.GetLeft(image);
            tableFor6.PositionY = Canvas.GetTop(image);

            AddBindings(tableFor6, image);
            _imageWithPlaceableEntities.Add(image, tableFor6);
            SpaceViewModel.Add.Execute(tableFor6);
        }

        private void People18_Click(object sender, RoutedEventArgs e)
        {
            if (dragObject != null)
            {
                return;
            }

            var tableFor18 = _dialogService.OpenDialog(new DinningTableDialogViewModel(new TableFor18(), false));
            if (tableFor18 == null)
            {
                return;
            }

            var image = AddImageToCanvas("18people.png");
            tableFor18.Type = PlaceableEntityType.TableFor18;
            tableFor18.PositionX = Canvas.GetLeft(image);
            tableFor18.PositionY = Canvas.GetTop(image);

            AddBindings(tableFor18, image);
            _imageWithPlaceableEntities.Add(image, tableFor18);
            SpaceViewModel.Add.Execute(tableFor18);
        }

        private void Music_Click(object sender, RoutedEventArgs e)
        {
            if (dragObject != null)
            {
                return;
            }

            var music = _dialogService.OpenDialog(new NonDinningTableDialogViewModel(new Music(), false));
            if (music == null)
            {
                return;
            }

            var image = AddImageToCanvas("music.png");
            music.Type = PlaceableEntityType.Music;
            music.PositionX = Canvas.GetLeft(image);
            music.PositionY = Canvas.GetTop(image);

            AddBindings(music, image);
            _imageWithPlaceableEntities.Add(image, music);
            SpaceViewModel.Add.Execute(music);
        }

        private void Empty_Click(object sender, RoutedEventArgs e)
        {
            if (dragObject != null)
            {
                return;
            }

            var servingTable = _dialogService.OpenDialog(new NonDinningTableDialogViewModel(new ServingTable(), false));
            if (servingTable == null)
            {
                return;
            }

            var image = AddImageToCanvas("empty.png");
            servingTable.Type = PlaceableEntityType.Empty;
            servingTable.PositionX = Canvas.GetLeft(image);
            servingTable.PositionY = Canvas.GetTop(image);

            AddBindings(servingTable, image);
            _imageWithPlaceableEntities.Add(image, servingTable);
            SpaceViewModel.Add.Execute(servingTable);
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
                Height = 80,
                Cursor = Cursors.Hand
            };
            image.PreviewMouseLeftButtonDown += Image_PreviewMouseLeftButtonDown;
            image.MouseLeftButtonDown += Image_MouseLeftButtonDown;

            Canvas.SetLeft(image, (MainCanvas.ActualWidth / 2) - (image.Width / 2));
            Canvas.SetTop(image, (MainCanvas.ActualHeight / 2) - (image.Height / 2));

            AddContextMenu(image);
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
                _dialogService.OpenDialog(new PlacingGuestsDialogViewModel(dinningTable));
                dragObject = null;
            }
        }

        private void AddContextMenu(Image image)
        {
            var contextMenu = new ContextMenu();
            var editMenuItem = new MenuItem()
            {
                Header = "Izmeni"
            };

            editMenuItem.Click += (object editSender, RoutedEventArgs editE) =>
            {
                if (_imageWithPlaceableEntities[image] is DinningTable dinningTable)
                {
                    var dinningTableCopy = dinningTable.Clone() as DinningTable;
                    var editedDinningTable = _dialogService.OpenDialog(new DinningTableDialogViewModel(dinningTableCopy, true));
                    if (editedDinningTable != null)
                    {
                        dinningTable.Movable = editedDinningTable.Movable;
                        dinningTable.Seats = editedDinningTable.Seats;
                    }
                }
                else
                {
                    var nonDinngingTable = _imageWithPlaceableEntities[image];

                    var nonDinngingTableCopy = nonDinngingTable.Clone();
                    var editedNonDinngingTableCopy = _dialogService.OpenDialog(new NonDinningTableDialogViewModel(nonDinngingTableCopy, true));
                    if (editedNonDinngingTableCopy != null)
                    {
                        nonDinngingTable.Movable = editedNonDinngingTableCopy.Movable;
                    }
                }
            };

            var deleteMenuItem = new MenuItem()
            {
                Header = "Ukloni",
            };

            deleteMenuItem.Click += (object deleteSender, RoutedEventArgs deleteE) =>
            {
                var index = _images.IndexOf(image);
                _images.RemoveAt(index);
                SpaceViewModel.Remove.Execute(index);
                _imageWithPlaceableEntities.Remove(image);
                MainCanvas.Children.Remove(image);
            };

            contextMenu.Items.Add(editMenuItem);
            contextMenu.Items.Add(deleteMenuItem);
            image.ContextMenu = contextMenu;
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