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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_Menu.xaml
    /// </summary>
    public partial class Admin_Menu : Page
    {
        public Admin_Menu()
        {
            InitializeComponent();
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            Admin_Menu_Insert a = new Admin_Menu_Insert();
            a.ShowDialog();
        }
    }
}
