using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Software_Trade_PLus.Models;
using Software_Trade_PLus.Utilities;
using System.Windows;
using Software_Trade_PLus.View.AddWindows;
using System.Windows.Input;

namespace Software_Trade_PLus.ViewModel
{
    class ListOfProductsVM : INotifyPropertyChanged
    {
        public List<Product> products = DatabaseManagement.GetAllProducts();

        public List<Product> Products
        {
            get { return products; }
            set
            {
                products = value;

                NotifyPropertyChanged("Products");
            }
        }

        private void OpenAddProductWindow()
        {
            AddProductWindow newProductWindow = new AddProductWindow();
            newProductWindow.Owner = Application.Current.MainWindow;
            newProductWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            newProductWindow.ShowDialog();
        }

        private RelayCommand openAddProductWnd;
        public RelayCommand OpenAddProductWnd
        {
            get
            {
                return openAddProductWnd ?? new RelayCommand(obj =>
                {
                    OpenAddProductWindow();
                });
            }
        }

        private void deleteCommand(int id)
        {
            DatabaseManagement.RemoveFromDBProduct(id);
            products = DatabaseManagement.GetAllProducts();
            NotifyPropertyChanged("Products");
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
