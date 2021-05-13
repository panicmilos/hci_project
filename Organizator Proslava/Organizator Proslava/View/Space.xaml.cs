using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Organizator_Proslava.View
{
    /// <summary>
    /// Interaction logic for Space.xaml
    /// </summary>
    public partial class Space : UserControl
    {
        public Space()
        {
            InitializeComponent();
        }

        private void Elipse_click(object sender, RoutedEventArgs e)
        {
            Ellipse ellipse = new Ellipse()
            {
                Height = 150,
                Width = 150,
                Stroke = Brushes.Black,
                StrokeThickness = 5,
                Fill = Brushes.Red
            };

            Canvas.SetLeft(ellipse, (MainCanvas.ActualWidth / 2) - (ellipse.Width / 2));
            Canvas.SetTop(ellipse, (MainCanvas.ActualHeight / 2) - (ellipse.Height / 2));

            ellipse.PreviewMouseLeftButtonDown += Ellipse_PreviewMouseLeftButtonDown;

            MainCanvas.Children.Add(ellipse);
        }

        private void Image_click(object sender, RoutedEventArgs e)
        {
            Image ellipse = new Image();
            ellipse.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/6people.png"));
            ellipse.Width = 90;
            ellipse.Height = 50;

            Canvas.SetLeft(ellipse, (MainCanvas.ActualWidth / 2) - (ellipse.Width / 2));
            Canvas.SetTop(ellipse, (MainCanvas.ActualHeight / 2) - (ellipse.Height / 2));

            ellipse.PreviewMouseLeftButtonDown += Ellipse_PreviewMouseLeftButtonDown;

            MainCanvas.Children.Add(ellipse);
        }

        private void Image2_click(object sender, RoutedEventArgs e)
        {
            Image ellipse = new Image();
            ellipse.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/18people.png"));
            ellipse.Width = 90;
            ellipse.Height = 50;

            Canvas.SetLeft(ellipse, (MainCanvas.ActualWidth / 2) - (ellipse.Width / 2));
            Canvas.SetTop(ellipse, (MainCanvas.ActualHeight / 2) - (ellipse.Height / 2));

            ellipse.PreviewMouseLeftButtonDown += Ellipse_PreviewMouseLeftButtonDown;

            MainCanvas.Children.Add(ellipse);
        }

        private void Image3_click(object sender, RoutedEventArgs e)
        {
            Image ellipse = new Image();
            ellipse.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/music.png"));
            ellipse.Width = 90;
            ellipse.Height = 50;

            Canvas.SetLeft(ellipse, (MainCanvas.ActualWidth / 2) - (ellipse.Width / 2));
            Canvas.SetTop(ellipse, (MainCanvas.ActualHeight / 2) - (ellipse.Height / 2));

            ellipse.PreviewMouseLeftButtonDown += Ellipse_PreviewMouseLeftButtonDown;

            MainCanvas.Children.Add(ellipse);
        }

        private void Image4_click(object sender, RoutedEventArgs e)
        {
            Image ellipse = new Image();
            ellipse.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/empty.png"));
            ellipse.Width = 90;
            ellipse.Height = 50;

            Canvas.SetLeft(ellipse, (MainCanvas.ActualWidth / 2) - (ellipse.Width / 2));
            Canvas.SetTop(ellipse, (MainCanvas.ActualHeight / 2) - (ellipse.Height / 2));

            ellipse.PreviewMouseLeftButtonDown += Ellipse_PreviewMouseLeftButtonDown;

            MainCanvas.Children.Add(ellipse);
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