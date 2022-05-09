using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace CryptocurrencyDashboard.Models
{
    public class CallAPI
    {
        
        static string API_KEY = "cbe3fde2-9e98-4d4b-913d-f0c11d1fecbc";

        public static WebClient getClient()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "5000";
            queryString["convert"] = "USD";
            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            return client;
        }
    }
}