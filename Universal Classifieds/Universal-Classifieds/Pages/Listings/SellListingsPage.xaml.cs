using System;
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

namespace AutomaticListigs.Pages.Listings
{
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
        }

        private string GetItemPrices()
        {
            // TODO: JsonConverter
            return this.fileReader.ReadDataFromFile(this.LocalFolder, LocalData.Prices);
        }

        private Task<string> GetUserInventory()
        {
            throw new NotImplementedException();
        }
    }
}
