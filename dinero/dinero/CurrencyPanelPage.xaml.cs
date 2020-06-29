using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyPanelPage : ContentPage
    {
        private CurrenciesView _currenciesView;
        public CurrencyPanelPage()
        {
            InitializeComponent();
            Currencies.ItemsSource = GetTransactions();
        }

        public List<Currency> GetTransactions()
        {
            _currenciesView = new CurrenciesView();
            var currencies = _currenciesView.GetRequest().Result;
            return currencies;
        }

        private async void BtnExchange_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ExchangePage());
        }
    }
}