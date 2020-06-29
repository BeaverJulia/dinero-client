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
            Transactions.ItemsSource = GetTransactions();
            Wallets.ItemsSource = WalletsList;
            btnNewTransaction.Clicked += BtnNewTransaction_Clicked;
        }

        private async void BtnNewTransaction_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTransaction());
        }

        public List<Wallet> GetWallets()
        {
            _walletView = new WalletView();
            return _walletView.GetRequest().Result;
        }

        public List<Transaction> GetTransactions()
        {
            _transactionView = new TransactionView();
            var transactions = _transactionView.GetRequests(10).Result;
            foreach (var transaction in transactions)
            {
                var dateTime = DateTimeOffset.Parse(transaction.Paid_At, CultureInfo.InvariantCulture);
                transaction.Paid_At = dateTime.DateTime.ToLongDateString();
            }

            return transactions;
        }

        public async void GetWalletDetails()
        {
            var walletDetail = (Wallet) Wallets.SelectedItem;
            await Navigation.PushModalAsync(new WalletDetailsPage(walletDetail));
        }

        public async void GetTransactionDetails()
        {
            var transactionDetail = (Transaction) Transactions.SelectedItem;
            await Navigation.PushModalAsync(new TransactionDetails(transactionDetail));
        }

        private void Transactions_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            GetTransactionDetails();
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