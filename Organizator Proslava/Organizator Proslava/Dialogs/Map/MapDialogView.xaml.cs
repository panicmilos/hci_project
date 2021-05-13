using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Organizator_Proslava.Dialogs.Map
{
    /// <summary>
    /// Interaction logic for MapDialogView.xaml
    /// </summary>
    public partial class MapDialogView : UserControl
    {
        private readonly PointLatLng _defaultPosition = new PointLatLng(44.374229f, 19.105961f);

        public MapDialogView()
        {
            InitializeComponent();

            GMapProviders.GoogleMap.ApiKey = "AIzaSyDbTdydamfpLVkg73WVEYSXLbIop0iaCpE";

            map.MapProvider = GMapProviders.GoogleMap;
            map.Position = _defaultPosition;
            map.MinZoom = 1;
            map.MaxZoom = 100;
            map.Zoom = 15;
            map.ShowCenter = false;
            map.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);

            addresses.ItemFilter += SearchAddress;
        }

        private void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickedPoint = e.GetPosition(map);
            PointLatLng point = map.FromLocalToLatLng((int)clickedPoint.X, (int)clickedPoint.Y);
            map.Position = point;
        }

        private bool SearchAddress(string search, object value)
        {
            return true;
        }
    }
}