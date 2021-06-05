using Organizator_Proslava.Utility;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Organizator_Proslava.View.DemoForm
{
    /// <summary>
    /// Interaction logic for DemoView.xaml
    /// </summary>
    public partial class DemoView : UserControl
    {
        private bool fullscreen = false;

        public DemoView()
        {
            InitializeComponent();

            EventBus.RegisterHandler("demoFunctinalityChanged", OnDemoFunctinalityChanged);
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!fullscreen)
            {
                fullscreen = true;
                EventBus.FireEvent("DemoFullscreenMode");
                LeftGridColum.Width = new GridLength(0);
                PlayButton.Visibility = Visibility.Hidden;
                VideoControl.Visibility = Visibility.Visible;
                VideoControl.Play();
            }
        }

        private void VideoControl_MediaEnded(object sender, RoutedEventArgs e)
        {
            VideoControl.Position = TimeSpan.Zero;
            VideoControl.Play();
        }

        private void OnDemoFunctinalityChanged()
        {
            VideoControl.Pause();
            VideoControl.Visibility = Visibility.Hidden;
            PlayButton.Visibility = Visibility.Visible;
        }

        private void VideoControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (fullscreen)
            {
                fullscreen = false;
                EventBus.FireEvent("ExitDemoFullscreenMode");
                LeftGridColum.Width = new GridLength(3, GridUnitType.Star);
                VideoControl.Pause();
                VideoControl.Visibility = Visibility.Hidden;
                PlayButton.Visibility = Visibility.Visible;
            }
        }
    }
}