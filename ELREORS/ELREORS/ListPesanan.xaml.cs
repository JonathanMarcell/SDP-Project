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
        public ListPesanan()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            conn = App.conn;

            dtPesanan = new DataTable();
            dtPesanan.Columns.Add("Nama Pesanan");
            dtPesanan.Columns.Add("Nomor Meja");
            dtPesanan.Columns.Add("Jumlah");
            dtPesanan.Columns.Add("Keterangan");
            dtPesanan.Columns.Add("Action");
            loadData();

        }

        private void dgPesanan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = Int32.Parse(tempData.Rows[dgPesanan.SelectedIndex][0].ToString());
                
                string qry = $"update djual set status=1 where id={index}";
                conn.Open();
                OracleCommand cmd = new OracleCommand(qry, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                hapusData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void hapusData()
        {
            tempData.Rows[dgPesanan.SelectedIndex].Delete();
            dtPesanan.Rows[dgPesanan.SelectedIndex].Delete();
           
        }
        public void loadData()
        {
            try
            {
                conn.Open();
                tempData = new DataTable();
                daPesanan = new OracleDataAdapter("select d.id, m.nama, h.nomor_meja, d.jumlah, d.keterangan from menu m, djual d, hjual h where d.id_header=h.id and d.id_menu=m.id and h.status=0 and d.status=0", conn);
                daPesanan.Fill(tempData);
                foreach (DataRow d in tempData.Rows) 
                {
                    dtPesanan.Rows.Add(d["nama"],"Meja "+d["nomor_meja"],d["jumlah"],d["keterangan"]);
                }
                dgPesanan.ItemsSource = dtPesanan.DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        
    }
}
