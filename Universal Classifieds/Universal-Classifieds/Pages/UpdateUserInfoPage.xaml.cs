using System;
using System.Linq;
using Windows.System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;

using Newtonsoft.Json;

using AutomaticListigs.Models;
using AutomaticListigs.Utilities;
using AutomaticListings.Utilities;
using System.Threading.Tasks;

namespace AutomaticListigs.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateUserInfoPage : Page
    {
        private readonly FileWriter fileWriter;
        private readonly FileReader fileReader;
        private readonly Ensure ensure;

        private readonly StorageFolder LocalFolder;

        public UpdateUserInfoPage()
        {
            this.fileWriter = new FileWriter();
            this.fileReader = new FileReader();
            this.ensure = new Ensure();
            this.LocalFolder = ApplicationData.Current.LocalFolder;
            this.EnsurePathState();
            this.InitializeComponent();
            this.PopulatePage();
        }

        #region Event Handlers
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.FormNotValid())
            {
                var errorDialog = new ErrorDialog(Messages.EmptyFieldFoundError);
                await errorDialog.ShowAsync();

                return;
            }

            var userInfo = this.GenerateUserInfoModel();
            this.fileWriter.WriteDataToFile(
               this.LocalFolder,
               LocalData.UserInfo,
               JsonConvert.SerializeObject(userInfo));

            this.Frame.Navigate(typeof(MainPage));
        }

        private void steamId_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = this.KeyIsValid(e);
        }

        private void steamGameCode_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = Helpers.TextContainsOnlyNumbers(sender as TextBox, e);
        }
        #endregion

        private void PopulatePage()
        {
            var infoFromFile = this.EnsureUserInfoData(JsonConvert.DeserializeObject<UserInfoModel>(
                     this.fileReader.ReadDataFromFile(this.LocalFolder, LocalData.UserInfo)
                ));

            this.apiKey.Text = infoFromFile.ApiKey;
            this.steamId.Text = infoFromFile.SteamId;
            this.accessToken.Text = infoFromFile.AccessToken;
            this.sellComment.Text = infoFromFile.SellComment;
            this.buyComment.Text = infoFromFile.BuyComment;
            this.steamGameCode.Text = infoFromFile.SteamGameCode;
        }

        private bool FormNotValid()
        {
            return
               string.IsNullOrEmpty(this.apiKey.Text) || string.IsNullOrEmpty(this.steamGameCode.Text) ||
               string.IsNullOrEmpty(this.accessToken.Text) || string.IsNullOrEmpty(this.steamId.Text) ||
               string.IsNullOrEmpty(this.buyComment.Text) || string.IsNullOrEmpty(this.sellComment.Text);
        }

        private UserInfoModel EnsureUserInfoData(UserInfoModel infoFromFile)
        {
            if (infoFromFile == null || string.IsNullOrEmpty(infoFromFile.ApiKey) ||
                string.IsNullOrEmpty(infoFromFile.AccessToken) || string.IsNullOrEmpty(infoFromFile.SteamId) ||
                string.IsNullOrEmpty(infoFromFile.BuyComment) || string.IsNullOrEmpty(infoFromFile.SellComment) ||
                string.IsNullOrEmpty(infoFromFile.SteamGameCode))
            {
                this.fileWriter.WriteDataToFile(
                   this.LocalFolder,
                   LocalData.UserInfo,
                   JsonConvert.SerializeObject(this.GenerateUserInfoModel()));

                infoFromFile = JsonConvert.DeserializeObject<UserInfoModel>(
                     this.fileReader.ReadDataFromFile(this.LocalFolder, LocalData.UserInfo));
            }

            return infoFromFile;
        }

        private UserInfoModel GenerateUserInfoModel()
        {
            return new UserInfoModel(
                this.apiKey.Text ?? "",
                this.steamId.Text ?? "",
                this.accessToken.Text ?? "",
                this.sellComment.Text ?? "",
                this.buyComment.Text ?? "",
                this.steamGameCode.Text ?? "");
        }

        private bool KeyIsValid(KeyRoutedEventArgs e)
        {
            var key = e.Key;
            var keyType = key.ToString().ToCharArray().Last();

            return !key.Equals(VirtualKey.Control) && !char.IsDigit(keyType);
        }

        private void EnsurePathState()
        {
            this.ensure.DirectoryExistsAsync(this.LocalFolder, LocalData.ResourcesFolder).GetAwaiter();
            this.ensure.FileExistsInDirectoryAsync(
                this.LocalFolder,
                LocalData.UserInfo,
                LocalData.ResourcesFolder).GetAwaiter();
        }
    }
}
