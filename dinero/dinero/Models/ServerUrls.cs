
namespace dinero.Models
{
    public static class ServerUrls
    {
        public const string Login= "https://dinero.azurewebsites.net/api/v1/accounts/login";
        public const string Register = "https://dinero.azurewebsites.net/api/v1/accounts/registration";
        public const string LoggedUser = "https://dinero.azurewebsites.net/api/v1/accounts/me";
        public const string Wallets = "https://dinero.azurewebsites.net/api/v1/payments/wallets";
        public const string GetTransactions = "https://dinero.azurewebsites.net/api/v1/payments/transactions?limit=";
        public const string PostTransactions = "https://dinero.azurewebsites.net/api/v1/payments/transactions";
        public const string GetCurrencies = "https://dinero.azurewebsites.net/api/v1/payments/currencies";
        public const string ExchangeCurrencies = "https://dinero.azurewebsites.net/api/v1/payments/exchange-currency";
        public const string UserSearch = "https://dinero.azurewebsites.net/api/v1/accounts/users";
        public const string GetPassword = "https://dinero.azurewebsites.net/api/v1/accounts/activate-account";
        public const string ResendEmail = "https://dinero.azurewebsites.net/api/v1/accounts/resend-email";
    }
}