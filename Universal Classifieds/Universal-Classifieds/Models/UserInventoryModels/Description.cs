using System.Collections.Generic;
using AutomaticListings.Models.UserInventoryModels.DescriptionModels;
using Newtonsoft.Json;

namespace AutomaticListings.Models.UserInventoryModels
{
    internal abstract class Description
    {
        public Description(int appId, string classId, string instanceId, decimal? currency, string backgroundColor,
            string iconUrl, string iconUrlLarge, ICollection<ItemDescription> descriptions,
            string tradable, ICollection<Action> actions, string name, string nameColor, string type,
            string marketName, string marketHashName, ICollection<MarketAction> marketActions,
            int? commodity, int? marketTradableRestriction, int? marketMarketableRestriction,
            int? marketable, ICollection<Tag> tags)
        {
            this.AppId = appId;
            this.ClassId = classId;
            this.InstanceId = instanceId;
            this.Currency = currency;
            this.BackgroundColor = backgroundColor;
            this.IconUrl = iconUrl;
            this.IconUrlLarge = iconUrlLarge;
            this.Descriptions = descriptions;
            this.Tradable = tradable;
            this.Actions = actions;
            this.Name = name;
            this.NameColor = nameColor;
            this.Type = type;
            this.MarketName = marketName;
            this.MarketHashName = marketHashName;
            this.MarketActions = marketActions;
            this.Commodity = commodity;
            this.MarketTradableRestriction = marketTradableRestriction;
            this.MarketMarketableRestriction = marketMarketableRestriction;
            this.Marketable = marketable;
            this.Tags = tags;
        }

        [JsonProperty("appid")]
        public int AppId { get; }

        [JsonProperty("classid")]
        public string ClassId { get; }

        [JsonProperty("instanceid")]
        public string InstanceId { get; }

        [JsonProperty("currency")]
        public decimal? Currency { get; }

        [JsonProperty("background_color")]
        public string BackgroundColor { get; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; }

        [JsonProperty("icon_url_large")]
        public string IconUrlLarge { get; }

        [JsonProperty("descriptions")]
        public ICollection<ItemDescription> Descriptions { get; }

        [JsonProperty("tradable")]
        public string Tradable { get; }

        [JsonProperty("actions")]
        public ICollection<Action> Actions { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("name_color")]
        public string NameColor { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("market_name")]
        public string MarketName { get; }

        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; }

        [JsonProperty("market_actions")]
        public ICollection<MarketAction> MarketActions { get; }

        [JsonProperty("commodity")]
        public int? Commodity { get; }

        [JsonProperty("market_tradable_restriction")]
        public int? MarketTradableRestriction { get; }

        [JsonProperty("market_marketable_restriction")]
        public int? MarketMarketableRestriction { get; }

        [JsonProperty("marketable")]
        public int? Marketable { get; }

        [JsonProperty("tags")]
        public ICollection<Tag> Tags { get; }
    }
}
