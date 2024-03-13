using Software_Trade_PLus.Models;
using Software_Trade_PLus.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Software_Trade_PLus.View.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }
        private void btnClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        string AddedStatus = "";

        private void AddButton(object sender, RoutedEventArgs e)
        {
            int AddedManagerId = 0;

            Manager? searchManager = null;

            bool ErrorReturn = false;
            if (String.IsNullOrWhiteSpace(NameField.Text))
            {
                ErrorReturn = true;
                NamePropmptString.Foreground = Brushes.Red;
            }
            // На всякий случай
            if (String.IsNullOrWhiteSpace(StatusField.Text))
            {
                ErrorReturn = true;
            }
            else
            {
                if(StatusField.SelectedIndex == 0)
                {
                    AddedStatus = "Key client";
                }
                else if(StatusField.SelectedIndex == 1)
                {
                    AddedStatus = "Regular client";
                }
            }
            if(String.IsNullOrWhiteSpace(ManagerIdField.Text))
            {
                ErrorReturn = true;
                ManagerIdPromptString.Foreground = Brushes.Red;
            }
            else
            {
                AddedManagerId = Convert.ToInt32(ManagerIdField.Text);
                searchManager = DatabaseManagement.GetManagerById(AddedManagerId);
                if(searchManager == null)
                {
                    ErrorReturn = true;
                    ManagerIdPromptString.Text += "\nNot Found";
                    ManagerIdPromptString.Foreground = Brushes.Red;
                }
            }
            if(String.IsNullOrWhiteSpace(ContactPersonField.Text))
            {
                ErrorReturn=true;
                ContactPersonPromptString.Foreground= Brushes.Red;
            }

            if(ErrorReturn)
            {
                return;
            }


            string AddedName = NameField.Text;
            string AddedPersonName = ContactPersonField.Text;
            DatabaseManagement.AddToDbClient(AddedName, AddedStatus, AddedManagerId, AddedPersonName);
            /*NotifyPropertyChanged("ClientManagerInfos");*/
        }
        private void OnlyInt(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            StringBuilder newText = new StringBuilder();

            foreach (char c in textBox.Text)
            {
                if (char.IsDigit(c))
                {
                    newText.Append(c);
                }
            }
            textBox.Text = newText.ToString();
            textBox.SelectionStart = textBox.Text.Length;
            textBox.Focus();
        }

/*        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }*/
    }
}
