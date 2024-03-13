using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Software_Trade_PLus.View.AddWindows;
using System.Windows;
using Software_Trade_PLus.Utilities;
using Software_Trade_PLus.Models;
using System;
using System.Windows.Input;

namespace Software_Trade_PLus.ViewModel
{
    class ListOfManagerClientsVM : INotifyPropertyChanged
    {
        private List<Client>? searchManagerClients;

        public string ManagerId { get; set; }
        public string ManagerName { get; set; }
        public List<Client> SearchManagerClients
        {
            get { return searchManagerClients; }
            set 
            { 
                searchManagerClients = value;
                NotifyPropertyChanged("SearchManagerClients");
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
        public ICommand TestCommand { get; private set; }
        public ListOfManagerClientsVM()
        {
            TestCommand = new RelayCommand(TestClick);
        }
        private void TestClick(object obj)
        {
            // Ваша логика обработки клика кнопки "Test"
            string str = ManagerId;
            Manager? SearchManager = DatabaseManagement.GetManagerById(Convert.ToInt32(str));
            if (SearchManager != null)
            {
                ManagerName = SearchManager.Name;
                searchManagerClients = DatabaseManagement.GetClientsByManager(SearchManager.Id);
            }
            else
            {
                searchManagerClients = null;
                ManagerName = "Not Found";
            }
            NotifyPropertyChanged("SearchManagerClients");
            NotifyPropertyChanged("ManagerName");
        }
    }
}