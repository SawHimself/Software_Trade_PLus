using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Software_Trade_PLus.Data;
using Software_Trade_PLus.View;

namespace Software_Trade_PLus.Models
{
    public static class DatabaseManagement
    {
        public static void ConnectDatabase()
        {
            using(STPdbContext db = new STPdbContext())
            {

            }
        }
        //Get all managers
        public static List<Manager> GetAllManagers()
        {
            using (STPdbContext db = new STPdbContext())
            {
                var result = db.Managers.ToList();
                return result;
            }
        }
        //Get all clients
        public static List<Client> GetAllClients()
        {
            using (STPdbContext db = new STPdbContext())
            {
                var result = db.Clients.ToList();
                return result;
            }
        }

        //get list clients by manager id
        public static List<Client> GetClientsByManager(int managerId)
        {
            using (STPdbContext db = new STPdbContext())
            {
                var result = db.Clients.Where(client => client.ManagerId == managerId).ToList();
                return result;
            }
        }

        //get list products by client id
        public static List<ActivatedSubscription> GetProductsByClient(int ClientId)
        {
            using (STPdbContext db = new STPdbContext())
            {
                var result = db.ActivatedSubscriptions.Where(product => product.ClientId == ClientId).ToList();
                return result;
            }
        }

        //Get manager by id

        public static Manager? GetManagerById(int managerId)
        {
            using(STPdbContext db = new STPdbContext())
            {
                var result = db.Managers.Where(manager => manager.Id == managerId).FirstOrDefault();
                return result;
            }
        }

        //Get client by id
        public static Client? GetClientById(int clientId)
        {
            using (STPdbContext db = new STPdbContext())
            {
                var result = db.Clients.Where(client => client.Id == clientId).FirstOrDefault();
                return result;
            }
        }

        //Get product by id
        public static Product? GetProductById(int productId)
        {
            using (STPdbContext db = new STPdbContext())
            {
                var result = db.Products.Where(product => product.Id == productId).FirstOrDefault();
                return result;
            }
        }

        //Get all products

        public static List<Product> GetAllProducts()
        {
            using (STPdbContext db = new STPdbContext())
            {
                var result = db.Products.ToList();
                return result;
            }
        }
        //Add to database
        public static void AddToDbManager(string name)
        {
            using(STPdbContext db = new STPdbContext())
            {
                //проверяем существует ли менеджер
                bool checkIsExist = db.Managers.Any(el => el.Name == name);
                if(!checkIsExist)
                {
                    Manager newManager = new Manager{ Name = name };
                    db.Managers.Add(newManager);
                    db.SaveChanges();
                }
            }
        }

        public static void AddToDbProduct(string name, double price)
        {
            using(STPdbContext db = new STPdbContext())
            {
                bool checkIsExist = db.Products.Any(el => el.Name == name);
                if(!checkIsExist)
                {
                    Product newProduct = new Product 
                    { 
                        Name = name, 
                        Price = price 
                    };
                    db.Products.Add(newProduct);
                    db.SaveChanges();
                }
            }
        }

        public static void AddToDbClient(string name, string clientStatus, int managerId, string personName)
        {
            using(STPdbContext db = new STPdbContext())
            {
                bool checkIsExist = db.Clients.Any(el => el.Name == name);
                if(!checkIsExist)
                {
                    Client newClient = new Client
                    {
                        Name = name,
                        ClientStatus = clientStatus,
                        ManagerId = managerId
                    };
                    db.Clients.Add(newClient);
                    db.SaveChanges();

                    ContactPerson newcontactPerson = new ContactPerson
                    {
                        Name = personName,
                        ClientId = newClient.Id
                    };
                    db.ContactPersons.Add(newcontactPerson);
                    db.SaveChanges();
                }
            }
        }
        public static void AddToDbActivatedSubscription(int clientId, 
                                                        int productId,
                                                        DateTime subscriptionActivationDate,
                                                        string subscriptionTermType
                                                        )
        {
            using (STPdbContext db = new STPdbContext())
            {
                bool checkIsExist = db.ActivatedSubscriptions.Any(el => el.ProductId == productId && el.ClientId == clientId && el.SubscriptionActivationDate == subscriptionActivationDate);
                if (!checkIsExist)
                {
                    ActivatedSubscription activatedSubscription = new ActivatedSubscription()
                    {
                        SubscriptionActivationDate = subscriptionActivationDate,
                        SubscriptionTermType = subscriptionTermType,
                        ClientId = clientId,
                        ProductId = productId
                    };
                    db.ActivatedSubscriptions.Add(activatedSubscription);
                    db.SaveChanges();
                }
            }
        }
        public static void AddToDbContactPerson(string name)
        {
            using (STPdbContext db = new STPdbContext())
            {
                bool checkIsExist = db.ContactPersons.Any(el => el.Name == name);
                if(!checkIsExist)
                {
                    ContactPerson person = new ContactPerson() 
                    { 
                        Name = name
                    };
                    db.ContactPersons.Add(person);
                }
            }
        }

        //Delete from database
        public static void RemoveFromDBManager(int managerId)
        {
            using(STPdbContext db = new STPdbContext())
            {
                var manager = db.Managers.FirstOrDefault(m => m.Id == managerId);
                if (manager != null)
                {
                    db.Managers.Remove(manager);
                    db.SaveChanges();
                }
            }
        }
        public static void RemoveFromDBProduct(int productId)
        {
            using(STPdbContext db = new STPdbContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
        }
        public static void RemoveFromDBClient(int clientId)
        {
            using(STPdbContext db = new STPdbContext())
            {
                var client = db.Clients.FirstOrDefault(p => p.Id == clientId);
                if (client != null)
                {
                    db.Clients.Remove(client);
                    db.SaveChanges();
                }
            }
        }
        public static void RemoveFromDBActivatedSubscription(int activatedSubscriptionId)
        {
            using(STPdbContext db = new STPdbContext())
            {
                var activatedSubscription = db.ActivatedSubscriptions.FirstOrDefault(p => p.Id == activatedSubscriptionId);
                if(activatedSubscription != null)
                {
                    db.ActivatedSubscriptions.Remove(activatedSubscription);
                    db.SaveChanges();
                }
            }
        }

        //Person не существует сам по себе и является частью таблицы client, так же как и client не существует без Person 
/*        public static void RemoveFromDBContactPerson(ContactPerson person)
        {
            using(STPdbContext db = new STPdbContext())
            {
                db.ContactPersons.Remove(person);
                db.SaveChanges();
            }
        }*/

        //Edit in database

    }
}
