﻿using Software_Trade_PLus.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Software_Trade_PLus.View
{
    /// <summary>
    /// Логика взаимодействия для ListOfManagerClients.xaml
    /// </summary>
    public partial class ListOfManagerClients : UserControl
    {
        public ListOfManagerClients()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}
