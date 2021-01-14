using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public IEnumerable<MeteoCity> GetCitiesList()
        {
            return JsonConvert.DeserializeObject<IEnumerable<MeteoCity>>(MeteoAPI.GetCities());
        }

        [HttpGet("mylist")]
        public IEnumerable<object> GetCitiesWeather()
        {
            // take list from the DB
            var citiesData = _context.City.ToList();

            List<MyList> myList = new List<MyList>(); 
            foreach (var code in citiesData)
            {
                var data = JsonConvert.DeserializeObject<MeteoCityInfo>(MeteoAPI.GetCityWeatherForecast(code.Code));
                if (data != null)
                {
                    MyList newItem = new MyList();
                    newItem.Id = code.Id;
                    newItem.CityName = data.Place.Name;
                    newItem.Description = code.Description;
                    newItem.MaxTemp = data.ForecastTimestamps.Max(o => o.AirTemperature);
                    newItem.MinTemp = data.ForecastTimestamps.Min(o => o.AirTemperature);
                    myList.Add(newItem);
                }
            }

            return myList;
        }

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
                return "Failed";
            }
        }

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