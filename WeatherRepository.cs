using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp.Respository
{
    class WeatherRepository
    {
        private readonly HttpClient _client = new();

        public async Task<(double latitude, double longitude)> GetCurrentLocationAsync()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync();
            }
            return (location.Latitude, location.Longitude);
        }

        public async Task<WeatherData> GetWeatherAsync(double latitude, double longitude)
        {
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,rain";
            var response = await _client.GetStringAsync(url);
            var json = JsonSerializer.Deserialize<WeatherApiResponse>(response);
            return new WeatherData
            {
                Time = DateTime.Now.ToString("HH:mm"),
                Temperature = json.current.temperature_2m,
                Humidity = json.current.relative_humidity_2m,
                Rain = json.current.rain
            };
        }
    }
}
