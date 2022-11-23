using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class ApiService
    {
        HttpClient _client;

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public ApiService()
        {
            _client= new HttpClient();
        }

        public async Task<WeatherData> GetWeatherDataAsync(string query)
        {
            WeatherData weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
            return weatherData;
        }

        public async Task<Location> GetCurrentLocation()
        {
            Location location = null;
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                _isCheckingLocation = false;
            }
            return location;
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }
}
