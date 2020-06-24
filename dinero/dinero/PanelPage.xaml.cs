using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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



        public RelayCommand<object> OKCommand
        {
            get
            {
                if (_okCommand == null)
                    _okCommand = new RelayCommand<object>(OkCommand_Execute);
                return _okCommand;
            }
        }
        private RelayCommand<object> _okCommand = null;

        private void OkCommand_Execute(object obj)
        {
            
        }
    }
}