using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CommunityToolkit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Foremaui.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using Foremaui.Views;
using System.Reflection.PortableExecutable;
using Foremaui.Platforms.Android;
using Foremaui.Helpers;
using Android.Preferences;
using Android.Views.Inspectors;
using Java.Nio.FileNio.Attributes;

namespace Foremaui.ViewModels
{
    public partial class SettingsViweModel : ObservableObject
    {
        static readonly HttpClient client = new HttpClient();
        private CancellationTokenSource _cancelTokenSource;


        [ObservableProperty]
        double defaultLat;

        [ObservableProperty]
        double defaultLon;

        [ObservableProperty]
        double defaultAlt;

        [ObservableProperty]
        string defaultCity;

        [ObservableProperty]
        bool isGettingUserLocation;

        [ObservableProperty]
        bool isSettingUserDefaultCity;

        [RelayCommand]
        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                IsGettingUserLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    DefaultLat = location.Latitude;
                    //CurrentUserLon = location.Longitude;
                    //CurrentUserAlt = (double)location.Altitude;

                    return location;
                }
            }
            catch (Exception) { }
            finally { IsGettingUserLocation = false; }

            return new Location();
        }

        [RelayCommand]
        public async Task setDefaultCityByLocation()
        {
            IsSettingUserDefaultCity = true;

            var location = await GetCurrentLocation();
            string reverseGeocodingUrl = $"https://api.openweathermap.org/geo/1.0/reverse?lat={location.Latitude}&lon={location.Longitude}&limit=1&appid=53e87574620ead6e4300024c0934a5f2";

            try
            {
                HttpResponseMessage responseReverseGeocoding = await client.GetAsync(reverseGeocodingUrl);
                if (responseReverseGeocoding.IsSuccessStatusCode)
                {

                    List<ReverseGeocoding> reverseGeocodingResult = await responseReverseGeocoding.Content.ReadFromJsonAsync<List<ReverseGeocoding>>();
                    if (reverseGeocodingResult != null)
                    {
                        DefaultCity = $"{reverseGeocodingResult[0].Name}, {reverseGeocodingResult[0].Country}";

                        DefaultLat = location.Latitude;
                        DefaultLon = location.Longitude;

                        Preferences.Default.Set("defaultCity", DefaultCity);
                    }
                }
            }
            catch (Exception) { }
            finally 
            { 
                IsSettingUserDefaultCity = false;
                await Application.Current.MainPage.DisplayAlert("New Default City Saved!", DefaultCity, "Next");
            }
        }

        [RelayCommand]
        public async Task SetDefaultCityByEntry()
        {
            Foremaui.Platforms.Android.Resources.KeyboardHelper.HideKeyboard();

            if (string.IsNullOrWhiteSpace(DefaultCity))
            {
                await Application.Current.MainPage.DisplayAlert("Cannot save City", DefaultCity, "Next");
                return;
            }
            try
            {
                Preferences.Default.Set("fav_city", DefaultCity);
            }
            catch (Exception) { }
            finally
            {
                IsSettingUserDefaultCity = false;
                await Application.Current.MainPage.DisplayAlert("New Default City Saved!", DefaultCity, "Next");
            }
        }

    }
}
