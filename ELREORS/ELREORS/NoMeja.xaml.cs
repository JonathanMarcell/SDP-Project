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
    /// Interaction logic for NoMeja.xaml
    /// </summary>
    public partial class NoMeja : Window
    {
        OracleConnection conn;
        public NoMeja()
        {
            InitializeComponent();
            App.openconn();
            conn = App.conn;
            refresh();
        }
        int max;
        void refresh()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                string qry = "select *  from jumlahmeja";
                cmd = new OracleCommand(qry, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    max = Convert.ToInt32(dr[0]);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int parsedValue;
            if (tbNo.Text == "")
            {
                MessageBox.Show("Silahkan isi no meja");
            }
            else if (!int.TryParse(tbNo.Text, out parsedValue))
            {
                MessageBox.Show("harus diisi dengan angka");
                return;
            }
            else if (Convert.ToInt32(tbNo.Text)<=0 || Convert.ToInt32(tbNo.Text) > App.getJumlahMeja() )
            {
                MessageBox.Show("no meja harus lebih besar dari 0 atau no meja terlalu besar");
            }
            else
            {
                Meja m = new Meja(tbNo.Text);
                m.Show();
                this.Close();
            }
        }
    }
}
