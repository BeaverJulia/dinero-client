
namespace dinero.Models
{
    public static class ServerUrls
    {
        private const string BaseUrl = "https://dinero.azurewebsites.net/api/v1";
        public const string Login= BaseUrl + "/accounts/login";
        public const string Register = BaseUrl + "/accounts/registration";
        public const string LoggedUser = BaseUrl + "/accounts/me";
        public const string Wallets = BaseUrl + "/payments/wallets";
        public const string GetTransactions = BaseUrl + "/payments/transactions?limit=";
        public const string PostTransactions = BaseUrl + "/payments/transactions";
        public const string GetCurrencies = BaseUrl + "/payments/currencies";
        public const string ExchangeCurrencies = BaseUrl + "/payments/exchange-currency";
        public const string UserSearch = BaseUrl + "/accounts/users";
        public const string GetPassword = BaseUrl + "/accounts/activate-account";
        public const string ResendEmail = BaseUrl + "/accounts/resend-email";
        public const string ChangePassword = BaseUrl + "/accounts/password";
    }
}