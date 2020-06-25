using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public WalletDetailsPage _WalletDetails;
        public PanelPage()
        {
            InitializeComponent();
            WalletsList = new List<Wallet>();
            WalletView = new WalletView();
            WalletsList = GetWallets();
            Wallets.ItemsSource = WalletsList;
            _WalletDetails= new WalletDetailsPage();
            BindingContext = _WalletDetails;
        }

        public List<Wallet> GetWallets()
        {
            WalletView = new WalletView();
            return WalletView.GetRequest().Result;
        }



      
    }
    }
