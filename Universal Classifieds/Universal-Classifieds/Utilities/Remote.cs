using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Windows.Storage;

using Newtonsoft.Json;

using AutomaticListigs.Models;
using AutomaticListings.Utilities;
using AutomaticListings.Pages.Dialogs;

namespace AutomaticListigs.Utilities
{
    public class Remote
    {
        private const string SteamInventoryUri = "http://steamcommunity.com/inventory";
        private const string ItemsQueryUri = "2?l=english&count=5000";
        private const string BaseUri = "https://backpack.tf/api/classifieds/list/v1?token=";
        private const string Message = "Price URI was empty. Used backpack.tf base URI!";

        private readonly FileReader fileReader;

        private readonly StorageFolder LocalFolder;

        public Remote()
        {
            this.fileReader = new FileReader();
            this.LocalFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task<string> DownloadItemPrices(UserInfoModel userInfo)
        {
            var preferences = this.GetPreferences();

            if (string.IsNullOrWhiteSpace(preferences.PricesUri))
            {
                preferences.PricesUri = BaseUri;
                await this.DisplayMessage();
            }

            var uri = new Uri(preferences.PricesUri + userInfo.ApiKey);
            try
            {
                var request = WebRequest.CreateHttp(uri);
                var stream = await request.GetResponseAsync();

                return this.ReadStream(stream.GetResponseStream());
            }
            catch (WebException ex)
            {
                // TODO: Create custom exceptions
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> DownloadUserInventory(UserInfoModel userInfo)
        {
            var uri = new Uri($"{SteamInventoryUri}/{userInfo.SteamId}/{userInfo.SteamGameCode}/{ItemsQueryUri}");
            try
            {
                var request = WebRequest.CreateHttp(uri);
                var stream = await request.GetResponseAsync();

                return this.ReadStream(stream.GetResponseStream());
            }
            catch (WebException ex)
            {
                // TODO: Create custom exceptions
                throw new InvalidOperationException(ex.Message);
            }
        }

        private async Task DisplayMessage()
        {
            var infoDialog = new InfoDialog(Message);
            await infoDialog.ShowAsync();
        }

        private string ReadStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        private PreferencesModel GetPreferences()
        {
            return JsonConvert.DeserializeObject<PreferencesModel>(
                 this.fileReader.ReadDataFromFile(this.LocalFolder, LocalData.UserPreferences));
        }
    }
}
