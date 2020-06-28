using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public WalletDetailsPage(Wallet wallet)
        {
            InitializeComponent();
            BindingContext = wallet;
        }


        private async void  Button_OnClicked(object sender, EventArgs e)
        {
            var wallet = new Wallet();
            var walletView = new WalletView();
            wallet.Name = Name?.Text;
            wallet.Balance = Balance?.Text;
            wallet.Id = int.Parse(Id.Text);
            var result =  await walletView.PatchAsync(wallet);
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
    }
}