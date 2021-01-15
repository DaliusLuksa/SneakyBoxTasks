using Polly;
using Polly.Bulkhead;
using Polly.Retry;
using System;
using System.Net;
using System.Net.Http;

namespace MyCitiesWeatherForecast.API
{
    public class MeteoAPI
    {
        private static readonly HttpClient client;
        private static readonly AsyncRetryPolicy retry;
        private static readonly AsyncBulkheadPolicy bulkhead;

        static MeteoAPI()
        {
            retry = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(new[] {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4)
                });

            bulkhead = Policy
                .BulkheadAsync(20, int.MaxValue);

            client = new HttpClient
            {
                Timeout = new TimeSpan(0, 2, 0)
            };
        }

        /// <summary>
        /// Gets all cities from MeteoAPI
        /// </summary>
        /// <returns>Json string of all cities</returns>
        public static string GetCities()
        {
            string url = @"https://api.meteo.lt/v1/places";
            using (var msg = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = Policy.WrapAsync(bulkhead, retry).ExecuteAsync(async () => await client.SendAsync(msg));
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    return response.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return response.Result.StatusCode.ToString();
                }
            }
        }

        /// <summary>
        /// Gets specific city info from MeteoAPI
        /// </summary>
        /// <param name="cityCode">City code</param>
        /// <returns>Json string with city information</returns>
        public static string GetCityInfo(string cityCode)
        {
            string url = @"https://api.meteo.lt/v1/places/" + cityCode;
            using (var msg = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = Policy.WrapAsync(bulkhead, retry).ExecuteAsync(async () => await client.SendAsync(msg));
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    return response.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return response.Result.StatusCode.ToString();
                }
            }
        }

        /// <summary>
        /// Gets specific city's weather information from MeteoAPI
        /// </summary>
        /// <param name="cityCode">City code</param>
        /// <returns>Json string with city's weather information</returns>
        public static string GetCityWeatherForecast(string cityCode)
        {
            string url = @"https://api.meteo.lt/v1/places/" + cityCode + "/forecasts/long-term";
            using (var msg = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = Policy.WrapAsync(bulkhead, retry).ExecuteAsync(async () => await client.SendAsync(msg));
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    return response.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return response.Result.StatusCode.ToString();
                }
            }
        }
    }
}