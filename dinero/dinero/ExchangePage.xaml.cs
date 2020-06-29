using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class ExchangePage : ContentPage
    {
        public List<Wallet> WalletsList;
        private WalletView _walletView;
        private CurrenciesView _currenciesView;
        public ExchangePage()
        {
            InitializeComponent();
            WalletsList = GetWallets();
        }
        public List<Wallet> GetWallets()
        {
            _walletView = new WalletView();
            return _walletView.GetRequest().Result;
        }

        private void WalletFromSearch_OnSearchButtonPressed(object sender, EventArgs e)
        {
            var keyword = WalletFromSearch.Text;
            var wallets = WalletsList.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            WalletFrom.ItemsSource = wallets;
        }

        private void WalletToSearch_OnSearchButtonPressed(object sender, EventArgs e)
        {
            var keyword = WalletToSearch.Text;
            var wallets = WalletsList.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            WalletTo.ItemsSource = wallets;
        }

        private async void BtnExchange_OnClicked(object sender, EventArgs e)
        {
            _currenciesView=new CurrenciesView();
            var exchange = new Exchange();
            var walletFrom = (Wallet)WalletFrom.SelectedItem;
            var walletTo = (Wallet)WalletTo.SelectedItem;
            exchange.from_amount = float.Parse(txtAmount.Text,
                CultureInfo.InvariantCulture);
            exchange.from_wallet = walletFrom.Id;
            exchange.to_wallet = walletTo.Id;
            if (exchange.from_amount < 0)
            {
                await DisplayAlert("Validation errors", "Please provide correct Amount", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(WalletFrom.SelectedItem.ToString()))
            {
                await DisplayAlert("Validation errors", "The User Name is required", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(WalletTo.SelectedItem.ToString()))
            {
                await DisplayAlert("Validation errors", "The User Name is required", "Ok");
                return;
            }

            var result = await _currenciesView.CreateRequest(exchange);
            var output = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
             
                await DisplayAlert("Exchange completed", "Money Exchanged Successfully", "Ok");
                await Navigation.PushModalAsync(new PanelPage());
            }
            else
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<List<Output>>(output);
                    await DisplayAlert("Something went wrong", response[0].Detail[0].ToString(), "Ok");
                }
                catch
                {
                    await DisplayAlert("Something went wrong", output, "Ok");
                }

            }
        }

    }
}