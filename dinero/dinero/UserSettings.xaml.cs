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
    public partial class UserSettings : ContentPage
    {
        public AccountView AccountView;
        public UserRegister User;
        public UserSettings()
        {
            AccountView = new AccountView();
            InitializeComponent();
            BindingContext = AccountView.GetUserInfo().Result;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var userRequest = new UserRegister();
            userRequest.Name = Name?.Text;
            userRequest.Email = Email?.Text;
            userRequest.Password = password.Text;
            var result = await AccountView.PatchAsync(userRequest);
            var output = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<ResultMessage>(output);
                await DisplayAlert("Wallet edited", response.Message, "Ok");
                await Navigation.PushModalAsync(new UserSettings());
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