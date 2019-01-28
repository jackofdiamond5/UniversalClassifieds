using Newtonsoft.Json;

namespace AutomaticListigs.Models
{
    /// <summary>
    /// Model that contains the user's configuration settings.
    /// </summary>
    public sealed class PreferencesModel
    {
        public PreferencesModel(
            string unusualSellMultiplier, string strangeSellMultiplier, string othersSellMultiplier,
            string unusualBuyMultiplier, string strangeBuyMultiplier, string othersBuyMultiplier,
            bool? autoCreateSellLisings, bool? autoCreateBuyListings, string recreationTimeout, string pricesUri)
        {
            this.UnusualSellMultplier = unusualSellMultiplier;
            this.StrangeSellMultiplier = strangeSellMultiplier;
            this.OthersSellMultiplier = othersSellMultiplier;
            this.UnusualBuyMultiplier = unusualBuyMultiplier;
            this.StrangeBuyMultiplier = strangeBuyMultiplier;
            this.OthersBuyMultiplier = othersBuyMultiplier;
            this.AutoCreateSellListings = autoCreateSellLisings;
            this.AutoCreateBuyListings = autoCreateBuyListings;
            this.RecreationTimeout = recreationTimeout;
            this.PricesUri = pricesUri;
        }

        [JsonProperty("unusualSellMultiplier")]
        public string UnusualSellMultplier { get; set; }

        [JsonProperty("strangeSellmultiplier")]
        public string StrangeSellMultiplier { get; set; }

        [JsonProperty("othersSellMultiplier")]
        public string OthersSellMultiplier { get; set; }

        [JsonProperty("unusualBuyMultiplier")]
        public string UnusualBuyMultiplier { get; set; }

        [JsonProperty("strangeBuyMultiplier")]
        public string StrangeBuyMultiplier { get; set; }

        [JsonProperty("othersBuyMultiplier")]
        public string OthersBuyMultiplier { get; set; }

        [JsonProperty("recreationTimeout")]
        public string RecreationTimeout { get; set; }

        [JsonProperty("autoCreateSellListings")]
        public bool? AutoCreateSellListings { get; set; }

        [JsonProperty("autoCreateBuyListings")]
        public bool? AutoCreateBuyListings { get; set; }

        [JsonProperty("pricesUri")]
        public string PricesUri { get; set; }
    }
}
