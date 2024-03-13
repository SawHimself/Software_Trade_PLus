using Software_Trade_PLus.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Software_Trade_PLus.Data
{
    public class STPdbContext : DbContext
    {
        public DbSet<ActivatedSubscription> ActivatedSubscriptions => Set<ActivatedSubscription>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<ContactPerson> ContactPersons => Set<ContactPerson>();
        public DbSet<Manager> Managers => Set<Manager>();
        public DbSet<Product> Products => Set<Product>();

        public STPdbContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Установка конфигурации осуществляется с помощью файла DataBaseSetting.json

            var config = new ConfigurationBuilder()
                                    .AddJsonFile("Data/DataBaseSettings.json")
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .Build();

            //optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
