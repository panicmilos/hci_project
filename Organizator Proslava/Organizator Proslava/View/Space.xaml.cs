using Organizator_Proslava.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        }

        private void People6_Click(object sender, RoutedEventArgs e)
        {
            var image = AddImageToCanvas(sender, e, "6people.png");
            spaceViewModel.AddTableFor6.Execute(image);
        }

        private void People18_Click(object sender, RoutedEventArgs e)
        {
            var image = AddImageToCanvas(sender, e, "18people.png");
            spaceViewModel.AddTableFor18.Execute(image);
        }

        private void Music_Click(object sender, RoutedEventArgs e)
        {
            var image = AddImageToCanvas(sender, e, "music.png");
            spaceViewModel.AddMusic.Execute(image);
        }

        private void Empty_Click(object sender, RoutedEventArgs e)
        {
            var image = AddImageToCanvas(sender, e, "empty.png");
            spaceViewModel.AddServingTable.Execute(image);
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
    }
}