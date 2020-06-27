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
        public List<Transaction> TransactionsList;
        public PanelPage()
        {
            InitializeComponent();
            WalletsList = new List<Wallet>();
            TransactionsList = new List<Transaction>();
            WalletsList = GetWallets();
            /*TransactionsList = GetTransactions();*/
            Transactions.ItemsSource = TransactionsList;
            Wallets.ItemsSource = WalletsList;
            btnNewTransaction.Clicked += BtnNewTransaction_Clicked;
            BindingContext = new WalletDetailsPage();
        }

        private async void BtnNewTransaction_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTransaction());
        }

        public List<Wallet> GetWallets()
        {
            WalletView = new WalletView();
            return WalletView.GetRequest().Result;
        }

        /*public List<Transaction> GetTransactions()
        {
            TransactionView = new TransactionView();
            return TransactionView.GetRequests(10).Result;
        }*/
      
    }
    }
