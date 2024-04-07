using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "0cac16f2dd944a669771031b21603dda"; 

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherForecast> GetWeatherForecastAsync(string city)
    {
        var response = await _httpClient.GetFromJsonAsync<WeatherForecast>(
            $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
        
        if (response != null)
        {
            // Mapping additional details from the response
            response.WindSpeed = response.Wind.Speed;
            response.Humidity = response.Main.Humidity;
            response.Condition = response.Weather[0].Main;
        }

        return response;
    }
}

public class WeatherForecast
{
    public string Name { get; set; }
    public Main Main { get; set; }
    public Wind Wind { get; set; }
    public List<Weather> Weather { get; set; }
    public double WindSpeed { get; set; } // Added for detailed report
    public int Humidity { get; set; } // Added for detailed report
    public string Condition { get; set; } // Added for detailed report
}

public class Main
{
    public double Temp { get; set; }
    public int Humidity { get; set; }
}

public class Wind
{
    public double Speed { get; set; }
}

public class Weather
{
    public string Main { get; set; }
    public string Description { get; set; }
}
