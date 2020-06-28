using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using dinero.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class PanelPage : ContentPage
    {
        public List<Wallet> WalletsList;
        private WalletView WalletView;
        private TransactionView TransactionView;
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
                var dateTime = DateTimeOffset.Parse(transaction.Paid_At, System.Globalization.CultureInfo.InvariantCulture);
                transaction.Paid_At = dateTime.DateTime.ToLongDateString();
            }
            return transactions;
        }

        public async void GetWalletDetails()
        {
            var walletDetail = (Wallet)Wallets.SelectedItem;
            await Navigation.PushModalAsync(new WalletDetailsPage(walletDetail));

        }

        public async void GetTransactionDetails()
        {
            var transactionDetail = (Transaction) Transactions.SelectedItem;
            await Navigation.PushAsync(new TransactionDetails(transactionDetail));
        }
        private void Tapped(object sender, ItemTappedEventArgs e)
        {
            GetWalletDetails();
        }

        private void Transactions_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            GetTransactionDetails();
        }

    }
    }
