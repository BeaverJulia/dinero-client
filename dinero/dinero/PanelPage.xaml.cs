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
        private TransactionView TransactionView;
        public List<Wallet> WalletsList;
        private WalletView WalletView;

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
            WalletView = new WalletView();
            return WalletView.GetRequest().Result;
        }

        public List<Transaction> GetTransactions()
        {
            TransactionView = new TransactionView();
            var transactions = TransactionView.GetRequests(10).Result;
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
    }
}