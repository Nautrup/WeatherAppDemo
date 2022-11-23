using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
	ApiService _apiService;

	public ImageSource IconSource { get; set; }
	public MainPage()
	{
		InitializeComponent();
		_apiService = new ApiService();
        StartUpLocationFromGPS();
	}

	private async void StartUpLocationFromGPS()
	{
		Location currentLocation =await _apiService.GetCurrentLocation();
        WeatherData weather = await _apiService.GetWeatherDataAsync(RequestUrl(ApiData.OpenWeatherApiUrl, currentLocation));
        BindingContext = weather;
    }

	public async void GetWeatherButtonClicked(object sender, EventArgs e)
	{
		WeatherData weather = await _apiService.GetWeatherDataAsync(RequestUrl(ApiData.OpenWeatherApiUrl));
        BindingContext = weather; 
	}

	private string RequestUrl(string endpoint)
	{
		string requestUrl = @$"{endpoint}?q={cityName.Text}&units=metric&lang=da&APPID={ApiData.OpenWeatherApiKey}";

		return requestUrl;
	}

    private string RequestUrl(string endpoint,Location location)
	{
        string requestUrl = @$"{endpoint}?lat={location.Latitude}&lon={location.Longitude}&units=metric&lang=da&appid={ApiData.OpenWeatherApiKey}";

        return requestUrl;
    }

}

