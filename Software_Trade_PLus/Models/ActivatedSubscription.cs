using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Software_Trade_PLus.Models
{
    public class ActivatedSubscription
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public DateTime SubscriptionActivationDate { get; set; } //Дата активации 
        public string SubscriptionTermType { get; set; } = null!; //месяц, квартал, год
        public Product Product { get; set; } = null!;
        public Client Client { get; set; } = null!;
    }
}
