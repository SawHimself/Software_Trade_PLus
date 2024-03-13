using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Software_Trade_PLus.Utilities;
using System.Windows.Input;
using Software_Trade_PLus.View;

namespace Software_Trade_PLus.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand ListOfClientProductsCommand { get; set; }
        public ICommand ListOfClientsCommand { get; set; }
        public ICommand ListOfManagerClientsCommand { get; set; }
        public ICommand ListOfManagersCommand { get; set; }
        public ICommand ListOfProductsCommand { get; set; }
        public ICommand HomeCommand { get; set; }


        private void Home(object obj) => CurrentView = new HomeVM();
        private void ListOfClientProducts(object obj) => CurrentView = new ListOfClientProductsVM();
        private void ListOfClients(object obj) => CurrentView = new ListOfClientsVM();
        private void ListOfManagerClients(object obj) => CurrentView = new ListOfManagerClientsVM();
        private void ListOfManagers(object obj) => CurrentView = new ListOfManagersVM();
        private void ListOfProducts(object obj) => CurrentView = new ListOfProductsVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            ListOfManagerClientsCommand = new RelayCommand(ListOfManagerClients);
            ListOfClientProductsCommand = new RelayCommand(ListOfClientProducts);
            ListOfClientsCommand = new RelayCommand(ListOfClients);
            ListOfManagersCommand = new RelayCommand(ListOfManagers);
            ListOfProductsCommand = new RelayCommand(ListOfProducts);

            //Startup Page
            CurrentView = new HomeVM();
        }
}
}
