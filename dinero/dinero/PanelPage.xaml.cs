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
        private List<Wallet> wallets;
        private WalletView walletView;
        public PanelPage()
        {
            InitializeComponent();
            wallets = new List<Wallet>();
            walletView = new WalletView();
            GetWallets();
        }
        public async void GetWallets()
        {
            wallets = await walletView.GetRequest();
        }
    }
}