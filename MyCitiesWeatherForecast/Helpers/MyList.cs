using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCitiesWeatherForecast.Helpers
{
    public class MyList
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string Description { get; set; }
        public decimal MaxTemp { get; set; }
        public decimal MinTemp { get; set; }
    }
}
