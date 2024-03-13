using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Software_Trade_PLus.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string ClientStatus { get; set; } = null!; //Ключевой клиент, Обычный клиент
        public string Name { get; set; } = null!;
        [Required]
        public int ManagerId { get; set; }
        public List<ActivatedSubscription> ActivatedSubscriptions { get; set; } = null!;
        public Manager Manager { get; set; } = null!;

        //public ContactPerson ContactPerson { get; set; } = null!;
    }
}
