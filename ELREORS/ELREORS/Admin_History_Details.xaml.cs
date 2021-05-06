using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Admin_History_Details.xaml
    /// </summary>
    public partial class Admin_History_Details : Window
    {
        DataTable dt = new DataTable();
        string id;
        public Admin_History_Details(string nonota,string id)
        {
            InitializeComponent();
            kodenota.Content = nonota;
            this.id = id;
            OracleDataAdapter da = new OracleDataAdapter(
                "select m.nama as Nama,  d.harga as Harga, d.JUMLAH as Jumlah , d.harga*d.jumlah as Subtotal " +
                "from djual d " +
                "left join menu m on d.id_menu = m.id " +
                $"where d.ID_HEADER={id} ", App.conn);
            da.Fill(dt);
            dgdetail.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
