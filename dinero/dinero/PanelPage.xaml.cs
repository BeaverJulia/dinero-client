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
        private ICommand _okCommand { get;  }
        public PanelPage()
        {
            InitializeComponent();
            WalletsList = new List<Wallet>();
            WalletView = new WalletView();
            WalletsList = GetWallets();
            Wallets.ItemsSource = WalletsList;
            _okCommand = new Command(OkCommand_Execute);
            btnNewTransaction.Clicked += BtnNewTransaction_Clicked;
           
        }

        private async void BtnNewTransaction_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTransaction());
        }

        public void OkCommand_Execute(Object obj)
        {
            var bla = obj;
        }

        public List<Wallet> GetWallets()
        {
            WalletView = new WalletView();
            return WalletView.GetRequest().Result;
        }

      
    }
    }
