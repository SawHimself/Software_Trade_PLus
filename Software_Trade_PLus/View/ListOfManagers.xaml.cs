using Software_Trade_PLus.Data;
using Software_Trade_PLus.ViewModel;
using System;
using System.Windows.Controls;

namespace Software_Trade_PLus.View
{
    /// <summary>
    /// Логика взаимодействия для ListOfManagers.xaml
    /// </summary>
    public partial class ListOfManagers : UserControl
    {
        public ListOfManagers()
        {
            InitializeComponent();
            DataContext = new ListOfManagersVM();
        }
    }
}
