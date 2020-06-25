using System;

namespace dinero.Models
{
    public class Transaction
    {
        public int Id { get; }
        public string Status { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
        public float Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime PaidAt { get; set; }
    }
}