using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;

namespace CryptocurrencyDashboard.Models
{
    public class CurrencyDAO
    {
        private UriBuilder URL;
        private WebClient client;
        private string jsonString;
        private JObject jsonObject;
        private static JToken data;
        public CurrencyDAO()
        {
            URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            client = CallAPI.getClient();
            jsonString = client.DownloadString(URL.ToString());
            jsonObject = JObject.Parse(jsonString);
            data = jsonObject.SelectToken("data");
            client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
        }
    
        
        public List<Currency> getList()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            List<Currency> currencies = new List<Currency>();
            foreach (JToken item in data)
            {
                Currency currency = new Currency();
                currency.Id = item.SelectToken("id").ToString();
                currency.Name = item.SelectToken("name").ToString();
                currency.Symbol = item.SelectToken("symbol").ToString();
                currency.Slug = item.SelectToken("slug").ToString();
                currency.Rank = item.SelectToken("cmc_rank").ToString();

                double price1 = double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("price").ToString());                     
                currency.Price = string.Format(cultureInfo, "{0:C}", price1);

                double vol24hUsd = double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("volume_24h").ToString());
                currency.Volume24hUsd = string.Format(cultureInfo, "{0:C}", vol24hUsd);

                double marketCapUsd = double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("market_cap").ToString());
                currency.MarketCapUsd = string.Format(cultureInfo, "{0:C}", marketCapUsd);

                currency.PercentChange1h = Math.Round(double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("percent_change_1h").ToString()),2);
                currency.PercentChange24h = Math.Round(double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("percent_change_24h").ToString()),2);
                currency.PercentChange7d = Math.Round(double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("percent_change_7d").ToString()),2);
                currency.LastUpdated = DateTime.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("last_updated").ToString());
                currency.img = "https://s2.coinmarketcap.com/static/img/coins/64x64/" + currency.Id + ".png";
                currencies.Add(currency);
            }
            return currencies;
        }
        public Currency getInform(string slug)
        {
            if(slug != null)
            {
                CultureInfo cultureInfo = new CultureInfo("en-US");
                JToken item = jsonObject.SelectToken("$.data[?(@.slug == '" + slug + "')]");
                Currency currency = new Currency();
                currency.Id = item.SelectToken("id").ToString();
                currency.Name = item.SelectToken("name").ToString();
                currency.Symbol = item.SelectToken("symbol").ToString();
                currency.Slug = item.SelectToken("slug").ToString();
                currency.Rank = item.SelectToken("cmc_rank").ToString();

                double price1 = double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("price").ToString());
                currency.Price = string.Format(cultureInfo, "{0:C}", price1);

                double vol24hUsd = double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("volume_24h").ToString());
                currency.Volume24hUsd = string.Format(cultureInfo, "{0:C}", vol24hUsd);

                double marketCapUsd = double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("market_cap").ToString());
                currency.MarketCapUsd = string.Format(cultureInfo, "{0:C}", marketCapUsd);

                currency.PercentChange1h = Math.Round(double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("percent_change_1h").ToString()), 2);
                currency.PercentChange24h = Math.Round(double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("percent_change_24h").ToString()), 2);
                currency.PercentChange7d = Math.Round(double.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("percent_change_7d").ToString()), 2);
                currency.LastUpdated = DateTime.Parse(item.SelectToken("quote").SelectToken("USD").SelectToken("last_updated").ToString());
                currency.img = "https://s2.coinmarketcap.com/static/img/coins/64x64/" + currency.Id + ".png";

                return currency;
            }
            return new Currency();
        }
    }
}