﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using dinero.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTransaction : ContentPage
    {
        private TransactionView _transactionView;
        public AccountView AccountView;
        public Wallet SelectedWallet;

        public AddTransaction(Wallet wallet)
        {
            InitializeComponent();
            SelectedWallet = wallet;
            btnSend.Clicked += BtnSend_Clicked;
            TxtWalletName.Text = wallet.Name;
        }

        private async void BtnSend_Clicked(object sender, EventArgs e)
        {
            _transactionView = new TransactionView();
            var transaction = new Transaction();
            var user = (User)SuggestedUsers.SelectedItem;
            transaction.Amount = float.Parse(txtAmount.Text,
                CultureInfo.InvariantCulture);
           
            transaction.Currency = SelectedWallet.Currency;
            transaction.To_User = user;
            if (transaction.Amount < 0)
            {
                await DisplayAlert("Validation errors", "Please provide correct Amount", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(SuggestedUsers.SelectedItem.ToString()))
            {
                await DisplayAlert("Validation errors", "The User Name is required", "Ok");
                return;
            }

            var result = await _transactionView.CreateRequest(transaction);
            var output = await result.Content.ReadAsStringAsync();
            
            if (result.IsSuccessStatusCode)
            {
                
                await DisplayAlert("Transaction completed", "Transaction completed", "Ok");
                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<List<Output>>(output);
                    await DisplayAlert("Something went wrong", response[0].Detail[0].ToString(), "Ok");
                }
                catch
                {
                    await DisplayAlert("Something went wrong", output, "Ok");
                }

            }
        }

        private void UserSearch_OnSearchButtonPressed(object sender, EventArgs e)
        {
            AccountView = new AccountView();
            var keyword = UserSearch.Text;
            var users = AccountView.UserSearch(keyword, "5").Result;
            SuggestedUsers.ItemsSource= users;
        }
        
    }
}