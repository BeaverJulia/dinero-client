namespace dinero.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public string Balance { get; set; }
    }
}