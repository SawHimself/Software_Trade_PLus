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
    class ListOfClientsVM : INotifyPropertyChanged
    {
        private static List<Client> clients = DatabaseManagement.GetAllClients();

        private static List<Manager> managers = DatabaseManagement.GetAllManagers();

        private List<ClientManagerInfo> clientManagerInfos;

        public List<ClientManagerInfo> ClientManagerInfos
        {
            get { return clientManagerInfos; }
            set
            {
                clientManagerInfos = value;
                NotifyPropertyChanged("ClientManagerInfos");
            }
        }

        public ListOfClientsVM()
        {
            SelectClient();
        }


        private void OpenAddClientWindow()
        {
            AddClientWindow newClientWindow = new AddClientWindow();
            newClientWindow.Owner = Application.Current.MainWindow;
            newClientWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            newClientWindow.ShowDialog();
        }

        private RelayCommand openAddClientWnd;
        public RelayCommand OpenAddClientWnd
        {
            get
            {
                return openAddClientWnd ?? new RelayCommand(obj =>
                {
                    OpenAddClientWindow();
                }
                );
            }
        }

        private void SelectKeyClients()
        {
            clientManagerInfos = (from client in clients
                                  join manager in managers on client.ManagerId equals manager.Id
                                  where client.ClientStatus == "Key client"
                                  select new ClientManagerInfo
                                  {
                                      ClientName = client.Name,
                                      ManagerName = manager.Name,
                                      ClientId = client.Id,
                                      ClientStatus = client.ClientStatus
                                  }).ToList();
        }

        public RelayCommand SelectKeyClient
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    SelectKeyClients();
                    NotifyPropertyChanged("ClientManagerInfos");
                }
                );
            }
        }

        private void SelectRegularClients()
        {
            ClientManagerInfos = (from client in clients
                                  join manager in managers on client.ManagerId equals manager.Id
                                  where client.ClientStatus == "Regular client"
                                  select new ClientManagerInfo
                                  {
                                      ClientName = client.Name,
                                      ManagerName = manager.Name,
                                      ClientId = client.Id,
                                      ClientStatus = client.ClientStatus
                                  }).ToList();
        }
        public RelayCommand SelectRegularClient
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    SelectRegularClients();
                    NotifyPropertyChanged("ClientManagerInfos");
                }
                );
            }
        }

        //Удаление из базы данных
        private void deleteCommand(int id)
        {
            DatabaseManagement.RemoveFromDBClient(id);
            SelectClient();
            NotifyPropertyChanged("ClientManagerInfos");
            NotifyPropertyChanged("Clients");
            NotifyPropertyChanged("ActivatedSubscription");
        }

        //Переписать в RelayCommand
        public ICommand DeleteCommand
        {
            get { return new RelayCommand(id => deleteCommand((int)id)); }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void SelectClient()
        {
            clients = DatabaseManagement.GetAllClients();
            managers = DatabaseManagement.GetAllManagers();
            ClientManagerInfos = (from client in clients
                                  join manager in managers on client.ManagerId equals manager.Id
                                  select new ClientManagerInfo
                                  {
                                      ClientName = client.Name,
                                      ManagerName = manager.Name,
                                      ClientId = client.Id,
                                      ClientStatus = client.ClientStatus
                                  }).ToList();
        }
    }


    class ClientManagerInfo
    {
        public string ClientName { get; set; }
        public string ManagerName { get; set; }
        public int ClientId { get; set; }
        public string ClientStatus { get; set; }
    }
}