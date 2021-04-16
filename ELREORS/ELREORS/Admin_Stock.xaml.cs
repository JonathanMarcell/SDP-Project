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
using Oracle.DataAccess.Client;
using System.Data;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_Stock.xaml
    /// </summary>
    public partial class Admin_Stock : Page
    {
        OracleConnection conn;
        DataTable dt;
        OracleCommandBuilder builder;
        public Admin_Stock()
        {
            InitializeComponent();
            conn = App.conn;
            loaddata();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            loaddata();
        }
        public void loaddata()
        {
            if (tb_search.Text=="")
            {
                OracleDataAdapter da = new OracleDataAdapter(
                    "select  * from bahan", conn);
                dt = new DataTable();
                da.Fill(dt);
                dg_Stock.ItemsSource = dt.DefaultView;
            }
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select* from bahan where lower(nama) like :param ";
                cmd.Parameters.Add(":param", '%' + tb_search.Text.ToLower() + '%');
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                dg_Stock.ItemsSource = dt.DefaultView;
            }
        }
    }
}
