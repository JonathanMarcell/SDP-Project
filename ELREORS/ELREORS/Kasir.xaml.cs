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
    /// Interaction logic for Kasir.xaml
    /// </summary>
    public partial class Kasir : Window
    {
        private OracleConnection conn;
        DataTable dt;
        int nomeja;
        public Kasir(OracleConnection conn)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.conn = conn;

            labelTanggal.Content = "Tanggal : "+ DateTime.Now.ToString().Substring(0,10);
            loadData();
        }

        void loadData()
        {
            string query = "select distinct hj.ID as \"No\", hj.KODE_HJUAL as \"Kode HJUAL\", to_char(hj.TANGGAL, 'dd/MM/yyyy') as \"Tanggal\", mn.NAMA as \"Menu\", pg.NAMA as \"Pegawai\", case when hj.STATUS>1 then 'Sudah Dikonfirmasi' else 'Belum Dikonfirmasi' end as \"Status\" from HJUAL hj, PEGAWAI pg, DJUAL dj, MENU mn where hj.ID_PEGAWAI = pg.ID and hj.ID = dj.ID_HEADER and mn.ID = dj.ID_MENU and nomor_meja = " + nomeja;
            OracleDataAdapter da = new OracleDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);

            dgKasir.ItemsSource = dt.DefaultView;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 1";
            nomeja = 1;
            loadData();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 2";
            nomeja = 2;
            loadData();
        }

        private void btn3_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 3";
            nomeja = 3;
            loadData();
        }

        private void btn4_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 4";
            nomeja = 4;
            loadData();
        }

        private void btn5_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 5";
            nomeja = 5;
            loadData();
        }

        private void btn6_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 6";
            nomeja = 6;
            loadData();
        }

        private void btn7_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 7";
            nomeja = 7;
            loadData();
        }

        private void btn8_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 8";
            nomeja = 8;
            loadData();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA";
            MainWindow mainwin = new MainWindow();
            mainwin.Show();
        }
    }
}
