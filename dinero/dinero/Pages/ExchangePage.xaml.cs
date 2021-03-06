﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using dinero.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExchangePage : INotifyPropertyChanged
    {
        private CurrenciesView _currenciesView;
        private WalletView _walletView;
        public List<Wallet> WalletsFromList;
        public List<Wallet> WalletsToList;

        public ExchangePage()
        {
            InitializeComponent();
            _currenciesView = new CurrenciesView();
            WalletsFromList = GetWallets();
            WalletsToList = GetToWallets();
            BindingContext = new WalletPickerMVVMViewModel(WalletsFromList, WalletsToList);
        }

        public List<Wallet> GetWallets()
        {
            _walletView = new WalletView();
            return _walletView.GetRequest().Result;
        }

        public List<Wallet> GetToWallets()
        {
            WalletsToList = new List<Wallet>();
            WalletsToList = WalletsFromList;
            var wallet = new Wallet();
            wallet = (Wallet) WalletFromPicker.SelectedItem;
            WalletsToList.Remove(wallet);
            return WalletsToList;
        }

        private  async void BtnExchange_OnClicked(object sender, EventArgs e)
        {
            var walletFrom = (Wallet)WalletFromPicker.SelectedItem;
            var walletTo = (Wallet)WalletToPicker.SelectedItem;
            var amount =  float.Parse(txtAmount.Text,
                CultureInfo.InvariantCulture);
            var availableRates = _currenciesView.GetExchangeRatesRequest().Result;
            
            var rate = availableRates.Where(x =>
                x.from_currency.Code == walletFrom.Currency.Code && x.to_currency.Code == walletTo.Currency.Code).ToList();
            if (rate==null|| !rate.Any())
            {
                await DisplayAlert("No available rate", "Currency exchange not available ", "Ok");
                return;
            }
            else
            {
                var exchange = rate.FirstOrDefault();
                var calculate = exchange.amount * amount;
               bool answer= await DisplayAlert("Proceed", "Exchange rate for "+exchange.from_currency.Code+" to "+exchange.to_currency.Code+" is "+exchange.amount.ToString()+". Your money will be exchanged to "+calculate.ToString(), "Proceed", "Cancel");
              
               if (answer){Exchange();}
               return;
            }
        }

        private async void Exchange()
        {
          
            var exchange = new Exchange();
            var walletFrom = (Wallet)WalletFromPicker.SelectedItem;
            var walletTo = (Wallet)WalletToPicker.SelectedItem;
            exchange.from_amount = float.Parse(txtAmount.Text,
                CultureInfo.InvariantCulture);
            exchange.from_wallet = walletFrom.Id;
            exchange.to_wallet = walletTo.Id;
            if (exchange.from_amount < 0)
            {
                await DisplayAlert("Validation errors", "Please provide correct Amount", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(WalletFromPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Validation errors", "The User Name is required", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(WalletToPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Validation errors", "The User Name is required", "Ok");
                return;
            }

            var result = await _currenciesView.CreateRequest(exchange);
            var output = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Exchange completed", "Money Exchanged Successfully", "Ok");
                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<List<Output>>(output);
                    await DisplayAlert("Something went wrong", response[0].Detail[0], "Ok");
                }
                catch
                {
                    await DisplayAlert("Something went wrong", output, "Ok");
                }
            }
        }
        /*private void WalletFromPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            WalletsFromList = GetWallets();
            WalletsToList = GetToWallets();
            BindingContext = new WalletPickerMVVMViewModel(WalletsFromList, WalletsToList);
        }

        private void WalletToPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            WalletsFromList = GetWallets();
            WalletsToList = GetToWallets();
            BindingContext = new WalletPickerMVVMViewModel(WalletsFromList, WalletsToList);
        }*/
        
    }

 
}