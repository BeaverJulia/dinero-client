using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using dinero.Models;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class WalletDetailsPage : INotifyPropertyChanged
    {
        private TransactionView _transactionView;
        public Wallet SelectedWallet;
        public WalletDetailsPage(Wallet wallet)
        {
            InitializeComponent();
            SelectedWallet = wallet;
            BindingContext = wallet;
            Transactions.ItemsSource = GetTransactions(wallet);
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var wallet = new Wallet();
            var walletView = new WalletView();
            wallet.Name = Name?.Text;
            wallet.Balance = Balance?.Text;
            wallet.Id = int.Parse(Id.Text);
            var result = await walletView.PatchAsync(wallet);
            var output = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<ResultMessage>(output);
                await DisplayAlert("Wallet edited", response.Message, "Ok");
                await Navigation.PushModalAsync(new PanelPage());
            }
            else
            {
                //TODO - change this validation
                try
                {
                    var response = JsonConvert.DeserializeObject<ResultMessage>(output);
                    await DisplayAlert("Validation errors", response.Message, "Ok");
                }
                catch
                {
                    await DisplayAlert("Validation errors", result.StatusCode.ToString(), "Ok");

                }

            }
        }

        private void Transactions_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            GetTransactionDetails();
        }
        public async void GetTransactionDetails()
        {
            var transactionDetail = (Transaction)Transactions.SelectedItem;
            await Navigation.PushModalAsync(new TransactionDetails(transactionDetail));
        }
        public List<Transaction> GetTransactions(Wallet wallet)
        {
            _transactionView = new TransactionView();
            var transactions = _transactionView.GetRequests(10).Result;
            foreach (var transaction in transactions)
            {
                var dateTime = DateTimeOffset.Parse(transaction.Paid_At, CultureInfo.InvariantCulture);
                transaction.Paid_At = dateTime.DateTime.ToLongDateString();
            }

            var transactionsForWallet = transactions.Where(x => x.Currency.Name.Contains(wallet.Currency.Name));
            return transactionsForWallet.ToList();
        }


        private async void BtnNewTransaction_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTransaction(SelectedWallet));
        }
    }
}