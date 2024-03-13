using Software_Trade_PLus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Software_Trade_PLus.View.AddWindows;
using Manager = Software_Trade_PLus.Models.Manager;
using System.Windows;
using Software_Trade_PLus.Utilities;
using System.Windows.Input;

namespace Software_Trade_PLus.ViewModel
{
    class ListOfManagersVM : INotifyPropertyChanged
    {
        private List<Manager> managers = DatabaseManagement.GetAllManagers();
        public List<Manager> Managers
        {
            get { return managers; }
            set
            {
                managers = value;
                NotifyPropertyChanged("Managers");
            }
        }


        //Методы открытия окон
        private void OpenAddManagerWindow()
        {
            AddManagerWindow newManagerWindow = new AddManagerWindow();
            //AddProductWindow newManagerWindow = new AddProductWindow();
            newManagerWindow.Owner = Application.Current.MainWindow;
            newManagerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            newManagerWindow.ShowDialog();
        }

        private RelayCommand openAddManagerWnd;
        public RelayCommand OpenAddManagerWnd
        {
            get
            {
                return openAddManagerWnd ?? new RelayCommand(obj =>
                {
                    OpenAddManagerWindow();
                }
                );
            }
        }

        //Удаление из базы данных
        private void deleteCommand(int id)
        {
            DatabaseManagement.RemoveFromDBManager(id);
            managers = DatabaseManagement.GetAllManagers();
            NotifyPropertyChanged("Managers");
            NotifyPropertyChanged("Clients");
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
    }
}
