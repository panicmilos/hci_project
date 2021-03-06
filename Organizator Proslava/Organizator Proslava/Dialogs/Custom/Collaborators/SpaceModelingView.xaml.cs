using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    /// <summary>
    /// Interaction logic for SpaceModelingView.xaml
    /// </summary>
    public partial class SpaceModelingView : UserControl
    {
        private SpaceModelingViewModel SpaceModelingViewModel { get => DataContext as SpaceModelingViewModel; }
        private readonly IList<Image> _images;

        private readonly IDialogService _dialogService;
        private readonly IDictionary<UIElement, PlaceableEntity> _imageWithPlaceableEntities;

        public SpaceModelingView()
        {
            InitializeComponent();

            _dialogService = new DialogService();
            _images = new List<Image>();
            _imageWithPlaceableEntities = new Dictionary<UIElement, PlaceableEntity>();

            foreach (var placeableEntity in GlobalStore.ReadAndRemoveObject<List<PlaceableEntity>>("placeableEntities"))
            {
                var image = AddImageToCanvas(placeableEntity.ImageName);
                AddBindings(placeableEntity, image);
                _imageWithPlaceableEntities.Add(image, placeableEntity);
            }
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
            SpaceModelingViewModel.Add.Execute(tableFor6);
            InsertAddTableUserCommand(tableFor6, image);
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
            SpaceModelingViewModel.Add.Execute(tableFor18);
            InsertAddTableUserCommand(tableFor18, image);
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
            SpaceModelingViewModel.Add.Execute(music);
            InsertAddTableUserCommand(music, image);
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
            SpaceModelingViewModel.Add.Execute(servingTable);
            InsertAddTableUserCommand(servingTable, image);
        }

        private void InsertAddTableUserCommand(PlaceableEntity placeableEntity, Image image)
        {
            var addTableCommand = new AddTable(
                () =>
                {
                    var index = _images.IndexOf(image);
                    _images.RemoveAt(index);
                    SpaceModelingViewModel.Remove.Execute(index);
                    _imageWithPlaceableEntities.Remove(image);
                    MainCanvas.Children.Remove(image);
                },
                () =>
                {
                    _images.Add(image);
                    SpaceModelingViewModel.Add.Execute(placeableEntity);
                    _imageWithPlaceableEntities.Add(image, placeableEntity);
                    MainCanvas.Children.Add(image);
                });

            GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(addTableCommand);
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

            Canvas.SetLeft(image, (MainCanvas.ActualWidth / 2) - (image.Width / 2));
            Canvas.SetTop(image, (MainCanvas.ActualHeight / 2) - (image.Height / 2));

            AddContextMenu(image);
            _images.Add(image);
            MainCanvas.Children.Add(image);

            return image;
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
                        SpaceModelingViewModel.OnPropertyChanged("NumberOfGuests");
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
                InsertRemoveTableUserCommand(_imageWithPlaceableEntities[image], image);
                var index = _images.IndexOf(image);
                _images.RemoveAt(index);
                SpaceModelingViewModel.Remove.Execute(index);
                _imageWithPlaceableEntities.Remove(image);
                MainCanvas.Children.Remove(image);
            };

            contextMenu.Items.Add(editMenuItem);
            contextMenu.Items.Add(deleteMenuItem);
            image.ContextMenu = contextMenu;
        }

        private void InsertRemoveTableUserCommand(PlaceableEntity placeableEntity, Image image)
        {
            var removeTableCommand = new RemoveTable(
                () =>
                {
                    var index = _images.IndexOf(image);
                    _images.RemoveAt(index);
                    SpaceModelingViewModel.Remove.Execute(index);
                    _imageWithPlaceableEntities.Remove(image);
                    MainCanvas.Children.Remove(image);
                },
                () =>
                {
                    _images.Add(image);
                    SpaceModelingViewModel.Add.Execute(placeableEntity);
                    _imageWithPlaceableEntities.Add(image, placeableEntity);
                    MainCanvas.Children.Add(image);
                });

            GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(removeTableCommand);
        }

        #region DragAndDrop

        private UIElement dragObject = null;
        private UIElement copyDragObject = null;
        private Point startPoint;
        private Point endPoint;
        private Point offset;

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragObject = sender as UIElement;
            copyDragObject = sender as UIElement;

            offset = e.GetPosition(MainCanvas);
            offset.Y -= Canvas.GetTop(dragObject);
            offset.X -= Canvas.GetLeft(dragObject);

            startPoint = new Point(Canvas.GetLeft(dragObject), Canvas.GetTop(dragObject));
        }

        private void MainCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (dragObject == null)
            {
                return;
            }

            var image = dragObject as Image;
            var position = e.GetPosition(sender as IInputElement);

            var newTop = position.Y - offset.Y;
            var newLeft = position.X - offset.X;
            var shouldNullDragObject = false;

            if (newTop < 0)
            {
                newTop = 0;
                shouldNullDragObject = true;
            }
            else if (newTop + image.ActualHeight > MainCanvas.ActualHeight)
            {
                newTop = MainCanvas.ActualHeight - image.ActualHeight;
                shouldNullDragObject = true;
            }

            if (newLeft < 0)
            {
                newLeft = 0;
                shouldNullDragObject = true;
            }
            else if (newLeft + image.ActualWidth > MainCanvas.ActualWidth)
            {
                newLeft = MainCanvas.ActualWidth - image.ActualWidth;
                shouldNullDragObject = true;
            }

            if (IsInCollision(image, newTop, newLeft))
            {
                return;
            }

            Canvas.SetTop(dragObject, newTop);
            Canvas.SetLeft(dragObject, newLeft);

            endPoint = new Point { X = newLeft, Y = newTop };

            if (shouldNullDragObject)
            {
                dragObject = null;
            }
        }

        // Please skip this function if you ever read this file.
        private bool IsInCollision(Image image, double newTop, double newLeft)
        {
            foreach (var otherImage in _images)
            {
                if (otherImage == image)
                {
                    continue;
                }
                var haveByTop = false;
                var haveByLeft = false;

                var otherPositionTop = Canvas.GetTop(otherImage);
                var otherPositionLeft = Canvas.GetLeft(otherImage);
                if ((otherPositionTop <= newTop && newTop <= otherPositionTop + otherImage.ActualHeight) ||
                    (otherPositionTop <= (newTop + image.ActualHeight) && (newTop + image.ActualHeight) <= otherPositionTop + otherImage.ActualHeight) ||
                    (newTop <= otherPositionTop && otherPositionTop <= newTop + image.ActualHeight) ||
                    (newTop <= (otherPositionTop + otherImage.ActualHeight) && (otherPositionTop + otherImage.ActualHeight) <= newTop + image.ActualHeight))
                {
                    haveByTop = true;
                }

                if ((otherPositionLeft <= newLeft && newLeft <= otherPositionLeft + otherImage.ActualWidth) ||
                    (otherPositionLeft <= (newLeft + image.ActualWidth) && (newLeft + image.ActualWidth) <= otherPositionLeft + otherImage.ActualWidth) ||
                    (newLeft <= otherPositionLeft && otherPositionLeft <= newLeft + image.ActualWidth) ||
                    (newLeft <= (otherPositionLeft + otherImage.ActualWidth) && (otherPositionLeft + otherImage.ActualWidth) <= newLeft + image.ActualWidth))
                {
                    haveByLeft = true;
                }

                if (haveByLeft && haveByTop)
                {
                    return true;
                }
            }
            return false;
        }

        private void MainCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new MoveImage(copyDragObject, new Point(startPoint.X, startPoint.Y), new Point(endPoint.X, endPoint.Y)));
            copyDragObject = null;
            dragObject = null;
        }

        #endregion DragAndDrop
    }
}