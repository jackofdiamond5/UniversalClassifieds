using System.Linq;
using Windows.System;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;

namespace AutomaticListings.Utilities
{
    public static class Helpers
    {
        public static bool TextContainsOnlyNumbers(TextBox sender, KeyRoutedEventArgs e)
        {
            var key = e.Key;
            var keyType = key.ToString().ToCharArray().Last();
            // TODO: Add validation for the decimal key that is not on the num pad.
            if (!key.Equals(VirtualKey.Control) && !char.IsDigit(keyType) && e.Key != VirtualKey.Decimal)
            {
                return true;
            }
            if (e.Key == VirtualKey.Decimal && sender.Text.IndexOf('.') > -1)
            {
                return true;
            }

            return false;
        }
    }
}
