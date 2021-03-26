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

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (tb_username.Text=="admin")
            {
                Admin a = new Admin();
                a.Show();
                Close();
            }
            else if (tb_username.Text=="meja1" || tb_username.Text == "meja2" || tb_username.Text == "meja3" || tb_username.Text == "meja4" || tb_username.Text == "meja5" ||
                tb_username.Text == "meja6" || tb_username.Text == "meja7" || tb_username.Text == "meja8" || tb_username.Text == "meja9" || tb_username.Text == "meja10")
            {
                Meja a = new Meja(tb_username.Text);
                a.Show();
                Close();
            }
            else if (tb_username.Text=="kasir")
            {
                Kasir a = new Kasir();
                a.Show();
                Close();
            }
            else if (tb_username.Text=="list")
            {
                ListPesanan a = new ListPesanan();
                a.Show();
                Close();
            }
            
        }
    }
}
