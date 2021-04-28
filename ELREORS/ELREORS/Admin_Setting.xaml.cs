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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_Setting.xaml
    /// </summary>
    public partial class Admin_Setting : Page
    {
        public Admin_Setting()
        {
            InitializeComponent();
            tbJumlahMeja.Text = App.getJumlahMeja() + "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string qry = "update jumlahmeja set value=:jum";
                OracleCommand cmd = new OracleCommand(qry, App.conn);
                cmd.Parameters.Add("jum",tbJumlahMeja.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil menyimpan");

            }
            catch (Exception)
            {
                MessageBox.Show("Gagal menyimpan");
                throw;
            }
        }
    }
}
