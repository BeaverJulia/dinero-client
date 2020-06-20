namespace dinero.Models
{
    public class Wallet
    {
        public string Id { get; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public string Balance { get; set; }
    }
}