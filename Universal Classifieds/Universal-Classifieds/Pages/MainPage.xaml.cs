using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;

using Newtonsoft.Json;

using AutomaticListigs.Pages;
using AutomaticListigs.Utilities;
using AutomaticListigs.Models;
using AutomaticListigs.Pages.Dialogs;
using AutomaticListigs.Pages.Listings;
using AutomaticListings.Utilities;
using System.IO;

namespace AutomaticListigs
{
    public sealed partial class MainPage : Page
    {
        private const int TimerMinValue = 30;

        private readonly FileWriter fileWriter;
        private readonly FileReader fileReader;
        private readonly Ensure ensure;

        private readonly StorageFolder LocalFolder;

        public MainPage()
        {
            this.fileReader = new FileReader();
            this.fileWriter = new FileWriter();
            this.ensure = new Ensure();
            this.LocalFolder = ApplicationData.Current.LocalFolder;
            this.InitializeComponent();
            this.EnsurePathState();
        }

        #region Event Handlers
        private void updatePreferences_Click(object sender, RoutedEventArgs e)
        {
            this.UpdatePreferences();
        }

        private void updateInfo_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdateUserInfoPage));
        }

        private void unusualSellMultiplier_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = Helpers.TextContainsOnlyNumbers(sender as TextBox, e);
        }

        private void strangeSellMultiplier_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = Helpers.TextContainsOnlyNumbers(sender as TextBox, e);
        }

        private void othersSellMultiplier_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = Helpers.TextContainsOnlyNumbers(sender as TextBox, e);
        }

        private void unusualBuyMultiplier_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = Helpers.TextContainsOnlyNumbers(sender as TextBox, e);
        }

        private void strangeBuyMultiplier_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = Helpers.TextContainsOnlyNumbers(sender as TextBox, e);
        }

        private void othersBuyMultiplier_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = Helpers.TextContainsOnlyNumbers(sender as TextBox, e);
        }

        private void recreationTimeout_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = Helpers.TextContainsOnlyNumbers(sender as TextBox, e);
        }

        private void createSellListings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SellListingsPage));
        }
        private void createBuyListings_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        private async void EnsurePathState()
        {
            await this.ensure.DirectoryExistsAsync(this.LocalFolder, LocalData.ResourcesFolder);
            await this.ensure.FileExistsInDirectoryAsync(this.LocalFolder, LocalData.UserPreferences, LocalData.ResourcesFolder);
            await this.ensure.FileExistsInDirectoryAsync(this.LocalFolder, LocalData.UserInfo, LocalData.ResourcesFolder);
            this.PopulatePage();
        }

        private void PopulatePage()
        {
            var infoFromFile = this.EnsurePreferencesData(JsonConvert.DeserializeObject<PreferencesModel>(
                   this.fileReader.ReadDataFromFile(this.LocalFolder, LocalData.UserPreferences)));

            this.unusualSellMultiplier.Text = infoFromFile.UnusualSellMultplier;
            this.strangeSellMultiplier.Text = infoFromFile.StrangeSellMultiplier;
            this.othersSellMultiplier.Text = infoFromFile.OthersSellMultiplier;
            this.unusualBuyMultiplier.Text = infoFromFile.UnusualBuyMultiplier;
            this.strangeBuyMultiplier.Text = infoFromFile.StrangeBuyMultiplier;
            this.othersBuyMultiplier.Text = infoFromFile.OthersBuyMultiplier;
            this.autoCreateSell.IsChecked = infoFromFile.AutoCreateSellListings;
            this.autoCreateBuy.IsChecked = infoFromFile.AutoCreateBuyListings;
            this.recreationTimeout.Text = infoFromFile.RecreationTimeout;
        }

        private PreferencesModel EnsurePreferencesData(PreferencesModel infoFromFile)
        {
            if (infoFromFile == null || string.IsNullOrEmpty(infoFromFile.UnusualSellMultplier) ||
                string.IsNullOrEmpty(infoFromFile.StrangeSellMultiplier) || string.IsNullOrEmpty(infoFromFile.OthersSellMultiplier) ||
                string.IsNullOrEmpty(infoFromFile.UnusualBuyMultiplier) || string.IsNullOrEmpty(infoFromFile.StrangeBuyMultiplier) ||
                string.IsNullOrEmpty(infoFromFile.OthersBuyMultiplier))
            {
                this.fileWriter.WriteDataToFile(
                     this.LocalFolder,
                     LocalData.UserPreferences,
                     JsonConvert.SerializeObject(this.GeneratePreferencesModel()));

                infoFromFile = JsonConvert.DeserializeObject<PreferencesModel>(
                     this.fileReader.ReadDataFromFile(
                        this.LocalFolder,
                        LocalData.UserPreferences));
            }

            return infoFromFile;
        }

        private void UpdatePreferences()
        {
            if (!this.EnsureFormState()) return;

            var preferences = GeneratePreferencesModel();
            this.fileWriter.WriteDataToFile(
                this.LocalFolder,
                LocalData.UserPreferences,
                JsonConvert.SerializeObject(preferences));

            this.ShowSuccessDialog(Messages.DataUpdateSuccess);
        }

        private void ShowSuccessDialog(string message)
        {
            var success = new SuccessDialog(message);
            success.ShowAsync().GetAwaiter();
        }

        private void ShowErrorDialog(string message)
        {
            var error = new ErrorDialog(message);
            error.ShowAsync().GetAwaiter();
        }

        #region Form Validation
        private bool EnsureFormState()
        {
            if (this.FormNotValid())
            {
                this.ShowErrorDialog(Messages.EmptyFieldFoundError);
                return false;
            }
            if (!this.TimeoutIsValid())
            {
                this.ShowErrorDialog(Messages.TimeoutValueError);
                return false;
            }
            if (!this.FormDataIsValid())
            {
                this.ShowErrorDialog(Messages.ValueBelowZeroError);
                return false;
            }

            return true;
        }

        private bool FormNotValid()
        {
            return
                string.IsNullOrEmpty(this.unusualSellMultiplier.Text) || string.IsNullOrEmpty(this.strangeSellMultiplier.Text) ||
                string.IsNullOrEmpty(this.othersSellMultiplier.Text) || string.IsNullOrEmpty(this.recreationTimeout.Text) ||
                string.IsNullOrEmpty(this.unusualBuyMultiplier.Text) || string.IsNullOrEmpty(this.strangeBuyMultiplier.Text) ||
                string.IsNullOrEmpty(this.othersSellMultiplier.Text);
        }

        private bool TimeoutIsValid()
        {
            var value = double.Parse(this.recreationTimeout.Text);
            return value >= TimerMinValue;
        }

        private bool FormDataIsValid()
        {
            return
                double.Parse(this.unusualSellMultiplier.Text) >= 0 &&
                double.Parse(this.strangeSellMultiplier.Text) >= 0 &&
                double.Parse(this.othersSellMultiplier.Text) >= 0 &&
                double.Parse(this.unusualBuyMultiplier.Text) >= 0 &&
                double.Parse(this.strangeBuyMultiplier.Text) >= 0 &&
                double.Parse(this.othersBuyMultiplier.Text) >= 0;
        }
        #endregion

        private PreferencesModel GeneratePreferencesModel()
        {
            return new PreferencesModel(
                this.unusualSellMultiplier.Text ?? "",
                this.strangeSellMultiplier.Text ?? "",
                this.othersSellMultiplier.Text ?? "",
                this.unusualBuyMultiplier.Text ?? "",
                this.strangeBuyMultiplier.Text ?? "",
                this.othersBuyMultiplier.Text ?? "",
                this.autoCreateSell.IsChecked,
                this.autoCreateBuy.IsChecked,
                this.recreationTimeout.Text ?? "",
                string.Empty);
        }

    }
}
