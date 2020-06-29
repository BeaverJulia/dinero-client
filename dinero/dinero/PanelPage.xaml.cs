using System;
using System.Collections.Generic;
using System.Globalization;
using dinero.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class PanelPage : ContentPage
    {
        private TransactionView _transactionView;
        public List<Wallet> WalletsList;
        private WalletView _walletView;

        public PanelPage()
        {
            InitializeComponent();
            WalletsList = new List<Wallet>();
            WalletsList = GetWallets();
            Wallets.ItemsSource = WalletsList;
         
        }


        public List<Wallet> GetWallets()
        {
            _walletView = new WalletView();
            return _walletView.GetRequest().Result;
        }


        public async void GetWalletDetails()
        {
            var walletDetail = (Wallet) Wallets.SelectedItem;
            await Navigation.PushModalAsync(new WalletDetailsPage(walletDetail));
        }


        private void Wallets_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            GetWalletDetails();
        }

        private async void BtnNewWallet_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddWallet());
        }

        private async void BtnUserSettings_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new UserSettings());
        }

        private async void BtnExchange_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CurrencyPanelPage());
        }

      
    }
}