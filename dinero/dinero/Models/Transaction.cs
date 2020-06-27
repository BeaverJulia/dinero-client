using System;
using Newtonsoft.Json;

namespace dinero.Models
{
    public class Transaction
    {
        public int Id { get; }
        public string Status { get; set; }
        [JsonProperty("From_User")]
        public User FromUser { get; set; }
        public User To_User { get; set; }
        public float Amount { get; set; }
        public Currency Currency { get; set; }
        public string Paid_At { get; set; }
    }
}