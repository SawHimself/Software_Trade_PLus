using Software_Trade_PLus.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для AddActivatedSubscriptionWindow.xaml
    /// </summary>
    public partial class AddActivatedSubscriptionWindow : Window
    {
        public AddActivatedSubscriptionWindow()
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
        private void TextBox_TextToDate(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            StringBuilder newText = new StringBuilder();

            if(textBox.Text.Length >= 16)
            {
                for(int i =0; i < 16; i++)
                {
                    newText.Append(textBox.Text[i]);
                }
                textBox.Text = newText.ToString();
                textBox.SelectionStart = textBox.Text.Length;
                textBox.Focus();
                return;
            }

            foreach (char c in textBox.Text)
            {
                if (char.IsDigit(c))
                {
                    newText.Append(c);
                }
                else
                {
                    if (c == '.')
                    {
                        if(newText.Length == 2 || newText.Length == 5)
                        {
                            newText.Append(c);
                        }
                        else if(newText.Length == 1 || newText.Length == 4)
                        {
                            newText.Insert(newText.Length - 1, '0');
                        }
                    }
                    if (c == ' ' && newText.Length == 10)
                    {
                        newText.Append(c);
                    }
                    if (c == ':')
                    {
                        if(newText.Length == 13)
                            newText.Append(c);
                        if(newText.Length == 12)
                            newText.Insert(newText.Length - 1, '0');
                    }
                }
            }
            if(newText.Length == 2 || newText.Length == 5)
            {
                newText.Append('.');
            }
            if(newText.Length == 10)
            {
                newText.Append(' ');
            }
            if(newText.Length == 13)
            {
                newText.Append(':');
            }
            textBox.Text = newText.ToString();
            textBox.SelectionStart = textBox.Text.Length;
            textBox.Focus();
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
        private void AddButton(object sender, RoutedEventArgs e)
        {
            if(ActivationDate.Text.Length == 16)
            {
                bool ErrorReturn = false;
                if(String.IsNullOrWhiteSpace(ClientId.Text))
                {
                    ClientId.BorderBrush = Brushes.Red;
                    ErrorReturn = true;
                }
                if(String.IsNullOrEmpty(ProductId.Text))
                {
                    ProductId.BorderBrush = Brushes.Red;
                    ErrorReturn = true;
                }
                if(string.IsNullOrEmpty(ActivationDate.Text))
                {
                    ActivationDate.BorderBrush = Brushes.Red;
                    ErrorReturn = true;
                }
                if(ErrorReturn)
                {
                    return;
                }

                DateTime dateTime = DateTime.Now;
                try
                {
                    dateTime = DateTime.ParseExact(ActivationDate.Text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                catch
                {
                    ErrorReturn = true;
                    DatePromptString.Text += "\nInput Error";
                    DatePromptString.Foreground = Brushes.Red;
                    ActivationDate.Text = "";
                    ActivationDate.BorderBrush = Brushes.Red;
                }

                Client? searchClient = DatabaseManagement.GetClientById(Convert.ToInt32(ClientId.Text));
                Product? searchProduct = DatabaseManagement.GetProductById(Convert.ToInt32(ProductId.Text));

                if(searchClient == null)
                {
                    ErrorReturn = true;
                    ClientId.Text += "\nNot Found";
                    ClientId.Foreground = Brushes.Red;
                }
                if (searchProduct == null)
                {
                    ErrorReturn = true;
                    ProductId.Text += "\nNot Found";
                    ProductId.Foreground = Brushes.Red;
                }
                if (ErrorReturn)
                {
                    return;
                }

                string subscriptionType = "Month";
                if(SubscriptionType.SelectedIndex == 0)
                {
                    subscriptionType = "Month";
                }
                if (SubscriptionType.SelectedIndex == 1)
                {
                    subscriptionType = "Quarter";
                }
                if (SubscriptionType.SelectedIndex == 2)
                {
                    subscriptionType = "Year";
                }

                DatabaseManagement.AddToDbActivatedSubscription(searchClient.Id, searchProduct.Id, dateTime, subscriptionType);
            }
        }

    }
}
