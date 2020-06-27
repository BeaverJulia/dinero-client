using System;

namespace dinero.Models
{
    public static class ServerUrls
    {
        public const string Login= "https://dinero.azurewebsites.net/api/v1/accounts/login";
        public const string Register = "https://dinero.azurewebsites.net/api/v1/accounts/registration";
        public const string Wallets = "https://dinero.azurewebsites.net/api/v1/payments/wallets";
        public const string GetTransactions = "https://dinero.azurewebsites.net/api/v1/payments/transactions?limit=Po";
        public const string PostTransactions = "https://dinero.azurewebsites.net/api/v1/payments/transactions";
        public const string GetCurrencies = "https://dinero.azurewebsites.net/api/v1/payments/currencies";
    }
}