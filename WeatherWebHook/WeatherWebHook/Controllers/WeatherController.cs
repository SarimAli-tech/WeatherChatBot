using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using WeatherWebHook.Model;

namespace WeatherWebHook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WeatherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather([FromBody] DialogflowRequest request)
        {
            try
            {
                var city = request.QueryResult.Parameters["geo-city"]?.ToString();
                var date = request.QueryResult.Parameters.ContainsKey("date")
                    ? request.QueryResult.Parameters["date"].ToString()
                    : null;
                if (string.IsNullOrEmpty(city))
                {
                    return BadRequest(new { fulfillmentText = "Please specify a city to get the weather forecast." });
                }

                string apiKey = "8de057868998c85b3c53c0b6db738cfd";
                string apiUrl = "";
                if (string.IsNullOrEmpty(date))
                {
                    apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
                }
                else
                {
                    apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&cnt=8&appid={apiKey}&units=metric";
                }
                var response = await _httpClient.GetStringAsync(apiUrl);
                var weatherData = JObject.Parse(response);
                string message = "";

                if (string.IsNullOrEmpty(date))
                {
                    string temperature = weatherData["main"]["temp"].ToString();
                    string description = weatherData["weather"][0]["description"].ToString();
                    message = $"The current weather in {city} is {temperature}°C with {description}.";
                }
                else
                {
                    message = GetForecastWeatherMessage(weatherData, city, date);
                }

                var dialogflowResponse = new
                {
                    fulfillmentText = message
                };

                return Ok(dialogflowResponse);
            }
            catch (HttpRequestException)
            {
                return BadRequest(new { fulfillmentText = "Sorry, I couldn't retrieve the weather data. Please try again later." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { fulfillmentText = $"An error occurred: {ex.Message}" });
            }
        }
        private string GetForecastWeatherMessage(JObject weatherData, string city, string date)
        {
            if (!DateTime.TryParse(date, out DateTime targetDate))
            {
                return "Invalid date format. Please use a valid date (e.g., '2024-11-09').";
            }

            if (weatherData["list"] == null || !weatherData["list"].HasValues)
            {
                return $"No forecast data available for {city}.";
            }

            foreach (var forecast in weatherData["list"])
            {
                if (DateTime.TryParse(forecast["dt_txt"]?.ToString(), out DateTime forecastDate) &&
                    forecastDate.Date == targetDate.Date)
                {
                    var temp = forecast["main"]?["temp"]?.ToString();
                    var description = forecast["weather"]?[0]?["description"]?.ToString();

                    string formattedDate = targetDate.ToString("yyyy-MM-dd");

                    if (!string.IsNullOrEmpty(temp) && !string.IsNullOrEmpty(description))
                    {
                        return $"The forecasted weather for {city} on {formattedDate} is {temp}°C with {description}.";
                    }

                    return "Quick response: Data missing for temperature or description.";
                }
            }

            return $"Unable to retrieve forecasted weather for {city} on {date}.";
        }
    }
}
