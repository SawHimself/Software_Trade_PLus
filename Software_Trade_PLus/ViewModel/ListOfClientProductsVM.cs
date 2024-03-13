using Software_Trade_PLus.Models;
using Software_Trade_PLus.Utilities;
using Software_Trade_PLus.View.AddWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Software_Trade_PLus.ViewModel
{
    class ListOfClientProductsVM : INotifyPropertyChanged
    {
        private List<ActivatedSubscription>? searchClientSubscriptions;

        private List<ProductActivatedInfo>? productActivatedInfo;

        public List<ProductActivatedInfo>? ProductActivatedInfo
        {
            get { return productActivatedInfo; }
            set 
            { 
                productActivatedInfo = value;
                NotifyPropertyChanged("ProductActivatedInfo");
            }
        }

        public string ClientId { get; set; }
        public string ClientName { get; set; }

        private List<Product> allProducts = new List<Product>();
        public ICommand SearchCommand { get; private set; }
        public ListOfClientProductsVM()
        {
            SearchCommand = new RelayCommand(searchCommand);
        }
        private void searchCommand(object obj)
        {
            string str = ClientId;
            Client? SearchClient = DatabaseManagement.GetClientById(Convert.ToInt32(str));
            if (SearchClient != null)
            {
                ClientName = SearchClient.Name;
                searchClientSubscriptions = DatabaseManagement.GetProductsByClient(SearchClient.Id);

                //Получаем продукты для связывания имени с ProductId
                allProducts = DatabaseManagement.GetAllProducts();
                productActivatedInfo = (from subscription in searchClientSubscriptions
                                         join product in allProducts on subscription.ProductId equals product.Id
                                         select new ProductActivatedInfo
                                         {
                                             Id = subscription.Id,
                                             ProductName = product.Name,
                                             ProductPrice = product.Price,
                                             SubscriptionTermType = subscription.SubscriptionTermType,
                                             SubscriptionActivationDate = subscription.SubscriptionActivationDate
                                         }).ToList();
            }
            else
            {
                ProductActivatedInfo = null;
                ClientName = "Not Found";
            }
            NotifyPropertyChanged("ProductActivatedInfo");
            NotifyPropertyChanged("ClientName");
        }

        private void OpenAddActivatedSubscriptionWindow()
        {
            AddActivatedSubscriptionWindow SubscriptionWindow = new AddActivatedSubscriptionWindow();
            SubscriptionWindow.Owner = Application.Current.MainWindow;
            SubscriptionWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SubscriptionWindow.ShowDialog();
        }
        private RelayCommand openAddActivatedSubscriptionWnd;
        public RelayCommand OpenAddActivatedSubscriptionWnd
        {
            get
            {
                return openAddActivatedSubscriptionWnd ?? new RelayCommand(obj =>
                {
                    OpenAddActivatedSubscriptionWindow();
                }
                );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    class ProductActivatedInfo
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string SubscriptionTermType { get; set; }
        public DateTime SubscriptionActivationDate { get; set; }
    }
}
