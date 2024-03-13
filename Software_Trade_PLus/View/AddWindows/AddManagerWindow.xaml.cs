using Software_Trade_PLus.Data;
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
    /// Логика взаимодействия для AddManagerWindow.xaml
    /// </summary>
    public partial class AddManagerWindow : Window
    {
        public AddManagerWindow()
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

        private void AddButton(object sender, RoutedEventArgs e)
        {
            string AddedName = NameField.Text;
            if(string.IsNullOrWhiteSpace(AddedName))
            {
                NamePromptField.Foreground = Brushes.Red;
                return;
            }
            DatabaseManagement.AddToDbManager(AddedName);
        }
    }
}
