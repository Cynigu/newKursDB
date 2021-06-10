﻿using System;
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
    /// Логика взаимодействия для EditOP.xaml
    /// </summary>
    public partial class EditKom : Window
    {
        public EditKom()
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
            if (UnitM.Text == null || UnitM.Text == "")
            {
                MessageBox.Show("Введите кол-во");
                return;
            }
            if (ComboBox1.Text == null || ComboBox1.Text == "")
            {
                MessageBox.Show("Введите номер НП");
                return;
            }

            this.DialogResult = true;
        }
    }
}