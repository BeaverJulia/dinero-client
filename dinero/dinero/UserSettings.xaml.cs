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
            if (string.IsNullOrEmpty(userRequest.Password))
            {
                await DisplayAlert("Password empty", "To change settings please provide password", "Ok");
                return;
            }
            if (result.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<ResultMessage>(output);
                await DisplayAlert("Info edited", "User Info changed", "Ok");
                await Navigation.PushModalAsync(new UserSettings());
            }
            else
            {
                //TODO - change this validation
                try
                {
                    var response = JsonConvert.DeserializeObject<ResultMessage>(output);
                    await DisplayAlert("Error", response.Message, "Ok");
                }
                catch
                {
                    await DisplayAlert("Error", result.StatusCode.ToString(), "Ok");

                }
            }
        }

    }
}