using Newtonsoft.Json;

namespace AutomaticListings.Models.UserInventoryModels.DescriptionModels
{
    internal abstract class Action
    {
        public Action(string link, string name)
        {
            this.Link = link;
            this.Name = name;
        }

        [JsonProperty("link")]
        public string Link { get; }

        [JsonProperty("name")]
        public string Name { get; }
    }
}
