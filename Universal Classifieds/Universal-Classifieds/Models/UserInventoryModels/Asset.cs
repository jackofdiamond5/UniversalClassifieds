using Newtonsoft.Json;

namespace AutomaticListings.Models.UserInventoryModels
{
    internal abstract class Asset
    {
        public Asset(int appId, string contextId, string assetId, string classId, string instanceId, string amount)
        {
            this.AppId = appId;
            this.ContextId = contextId;
            this.AssetId = assetId;
            this.ClassId = classId;
            this.InstanceId = instanceId;
            this.Amount = amount;
        }

        [JsonProperty("appid")]
        public int AppId { get; }

        [JsonProperty("contextid")]
        public string ContextId { get; }

        [JsonProperty("assetid")]
        public string AssetId { get; }

        [JsonProperty("classid")]
        public string ClassId { get; }

        [JsonProperty("instanceid")]
        public string InstanceId { get; }

        [JsonProperty("amount")]
        public string Amount { get; }
    }
}
