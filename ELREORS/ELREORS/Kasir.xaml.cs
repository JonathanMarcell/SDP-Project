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
        public Kasir()
        {
            InitializeComponent();
            App.openconn();
            this.WindowState = WindowState.Maximized;
            this.conn = App.conn;

            labelTanggal.Content = "Tanggal : "+ DateTime.Now.ToString().Substring(0,10);
            genButton();
        }

        void loadData()
        {
            string query = "select distinct hj.ID as \"No\", hj.KODE_HJUAL as \"Kode HJUAL\", to_char(hj.TANGGAL, 'dd/MM/yyyy') as \"Tanggal\", mn.NAMA as \"Menu\", dj.JUMLAH as \"Jumlah\", dj.HARGA as \"Harga\", dj.JUMLAH * dj.HARGA as \"Subtotal\" from HJUAL hj, DJUAL dj, MENU mn where hj.ID = dj.ID_HEADER and mn.ID = dj.ID_MENU and hj.status = 0 and hj.nomor_meja = " + nomeja;
            OracleDataAdapter da = new OracleDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);

            dgKasir.ItemsSource = dt.DefaultView;
        }

        void genButton()
        {
            for (int i = 1; i <= App.getJumlahMeja(); i++)
            {
                Button btn = new Button();
                btn.Height = 60;
                btn.Width = 60;
                btn.Content = i;
                btn.Margin = new Thickness(0, 0, 0, 20);
                btn.FontSize = 18;
                btn.Click += (sender, e) => {
                    nomeja = Convert.ToInt32(btn.Content);
                    mejudul.Content = "MEJA " + nomeja.ToString();
                    loadData();
                };

                if(i % 2 == 0)
                {
                    stackpanel2.Children.Add(btn);
                }
                else
                {
                    stackpanel.Children.Add(btn);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA";
            MainWindow mainwin = new MainWindow();
            mainwin.Show();
            this.Close();
        }

        private void dgKasir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowNota sn = new ShowNota();
                sn.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
