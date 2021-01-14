using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCitiesWeatherForecast.Helpers
{
    public class Weather
    {
        public DateTime ForecastTimeUtc { get; set; }
        public decimal AirTemperature { get; set; }
        public decimal WindSpeed { get; set; }
        public decimal WindGust { get; set; }
        public int WindDirection { get; set; }
        public int CloudCover { get; set; }
        public int SeaLevelPressure { get; set; }
        public decimal RelativeHumidity { get; set; }
        public decimal TotalPrecipitation { get; set; }
        public string ConditionCode { get; set; }
    }
}
