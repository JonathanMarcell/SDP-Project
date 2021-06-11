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
using Oracle.DataAccess.Client;
using System.Data;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for ListPesanan.xaml
    /// </summary>
    public partial class ListPesanan : Window
    {
        DataTable dtPesanan, tempData;
        OracleDataAdapter daPesanan;
        OracleConnection conn;
        //int savedId = -1;
        
        public ListPesanan()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
            conn = App.conn;

            dtPesanan = new DataTable();
            dtPesanan.Columns.Add("Nama Pesanan");
            dtPesanan.Columns.Add("Nomor Meja");
            dtPesanan.Columns.Add("Jumlah");
            dtPesanan.Columns.Add("Keterangan");
            dtPesanan.Columns.Add("Status");
            loadData();

        }

        private void dgPesanan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        public void hapusData()
        {
            //tempData.Rows[dgPesanan.SelectedIndex].Delete();
            //dtPesanan.Rows[dgPesanan.SelectedIndex].Delete();
           
        }

        private void btnSelesai_Click(object sender, RoutedEventArgs e)
        {
            if (dgPesanan.SelectedIndex < 0)
            {

            }
            else
            {
                try
                {
                    int index = Int32.Parse(tempData.Rows[dgPesanan.SelectedIndex][0].ToString());

                    string qry = $"update djual set status=2 where id={index}";
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    OracleCommand cmd = new OracleCommand(qry, conn);
                    cmd.ExecuteNonQuery();
                    hapusData();
                    loadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            loadData();
            //int tempInd = 0;
            //if (savedId != -1)
            //{
            //    foreach (DataRow dr in tempData.Rows)
            //    {
            //        if (dr[0].ToString() == savedId.ToString())
            //        {
            //            dgPesanan.SelectedIndex = tempInd;
            //        }
            //        tempInd++;
            //    }
            //}
            
        }

        private void dgPesanan_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //savedId = Int32.Parse(tempData.Rows[dgPesanan.SelectedIndex][0].ToString());
        }

        public void loadData()
        {
            try
            {
                dgPesanan.Columns.Clear();

                dtPesanan = new DataTable();
                dtPesanan.Columns.Add("Nama Pesanan");
                dtPesanan.Columns.Add("Nomor Meja");
                dtPesanan.Columns.Add("Jumlah");
                dtPesanan.Columns.Add("Keterangan");
                dtPesanan.Columns.Add("Status");

                tempData = new DataTable();
                daPesanan = new OracleDataAdapter("select d.id, m.nama, h.nomor_meja, d.jumlah, d.keterangan, d.status from menu m, djual d, hjual h where d.id_header=h.id and d.id_menu=m.id and h.status=2 and d.status=1 order by d.status, h.nomor_meja", conn);
                daPesanan.Fill(tempData);
                foreach (DataRow d in tempData.Rows) 
                {
                    string ket = "";
                 
                    if (d["keterangan"].ToString() == "")
                    {
                        ket = "-";
                    }
                    else
                    {
                        ket = d["keterangan"].ToString();
                    }
                    dtPesanan.Rows.Add(d["nama"],"Meja "+d["nomor_meja"],d["jumlah"],ket,"In Process");
                }
                dgPesanan.ItemsSource = dtPesanan.DefaultView;
                //for (int i = 0; i < dgPesanan.Items.Count; i++)
                //{
                //    if (dgPesanan.Columns[4].GetCellContent(i).ToString() == "DONE")
                //    {

                //    }
                //    else
                //    {

                //    }
                //}
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        
    }
}
