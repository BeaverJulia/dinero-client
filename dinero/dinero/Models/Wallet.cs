namespace dinero.Models
{
    public class Wallet
    {
        public string Id { get; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public string Balance { get; set; }
    }

    public class Currency
    {
        public string name { get; set; }
        public string code { get; set; }
    }
}