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

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent(); 
            App.openconn(); 
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            Main.Content = new Admin_History();
        }

        private void btn_history_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Admin_History();
        }

        private void btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Admin_Menu();
        }

        private void btn_Stock_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Admin_Stock();
        }

        private void btn_Setting_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Admin_Setting();
        }

    }
}
