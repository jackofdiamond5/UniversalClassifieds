using Newtonsoft.Json;

namespace AutomaticListings.Models.UserInventoryModels.DescriptionModels
{
    internal abstract class Tag
    {
        public Tag(string category, string internalName, string localizedCategoryName, string color)
        {
            this.Category = category;
            this.InternalName = internalName;
            this.Color = color;
        }

        [JsonProperty("category")]
        public string Category { get; }

        [JsonProperty("internal_name")]
        public string InternalName { get; }

        [JsonProperty("localized_category_name")]
        public string LocalizedCategoryName { get; }

        [JsonProperty("color")]
        public string Color { get; }
    }
}
