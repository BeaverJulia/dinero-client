using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpPage : ContentPage
    {
        public Wallet SelectedWallet;
        public PopUpPage(Wallet wallet)
        {
            
            InitializeComponent();
            SelectedWallet = wallet;
        }
        private async void BtnPopUp_OnClicked(object sender, EventArgs e)
        {
            var walletView = new WalletView();
            float balance = float.Parse(SelectedWallet.Balance);
            balance += float.Parse(Balance?.Text);
            SelectedWallet.Balance = balance.ToString();
            var result = await walletView.PatchAsync(SelectedWallet);
            var output = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<ResultMessage>(output);
                await DisplayAlert("Balance changed", "Available funds: " + SelectedWallet.Balance, "Ok");
                await Navigation.PushModalAsync(new WalletDetailsPage(SelectedWallet));
            }
            else
            {
                //TODO - change this validation
                try
                {
                    var response = JsonConvert.DeserializeObject<ResultMessage>(output);
                    await DisplayAlert("Validation errors", response.Message, "Ok");
                }
                catch
                {
                    await DisplayAlert("Validation errors", result.StatusCode.ToString(), "Ok");

                }

            }
        }

    }
}