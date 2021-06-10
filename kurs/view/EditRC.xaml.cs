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
    /// Логика взаимодействия для EditRC.xaml
    /// </summary>
    public partial class EditRC : Window
    {
        public EditRC()
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
                MessageBox.Show("Введите описание");
                return;
            }

            if (Period.Text == null || Period.Text == "")
            {
                MessageBox.Show("Неккоректно введена мощность");
                return;
            }

            this.DialogResult = true;
        }
    }
}
