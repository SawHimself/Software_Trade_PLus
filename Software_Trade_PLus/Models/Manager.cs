using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Trade_PLus.Models
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Client> Clients { get; set; } = null!;
    }
}
