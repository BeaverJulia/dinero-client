namespace dinero.Models
{
    public class Exchange
    {
        public int from_wallet { get; set; }
        public int to_wallet { get; set; }
        public float from_amount { get; set; }
    }
}