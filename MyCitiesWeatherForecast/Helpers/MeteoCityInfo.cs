using System;

namespace MyCitiesWeatherForecast.Helpers
{
    public partial class MeteoCityInfo
    {
        
        //public string Code { get; set; }
        //public string Name { get; set; }
        //public string AdministrativeDivision { get; set; }
        //public string Country { get; set; }
        //public string CountryCode { get; set; }
        //public string Coordinates { get; set; }
        public string ForecastType { get; set; }
        public DateTime ForecastCreationTimeUtc { get; set; }

        public virtual MeteoCity Place { get; set; }
        public virtual Weather[] ForecastTimestamps { get; set; }
    }
}