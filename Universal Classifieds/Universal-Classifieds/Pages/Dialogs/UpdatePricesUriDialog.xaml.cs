using Windows.Storage;
using Windows.UI.Xaml.Controls;

using Newtonsoft.Json;

using AutomaticListigs.Models;
using AutomaticListigs.Utilities;
using AutomaticListings.Utilities;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AutomaticListings.Pages.Dialogs
{
    public sealed partial class UpdatePricesUriDialog : ContentDialog
    {
        private readonly FileReader fileReader;
        private readonly FileWriter fileWriter;

        private readonly StorageFolder LocalFolder;

        public UpdatePricesUriDialog()
        {
            this.fileReader = new FileReader();
            this.fileWriter = new FileWriter();
            this.LocalFolder = ApplicationData.Current.LocalFolder;
            this.InitializeComponent();
            this.PopulateForm();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.UpdatePricesUri();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void PopulateForm()
        {
            var preferences = this.GetPreferences();
            this.uriTextBox.Text = preferences.PricesUri;
        }

        private void UpdatePricesUri()
        {
            var newUri = this.uriTextBox.Text;
            var preferences = this.GetPreferences();
            preferences.PricesUri = newUri;

            this.fileWriter.WriteDataToFile(
               this.LocalFolder,
               LocalData.UserPreferences,
               JsonConvert.SerializeObject(preferences));
        }

        private PreferencesModel GetPreferences()
        {
            return JsonConvert.DeserializeObject<PreferencesModel>(
                 this.fileReader.ReadDataFromFile(this.LocalFolder, LocalData.UserPreferences));
        }
    }
}
