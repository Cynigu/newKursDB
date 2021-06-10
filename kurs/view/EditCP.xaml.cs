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
    /// Логика взаимодействия для EditCP.xaml
    /// </summary>
    public partial class EditCP : Window
    {
        public EditCP()
        {
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Name1.Text == null || Name1.Text == "")
            {
                MessageBox.Show("Введите статус");
                return;
            }

            this.DialogResult = true;
        }
    }
}
