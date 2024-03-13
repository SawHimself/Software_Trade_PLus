using Software_Trade_PLus.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
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

        private void OnlyDouble(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            StringBuilder newText = new StringBuilder();

            foreach (char c in textBox.Text)
            {
                if (char.IsDigit(c))
                {
                    newText.Append(c);
                }
                if (c == ',' || c == '.')
                {
                    newText.Append(',');
                }
            }
            textBox.Text = newText.ToString();
            textBox.SelectionStart = textBox.Text.Length;
            textBox.Focus();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            bool ErrorReturn = false;
            if(String.IsNullOrWhiteSpace(NameField.Text))
            {
                ErrorReturn = true;
                NamePromptField.Foreground = Brushes.Red;
            }
            if(String.IsNullOrWhiteSpace(PriceField.Text))
            {
                ErrorReturn = true;
                PricePromptField.Foreground = Brushes.Red;
                
            }
            if (ErrorReturn)
                return;

            string AddedName = NameField.Text;
            double AddedPrice = Convert.ToDouble(PriceField.Text);
            DatabaseManagement.AddToDbProduct(AddedName, AddedPrice);
            //int x = 0;
        }
    }
}
