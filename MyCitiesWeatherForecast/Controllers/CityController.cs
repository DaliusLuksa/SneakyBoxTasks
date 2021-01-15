using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyCitiesWeatherForecast.API;
using MyCitiesWeatherForecast.Entities;
using MyCitiesWeatherForecast.Helpers;
using Newtonsoft.Json;

namespace MyCitiesWeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : Controller
    {
        private readonly CitiesDBContext _context;

        public CityController(CitiesDBContext context)
        {
            _context = context;
        }

        // Method for getting cities list from the MeteoAPI
        [HttpGet]
        public IEnumerable<MeteoCity> GetCitiesList()
        {
            return JsonConvert.DeserializeObject<IEnumerable<MeteoCity>>(MeteoAPI.GetCities());
        }

        // Method for getting cities weather information
        [HttpGet("mylist")]
        public IEnumerable<object> GetCitiesWeather()
        {
            // take list from the DB
            var citiesData = _context.City.ToList();

            // populate new list with min and max temps from the MeteoAPI
            List<MyList> myList = new List<MyList>(); 
            foreach (var city in citiesData)
            {
                var data = JsonConvert.DeserializeObject<MeteoCityInfo>(MeteoAPI.GetCityWeatherForecast(city.Code));
                if (data != null)
                {
                    MyList newItem = new MyList();
                    newItem.Id = city.Id;
                    newItem.CityName = data.Place.Name;
                    newItem.Description = city.Description;
                    newItem.MaxTemp = data.ForecastTimestamps.Max(o => o.AirTemperature);
                    newItem.MinTemp = data.ForecastTimestamps.Min(o => o.AirTemperature);
                    myList.Add(newItem);
                }
            }

            return myList;
        }

        // Method for adding new city to the database
        [HttpPost]
        public string AddNewCity(City newCity)
        {
            try
            {
                var cityInfo = JsonConvert.DeserializeObject<MeteoCity>(MeteoAPI.GetCityInfo(newCity.Code));
                newCity.Name = cityInfo.Name;

                _context.City.Add(newCity);
                _context.SaveChanges();

                return "Created";
            }
            catch
            {
                return "Failed to add new city to the list.";
            }
        }

        // Method for deleting existing city from the database
        [HttpDelete("{id}")]
        public string DeleteCity([FromRoute] int id)
        {
            try
            {
                var cityToDelete = _context.City.Where(o => o.Id == id).FirstOrDefault();
                _context.Remove(cityToDelete);
                _context.SaveChanges();

                return "Deleted";
            }
            catch
            {
                return "Failed to delete city";
            }
        }

        // Method for deleting all cities from the database
        [HttpDelete()]
        public string DeleteAllCities()
        {
            try
            {
                City[] cities = _context.City.ToArray();
                _context.City.RemoveRange(cities);
                _context.SaveChanges();

                return "Deleted";
            }
            catch
            {
                return "Failed to delete city list";
            }
        }
    }
}