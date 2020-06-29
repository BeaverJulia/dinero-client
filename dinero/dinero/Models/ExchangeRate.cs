namespace dinero.Models
{
    public class ExchangeRate
    {
        public Currency from_currency { get; set; }
        public Currency to_currency { get; set; }
        public float amount { get; set; }
        public string updated_at { get; set; }
    }
}