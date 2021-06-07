using GMap.NET;
using GMap.NET.MapProviders;
using Organizator_Proslava.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            EventBus.RegisterHandler("OpenAddresses", () => addresses.IsDropDownOpen = true);
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