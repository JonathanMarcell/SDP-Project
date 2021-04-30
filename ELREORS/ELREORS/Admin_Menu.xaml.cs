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
    /// Interaction logic for Admin_Menu.xaml
    /// </summary>
    public partial class Admin_Menu : Page
    {
        OracleConnection conn;
        DataTable dt;
        OracleCommandBuilder builder;
        public Admin_Menu()
        {
            InitializeComponent();
            conn = App.conn;
            loaddata();
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            Admin_Menu_Insert a = new Admin_Menu_Insert();
            a.ShowDialog();
            loaddata();
        }
        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            int idx = dg_Menu.SelectedIndex;
            if (idx<0)
            {
                return;
            }
            string id  = dt.Rows[idx]["ID"].ToString();
            string kode = dt.Rows[idx]["KODE"].ToString();
            string nm  = dt.Rows[idx]["NAMA"].ToString();
            string stat  = dt.Rows[idx]["STATUS"].ToString();
            string hrg  = dt.Rows[idx]["HARGA"].ToString();
            string ketr  = dt.Rows[idx]["KETERANGAN"].ToString();
            Admin_Menu_Update a = new Admin_Menu_Update(id,kode,nm,stat,hrg,ketr);
            a.ShowDialog();
            loaddata();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            loaddata();
        }

        public void loaddata()
        {
            if (tb_search.Text == "")
            {
                OracleDataAdapter da = new OracleDataAdapter(
                    "select ID, KODE_MENU as KODE , NAMA , CASE when STATUS = 1 then 'Aktif' ELSE 'NonAktif' END as STATUS , HARGA , KETERANGAN from menu order by 1", conn);
                dt = new DataTable();
                da.Fill(dt);
                dg_Menu.ItemsSource = dt.DefaultView;
            }
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select ID, KODE_MENU as KODE , NAMA , CASE when STATUS = 1 then 'Aktif' ELSE 'NonAktif' END as STATUS , HARGA , KETERANGAN from menu where lower(nama) like :param order by 1";
                cmd.Parameters.Add(":param", '%'+ tb_search.Text.ToLower() + '%');
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                dg_Menu.ItemsSource = dt.DefaultView;
            }
        }


    }
}
