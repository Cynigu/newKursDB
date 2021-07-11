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
    /// Логика взаимодействия для WindowUOTGP.xaml
    /// </summary>
    public partial class WindowUOTGP : Window
    {
        private void UnitM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
        public WindowUOTGP()
        {
            InitializeComponent();
        }
    }
}
