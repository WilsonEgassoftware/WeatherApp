using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Respository;

namespace WeatherApp.ViewModels
{
    class WeatherViewModel
    {
        private readonly WeatherRepository _repository = new();

        public string Time { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string Rain { get; set; }

        public async Task LoadWeatherAsync()
        {
            var (lat, lon) = await _repository.GetCurrentLocationAsync();
            var weather = await _repository.GetWeatherAsync(lat, lon);

            Time = weather.Time;
            Temperature = $"{weather.Temperature} °C";
            Humidity = $"{weather.Humidity} %";
            Rain = $"{weather.Rain} mm";
            OnPropertyChanged(nameof(Time));
            OnPropertyChanged(nameof(Temperature));
            OnPropertyChanged(nameof(Humidity));
            OnPropertyChanged(nameof(Rain));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
