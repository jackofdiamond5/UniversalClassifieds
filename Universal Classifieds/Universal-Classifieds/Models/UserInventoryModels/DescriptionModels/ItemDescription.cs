using Newtonsoft.Json;

namespace AutomaticListings.Models.UserInventoryModels.DescriptionModels
{
    internal abstract class ItemDescription
    {
        public ItemDescription(string value, string color)
        {
            this.Value = value;
            this.Color = color;
        }

        [JsonProperty("value")]
        public string Value { get; }

        [JsonProperty("color")]
        public string Color { get; }
    }
}
