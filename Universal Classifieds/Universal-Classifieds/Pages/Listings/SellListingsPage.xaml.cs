﻿using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Newtonsoft.Json;

using AutomaticListigs.Models;
using AutomaticListigs.Utilities;
using AutomaticListings.Pages.Dialogs;

using AutomaticListings.Utilities;
using System.Threading.Tasks;

using System.IO;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AutomaticListigs.Pages.Listings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SellListingsPage : Page
    {
        private readonly Ensure ensure;
        private readonly FileReader fileReader;
        private readonly FileWriter fileWriter;

        private readonly StorageFolder LocalFolder;

        public SellListingsPage()
        {
            this.LocalFolder = ApplicationData.Current.LocalFolder;
            this.fileReader = new FileReader();
            this.fileWriter = new FileWriter();
            this.ensure = new Ensure();
            this.InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void updatePricesButton_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateItemPrices();
        }

        private void updateUriButton_Click(object sender, RoutedEventArgs e)
        {
            var updateDialog = new UpdatePricesUriDialog();
            updateDialog.ShowAsync().GetAwaiter();
        }

        private async void EnsurePricesFile()
        {
            await this.ensure.FileExistsInDirectoryAsync(this.LocalFolder, LocalData.Prices, LocalData.ResourcesFolder);
        }

        private void UpdateItemPrices()
        {
            throw new NotImplementedException();
            // TODO
            //var data = JsonConvert.DeserializeObject<UserInfoModel>(
            //     this.fileReader.ReadDataFromFile(this.LocalFolder, LocalData.UserInfo));
            //var prices = await this.remote.DownloadItemPrices(data);

            //this.fileWriter.WriteDataToFile(this.LocalFolder, LocalData.Prices, prices);
        }

        private string GetItemPrices()
        {
            // TODO: JsonConverter
            return this.fileReader.ReadDataFromFile(this.LocalFolder, LocalData.Prices);
        }

        private Task<string> GetUserInventory()
        {
            throw new NotImplementedException();
            // TODO
            //var data = JsonConvert.DeserializeObject<UserInfoModel>(
            //    this.fileReader.ReadDataFromFile(this.LocalFolder, LocalData.UserInfo));
            //return await this.remote.DownloadUserInventory(data);
        }
    }
}
