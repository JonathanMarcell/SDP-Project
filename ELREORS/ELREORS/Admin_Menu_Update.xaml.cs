using Oracle.DataAccess.Client;
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
    /// Interaction logic for Admin_Menu_Update.xaml
    /// </summary>
    public partial class Admin_Menu_Update : Window
    {
        string Id;
        OracleConnection conn;
        public Admin_Menu_Update()
        {
            InitializeComponent();
            conn = App.conn;
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_pilihBahan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
