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
    /// Логика взаимодействия для ListOfClientProducts.xaml
    /// </summary>
    public partial class ListOfClientProducts : UserControl
    {
        public ListOfClientProducts()
        {
            InitializeComponent();
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
    }
}
