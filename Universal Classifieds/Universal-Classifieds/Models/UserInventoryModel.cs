using Newtonsoft.Json;
using System.Collections.Generic;

using AutomaticListings.Models.UserInventoryModels;

namespace AutomaticListings.Models
{
    internal abstract class UserInventoryModel
    {
        public UserInventoryModel(ICollection<Asset> assets, ICollection<Description> descriptions,
            int totalInventoryCount, int success, int rwgrsn)
        {
            this.Assets = assets;
            this.Descriptions = descriptions;
            this.TotalInventoryCount = totalInventoryCount;
            this.Success = success;
            this.Rwgrsn = rwgrsn;
        }

        /// <summary>
        /// Contains all item asset objects.
        /// </summary>
        [JsonProperty("assets")]
        public ICollection<Asset> Assets { get; }

        /// <summary>
        /// Contains all item description objects.
        /// </summary>
        [JsonProperty("descriptions")]
        public ICollection<Description> Descriptions { get; }

        /// <summary>
        /// The total amount of items in the user's inventory.
        /// </summary>
        [JsonProperty("total_inventory_count")]
        public int TotalInventoryCount { get; }

        /// <summary>
        /// Value is 1 if the user's iventory was fetched and less than 1 otherwise.
        /// </summary>
        [JsonProperty("success")]
        public int Success { get; }

        [JsonProperty("rwgrsn")]
        public int Rwgrsn { get; }
    }
}
