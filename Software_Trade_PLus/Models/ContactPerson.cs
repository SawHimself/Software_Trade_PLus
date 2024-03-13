using System.ComponentModel.DataAnnotations;

namespace Software_Trade_PLus.Models
{
    public class ContactPerson
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Required]

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        //Контактная информация
    }
}
