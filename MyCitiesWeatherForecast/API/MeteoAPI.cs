﻿using Polly;
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

        public static string GetAds()
        {
            string url = @"http://localhost:56795/api/ad";
            using (var msg = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = Policy.WrapAsync(bulkhead, retry).ExecuteAsync(async () => await client.SendAsync(msg));
                if (response.Result.StatusCode == HttpStatusCode.OK)
                    return response.Result.Content.ReadAsStringAsync().Result;
                return null;
            }
        }
    }
}
