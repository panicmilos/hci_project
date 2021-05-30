using GMap.NET;
using GMap.NET.WindowsPresentation;
using Newtonsoft.Json.Linq;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Map
{
    public class MapDialogViewModel : DialogViewModelBase<Address>
    {
        private static readonly HttpClient client = new HttpClient();

        public ICommand Search { get; set; }
        public ObservableCollection<string> WholeAddresses { get; set; }
        public IList<Address> Addresses { get; set; }

        public Address SelectedAddress;
        private MapControl _map;

        private string _inputedAddress;
        public string InputedAddress { get => _inputedAddress; set => OnInputedAddresChanged(value); }

        private string _errorMessage;
        public string ErrorMessage { get => _errorMessage; set => OnPropertyChanged(ref _errorMessage, value); }

        public ICommand Choose { get; set; }
        public ICommand Back { get; set; }

        public MapDialogViewModel(string title) :
            base(title, 560, 500)
        {
            Search = new RelayCommand<MapControl>(SearchAddressesAsync);
            WholeAddresses = new ObservableCollection<string>();
            Addresses = new List<Address>();

            Choose = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, SelectedAddress));
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, null));
        }

        private void OnInputedAddresChanged(string value)
        {
            OnPropertyChanged(ref _inputedAddress, value);
            SelectedAddress = Addresses.FirstOrDefault(a => a.WholeAddress == InputedAddress);

            if (SelectedAddress == null || _map == null)
            {
                return;
            }

            _map.Markers.Clear();
            var markerPosiiton = new PointLatLng(SelectedAddress.Lat, SelectedAddress.Lng);

            var marker = new GMapMarker(markerPosiiton)
            {
                Shape = new PinControl()
            };
            _map.Markers.Add(marker);
            _map.Position = markerPosiiton;
        }

        public async void SearchAddressesAsync(MapControl map)
        {
            _map = map;

            ErrorMessage = string.Empty;
            WholeAddresses.Clear();

            Addresses = await FetchAddresses();
            Addresses.Select(a => a.WholeAddress).ToList().ForEach(a => WholeAddresses.Add(a));
        }

        private async Task<IList<Address>> FetchAddresses()
        {
            IList<Address> fetchedAddresses = new List<Address>();

            var responseBody = await SendRequest();
            if (responseBody == "[]")
            {
                ErrorMessage = "Nema rezultata pretrage. Proverite da li ste uneli dobru adresu.";
            }
            else
            {
                var jsonAddresses = JArray.Parse(responseBody);
                fetchedAddresses = ConvertJArrayOfAddressesToList(jsonAddresses);
            }

            return fetchedAddresses;
        }

        private async Task<string> SendRequest()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search.php?q=${InputedAddress}&polygon_geojson=1&format=jsonv2&limit=5");
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "1.0"));

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            return "[]";
        }

        private IList<Address> ConvertJArrayOfAddressesToList(JArray jsonAddresses)
        {
            return jsonAddresses.Select(jsonAddress => new Address
            {
                WholeAddress = jsonAddress["display_name"].ToObject<string>(),
                Lat = jsonAddress["lat"].ToObject<float>(),
                Lng = jsonAddress["lon"].ToObject<float>()
            }).ToList();
        }
    }
}