using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWallet : ContentPage
    {
        public WalletView WalletView;
        public AddWallet()
        {
            InitializeComponent();
            BindingContext = new PickerMVVMViewModel();
        }

        private async void BtnCreate_OnClicked(object sender, EventArgs e)
        {
           WalletView = new WalletView();
           var wallet = new Wallet();
           var currency = new Currency();
           currency = (Currency)blaPicker.SelectedItem;
           wallet.Currency = currency;
           wallet.Name = txtName.Text;
           wallet.Balance = txtBalance.Text;
      
           if (string.IsNullOrEmpty(wallet.Balance))
           {
               await DisplayAlert("Validation errors", "Please provide correct Balance", "Ok");
               return;
           }

           if (string.IsNullOrEmpty(wallet.Name))
           {
               await DisplayAlert("Validation errors", "Name is required", "Ok");
               return;
           }
           var result = await WalletView.CreateRequest(wallet);
           var output = await result.Content.ReadAsStringAsync();
           if (result.IsSuccessStatusCode)
           {

               await DisplayAlert("Wallet Created", "Wallet " +wallet.Name+ " created", "Ok");
               await Navigation.PushModalAsync(new PanelPage());
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
    }
}