using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptocurrencyDashboard.Models
{
    public class Currency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public string Rank { get; set; }
        public string Price { get; set; }
        public string Volume24hUsd { get; set; }
        public string MarketCapUsd { get; set; }
        public double PercentChange1h { get; set; }
        public double PercentChange24h { get; set; }
        public double PercentChange7d { get; set; }
        public DateTime LastUpdated { get; set; }
        public string img { get; set; }
    }
}