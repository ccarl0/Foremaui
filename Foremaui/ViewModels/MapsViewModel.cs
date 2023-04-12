using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foremaui.ViewModels
{


    public partial class MapsViewModel : ObservableObject
    {
        private CancellationTokenSource _cancelTokenSource;


        [ObservableProperty]
        string bingAPI;

        public MapsViewModel()
        {
            ShowMapAsync();
        }

        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    return location;
                    
                }
            }
            catch (Exception) { }

            return new Location();
        }



        [RelayCommand]
        async void ShowMapAsync()
        {
            var loc = await GetCurrentLocation();

            BingAPI = $"https://dev.virtualearth.net/REST/v1/Imagery/Map/Aerial/{loc.Latitude},{loc.Longitude}/19?mapSize=2048,%202048&key=Arr6MeF-fiobL58CkmD9JI3ooJIScLiEWy-SeV7bQMwfYdwEm06Odl2pnxjY_wO9";

        }
    }
}
