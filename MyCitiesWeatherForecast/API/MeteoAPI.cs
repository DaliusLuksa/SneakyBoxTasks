using Polly;
using Polly.Bulkhead;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
