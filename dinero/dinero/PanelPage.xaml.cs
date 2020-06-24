using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanelPage : ContentPage
    {
        public List<Wallet> WalletsList;
        private WalletView WalletView;
        public PanelPage()
        {
            InitializeComponent();
            WalletsList = new List<Wallet>();
            WalletView = new WalletView();
            WalletsList = GetWallets();
            Wallets.ItemsSource = WalletsList;

        }
        public List<Wallet> GetWallets()
        {
            WalletView = new WalletView();
            return WalletView.GetRequest().Result;
        }
    }
}