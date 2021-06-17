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
        string kodekasir;
        string namakasir;
        public Kasir(string kode)
        {
            InitializeComponent();
            App.openconn();
            this.conn = App.conn;
            kodekasir = kode;
            namakasir = getNamaPegawai(kode);
            lb_username.Content += namakasir;
            try
            {
                //nambah gambar di Background (pake brush) / Image (pake ImageSource)
                Uri uri = new Uri("Resource/background.png", UriKind.Relative);
                BitmapImage img = new BitmapImage(uri);
                ImageBrush b = new ImageBrush(img);
                //mode 
                b.Opacity = 1; //default 1
                b.TileMode = TileMode.Tile; //default None
                b.Stretch = Stretch.Fill; //default Fill

                win.Background = b; // win ini x:Name nya Window
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            labelTanggal.Content = "Tanggal : "+ DateTime.Now.ToShortDateString();
            genButton();
        }

        void loadData()
        {
            string query = $"select distinct hj.KODE_HJUAL as \"Kode HJUAL\", to_char(hj.TANGGAL, 'dd/MM/yyyy') as \"Tanggal\", mn.NAMA as \"Menu\", dj.JUMLAH as \"Jumlah\", dj.HARGA as \"Harga\", dj.JUMLAH * dj.HARGA as \"Subtotal\" from HJUAL hj, DJUAL dj, MENU mn where hj.ID = dj.ID_HEADER and mn.ID = dj.ID_MENU and hj.status = 0 and hj.nomor_meja = {nomeja} order by \"Kode HJUAL\" ";
            OracleDataAdapter da = new OracleDataAdapter(query, conn);
            dt = new DataTable();
            dt.Columns.Add("No");
            da.Fill(dt);

            dgKasir.ItemsSource = dt.DefaultView;
        }

        void genNo()
        {
            int harga = 0;
            double hrgpajak = 0;
            if(dt.Rows.Count < 1)
            {
                pajak.Content = "-";
                totHarga.Content = "-";
            }
            else
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dt.Rows[j]["No"] = j + 1;
                    hrgpajak += (Convert.ToInt32(dt.Rows[j]["Subtotal"]) * 0.1);
                    harga += Convert.ToInt32(dt.Rows[j]["Subtotal"]);
                }
                pajak.Content = hrgpajak.ToString("C0");
                totHarga.Content = (harga + (int)hrgpajak).ToString("C0");
            }
        }

        void genButton()
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
            for (int i = 1; i <= App.getJumlahMeja(); i++)
            {
                Button btn = new Button();
                btn.Height = 60;
                btn.Width = 60;
                btn.Content = i;
                btn.Margin = new Thickness(0, 0, 0, 20);
                btn.FontSize = 18;
                btn.Foreground = new System.Windows.Media.SolidColorBrush(color);
                Uri uri = new Uri("Resource/button square-bold.png", UriKind.Relative);
                BitmapImage img = new BitmapImage(uri);
                ImageBrush b = new ImageBrush(img);
                b.Opacity = 1; //default 1
                b.TileMode = TileMode.Tile; //default None
                b.Stretch = Stretch.Fill; //default Fill
                btn.Background = b;
                btn.FontWeight = FontWeights.Bold;
                btn.Click += (sender, e) => {
                    int harga = 0;
                    double hrgpajak = 0;
                    nomeja = Convert.ToInt32(btn.Content);
                    mejudul.Content = "MEJA " + nomeja.ToString();
                    loadData();
                    if (dt.Rows.Count < 1)
                    {
                        pajak.Content = "-";
                        totHarga.Content = "-";
                    }
                    else
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            dt.Rows[j]["No"] = j + 1;
                            hrgpajak += (Convert.ToInt32(dt.Rows[j]["Subtotal"]) * 0.1);
                            harga += Convert.ToInt32(dt.Rows[j]["Subtotal"]);
                        }
                        pajak.Content = hrgpajak.ToString("C0");
                        totHarga.Content = (harga + (int)hrgpajak).ToString("C0");
                    }
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
            this.Close();
        }

        private void dgKasir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

            if ( dt == null  )
            {
                MessageBox.Show("Pilih Meja Terlebih dahulu");
            }
            else if (dt.Rows.Count == 0)
            {

                MessageBox.Show("Tidak Ada Pesanan Pada Meja " + nomeja);
            }
            else
            {
                int updatedstathj = 2, updatedstatdj = 1;

                string qryidhj = "select id from hjual where NOMOR_MEJA= " + nomeja + " and status = 0";
                OracleCommand cmd3 = new OracleCommand(qryidhj, conn);
                int idhj = Convert.ToInt32(cmd3.ExecuteScalar().ToString());

                string confirm = $"UPDATE HJUAL set STATUS = :stat , ID_PEGAWAI = :idpeg WHERE id ={idhj}";
                OracleCommand cmd = new OracleCommand(confirm, conn);
                try
                {
                    cmd.Parameters.Add(":stat", updatedstathj);
                    cmd.Parameters.Add(":idpeg", getIDPegawai(kodekasir));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Konfirmasi Berhasil");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                string djualstat = "UPDATE DJUAL set STATUS = :stat WHERE ID_HEADER = :idHJ";
                OracleCommand cmd2 = new OracleCommand(djualstat, conn);
                try
                {
                    cmd2.Parameters.Add(":stat", updatedstatdj);
                    cmd2.Parameters.Add(":idHJ", idhj);

                    cmd2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    ShowNota sn = new ShowNota(idhj,getIDPegawai(kodekasir));
                    sn.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                loadData();
                genNo();
            }
        }

        private void dgKasir_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgKasir.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgKasir.Columns[1].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            dgKasir.Columns[2].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            dgKasir.Columns[3].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            dgKasir.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgKasir.Columns[5].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            dgKasir.Columns[6].Width = new DataGridLength(2, DataGridLengthUnitType.Star);

            dgKasir.Columns[5].ClipboardContentBinding.StringFormat = "C0";
            dgKasir.Columns[6].ClipboardContentBinding.StringFormat = "C0";
        }

        private void dgKasir_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int idx = dgKasir.SelectedIndex;
            if (idx < 0)
            {
                return;
            }
            string idhjual = dt.Rows[idx]["Kode HJUAL"].ToString();
            string menu = dt.Rows[idx]["Menu"].ToString();
            int jumlah = Convert.ToInt32(dt.Rows[idx]["Jumlah"].ToString());
            string qryid = "select id from menu where nama='"+menu+"'";
            OracleCommand cmd = new OracleCommand(qryid, App.conn);
            int idmenu = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            KasirEdit ke = new KasirEdit(nomeja, idhjual, menu, jumlah, idmenu);
            ke.ShowDialog();
            loadData();
            genNo();
        }

        private string getNamaPegawai(string kode)
        {
            OracleCommand cmd = new OracleCommand($"select nama from pegawai where kode_pegawai='{kode}'",conn);
            return cmd.ExecuteScalar().ToString();
        }
        private string getIDPegawai(string kode)
        {
            OracleCommand cmd = new OracleCommand($"select id from pegawai where kode_pegawai='{kode}'", conn);
            return cmd.ExecuteScalar().ToString();
        }
    }
}
