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
    /// Логика взаимодействия для EditNP.xaml
    /// </summary>
    public partial class EditNP : Window
    {
        public EditNP()
        {
            InitializeComponent();
        }
        private void Tdokaz_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
        private void Kdokaz_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
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
                MessageBox.Show("Введите наименование");
                return;
            }
            if (UnitM.Text == null || UnitM.Text == "")
            {
                MessageBox.Show("Введите единицу измерения");
                return;
            }

            this.DialogResult = true;
        }
    }
}
