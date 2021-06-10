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

namespace kurs.view
{
    /// <summary>
    /// Логика взаимодействия для EditUOM.xaml
    /// </summary>
    public partial class EditUOM : Window
    {
        public EditUOM()
        {
            InitializeComponent();
        }
        private void Period_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Name1.Text == null || Name1.Text == "")
            {
                MessageBox.Show("Введите операцию");
                return;
            }

            if (Period.Text == null || Period.Text == "")
            {
                MessageBox.Show("Введите дату начала");
                return;
            }

            this.DialogResult = true;
        }
    }
}
