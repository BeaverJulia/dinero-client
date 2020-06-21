using System;

namespace dinero.Models
{
    public class ServerUris
    {
        public ServerUris()
        {
            BaseUri = "https://dinero.azurewebsites.net/api/v1";
            LoginUri = new Uri(BaseUri + "/accounts​/login");
            RegisterUri = new Uri(BaseUri + "/accounts/registration");
            CreateWalletUri = new Uri(BaseUri + "/payments/wallets");
            GetWalletsUri = new Uri(BaseUri + "/payments/wallets");
            GetWalletDetailsUri = new Uri(BaseUri + "");
        }

        public static string BaseUri { get; set; }
        public static Uri LoginUri { get; set; }
        public static Uri RegisterUri { get; set; }
        public static Uri CreateWalletUri { get; set; }
        public static Uri GetWalletsUri { get; set; }
        public static Uri GetWalletDetailsUri { get; set; }
    }
}