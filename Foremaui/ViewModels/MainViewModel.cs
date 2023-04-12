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
using Foremaui;
using static Android.Content.ClipData;

namespace Foremaui.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        static readonly HttpClient client = new();
        private CancellationTokenSource _cancelTokenSource;

        //today page
        [ObservableProperty]
        string city;

        [ObservableProperty]
        double currentUserLat;

        [ObservableProperty]
        double currentUserLon;

        [ObservableProperty]
        double currentUserAlt;


        [ObservableProperty]
        Forecast forecast;



        Models.Daily dailyFc;

        //user fav
        [ObservableProperty]
        string defaultCity;



        //control
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        bool isGettingUserLocation;

        [ObservableProperty]
        bool isGettingUserForecast;

        public MainViewModel()
        {
            //if (Preferences.Default.Get("use_gps_on_startup", false))
            //{
            //   GetUserForecastAsync();
            //}
            //else
            //{
            //    //GetCityForecastAsync();
            //}
            GetCityForecastAsync();
        }


        //not commands
        public static DateTime UnixTimeStampToDateTime(long unixTime)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTime = unixEpoch.AddSeconds(unixTime);

            return dateTime;
        }

        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                IsGettingUserForecast = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    CurrentUserLat = location.Latitude;
                    CurrentUserLon = location.Longitude;
                    CurrentUserAlt = (double)location.Altitude;

                    return location;
                }
            }
            catch (Exception) { }
            finally { IsGettingUserLocation = false; }

            return new Location();
        }



        async Task UpdateForecastAsync(double lat, double lon, string units, string lang)
        {
            try
            {
                FormattableString addressUrlFormattable = $"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude=minutely&units={units}&lang={lang}&appid=53e87574620ead6e4300024c0934a5f2";
                string addressUrl = FormattableString.Invariant(addressUrlFormattable);
                var response = await client.GetAsync($"{addressUrlFormattable}");

                if (response.IsSuccessStatusCode)
                {
                    var forecast = await response.Content.ReadFromJsonAsync<Forecast>();
                    if (forecast != null)
                    {
                        if (forecast.Current != null)
                        {
                            Forecast = forecast;
                            Forecast.Current.Date = UnixTimeStampToDateTime(Forecast.Current.Dt);
                            Forecast.Current.SunRiseDt = UnixTimeStampToDateTime(Forecast.Current.Sunrise);
                            Forecast.Current.SunSetDt = UnixTimeStampToDateTime(Forecast.Current.Sunset);
                            foreach (var item in Forecast.Hourly)
                            {
                                item.Date = UnixTimeStampToDateTime(item.Dt);
                            }
                            foreach (var item in forecast.Daily)
                            {
                                item.Date = UnixTimeStampToDateTime(item.Dt);
                                item.SunRiseDt= UnixTimeStampToDateTime(item.Sunrise);
                                item.SunSetDt= UnixTimeStampToDateTime(item.Sunset);
                                item.MoonSetDt = UnixTimeStampToDateTime(item.Moonset);
                                item.MoonRiseDt = UnixTimeStampToDateTime(item.Moonrise);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            finally { IsRefreshing = false; }
        }



        //commands

        //called when user types in the city name
        [RelayCommand]
        async Task GetCityForecastAsync()
        {
            IsGettingUserLocation = true;

            Foremaui.Platforms.Android.Resources.KeyboardHelper.HideKeyboard ();

            string city = City;
            string cityName = HttpUtility.UrlEncode(city);
            string countryCode = "it";
            int limit = 5;
            string geocodingUrl = $"https://api.openweathermap.org/geo/1.0/direct?q={cityName},{countryCode}&limit={limit}&appid=53e87574620ead6e4300024c0934a5f2";

            try
            {
                HttpResponseMessage responseGeocoding = await client.GetAsync($"{geocodingUrl}");
                if (responseGeocoding.IsSuccessStatusCode)
                {
                    List<GPS> geocodingResult = await responseGeocoding.Content.ReadFromJsonAsync<List<GPS>>();
                    if (geocodingResult != null && geocodingResult.Count > 0)
                    {
                        double lat = geocodingResult[0].Lat;
                        double lon = geocodingResult[0].Lon;
                        string units = "metric";
                        string lang = "en";

                        await UpdateForecastAsync(lat, lon, units, lang);
                    }
                }
            }
            catch (Exception) { }
            finally { IsGettingUserLocation = false; }
        }


        //called when user asks for current position weather
        [RelayCommand]
        async Task GetUserForecastAsync()
        {
            IsGettingUserLocation = true;
            IsGettingUserForecast = true;

            var location = await GetCurrentLocation();

            IsGettingUserLocation = false;
            string reverseGeocodingUrl = $"https://api.openweathermap.org/geo/1.0/reverse?lat={location.Latitude}&lon={location.Longitude}&limit=1&appid=53e87574620ead6e4300024c0934a5f2";



            try
            {
                HttpResponseMessage responseReverseGeocoding = await client.GetAsync(reverseGeocodingUrl);
                if (responseReverseGeocoding.IsSuccessStatusCode)
                {
                    List<ReverseGeocoding> reverseGeocodingResult = await responseReverseGeocoding.Content.ReadFromJsonAsync<List<ReverseGeocoding>>();
                    if (reverseGeocodingResult != null)
                    {
                        City = $"{reverseGeocodingResult[0].Name}, {reverseGeocodingResult[0].Country}";

                        double lat = location.Latitude;
                        double lon = location.Longitude;
                        string units = "metric";
                        string lang = "en";
                     
                        await UpdateForecastAsync(lat, lon, units, lang);
                    }
                }
            }
            catch (Exception) { }
            finally { IsGettingUserForecast = false; }
        }


        [RelayCommand]
        async Task GoToDetails(Models.Daily daily)
        {
            if (daily == null) return;
            
            
            await App.Current.MainPage.Navigation.PushAsync(new MoreInfoDailyPage(daily));
        }


        
        
    }
}
