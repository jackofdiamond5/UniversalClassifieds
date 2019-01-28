using Newtonsoft.Json;

namespace AutomaticListigs.Models
{
    public sealed class UserInfoModel
    {
        public UserInfoModel(
            string apiKey, string steamId, string accessToken, 
            string sellComment, string buyComment, string steamGameCode)
        {
            this.ApiKey = apiKey;
            this.SteamId = steamId;
            this.AccessToken = accessToken;
            this.SellComment = sellComment;
            this.BuyComment = buyComment;
            this.SteamGameCode = steamGameCode;
        }

        /// <summary>
        /// API key of backpack.tf
        /// </summary>
        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }

        /// <summary>
        /// SteamId64 of the user.
        /// </summary>
        [JsonProperty("steamId64")]
        public string SteamId { get; set; }

        /// <summary>
        /// Access token of backpack.tf
        /// </summary>
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Comment that will be written on the sell listing.
        /// </summary>
        [JsonProperty("sellComment")]
        public string SellComment { get; set; }

        /// <summary>
        /// Comment that will be written on the buy listing.
        /// </summary>
        [JsonProperty("buyComment")]
        public string BuyComment { get; set; }

        /// <summary>
        /// The game code for the current items.
        /// </summary>
        [JsonProperty("gameCode")]
        public string SteamGameCode { get; set; }
    }
}
