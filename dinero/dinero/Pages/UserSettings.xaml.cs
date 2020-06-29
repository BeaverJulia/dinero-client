using System;
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
            userRequest.Password = "not_null_field2";
            var result = await AccountView.PatchAsync(userRequest);
            var output = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Info edited", "User Info changed", "Ok");
                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
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
        private async void ChangePasswordBtn_Clicked(object sender, EventArgs e)
        {
            var changePasswordRequest = new ChangePassword();
            changePasswordRequest.OldPassword = OldPassword?.Text;
            changePasswordRequest.NewPassword = NewPassword?.Text;
            
            var result = await AccountView.PasswordChangeRequest(changePasswordRequest);
            var output = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Password changed", "User password changed", "Ok");
            }
            else
            {
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

        private void LogoutBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoginPage());
        }
    }
}