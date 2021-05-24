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
            dtPesanan.Columns.Add("Status");
            dtPesanan.Columns.Add("Action");
            loadData();

        }

        private void dgPesanan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int index = Int32.Parse(tempData.Rows[dgPesanan.SelectedIndex][0].ToString());
                string qry = "";
                if (tempData.Rows[dgPesanan.SelectedIndex][5].ToString() == "0") {
                    qry = $"update djual set status=1 where id={index}";
                }
                else
                {
                    qry = $"update djual set status=0 where id={index}";
                }
         
                conn.Open();
                OracleCommand cmd = new OracleCommand(qry, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                hapusData();
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void hapusData()
        {
            //tempData.Rows[dgPesanan.SelectedIndex].Delete();
            //dtPesanan.Rows[dgPesanan.SelectedIndex].Delete();
           
        }
        public void loadData()
        {
            try
            {
                dgPesanan.Columns.Clear();
                conn.Open();

                dtPesanan = new DataTable();
                dtPesanan.Columns.Add("Nama Pesanan");
                dtPesanan.Columns.Add("Nomor Meja");
                dtPesanan.Columns.Add("Jumlah");
                dtPesanan.Columns.Add("Keterangan");
                dtPesanan.Columns.Add("Status");
                dtPesanan.Columns.Add("Action");

                tempData = new DataTable();
                daPesanan = new OracleDataAdapter("select d.id, m.nama, h.nomor_meja, d.jumlah, d.keterangan, d.status from menu m, djual d, hjual h where d.id_header=h.id and d.id_menu=m.id and h.status=0 order by d.status, h.nomor_meja", conn);
                daPesanan.Fill(tempData);
                foreach (DataRow d in tempData.Rows) 
                {
                    string tempString = "";
                    string ket = "";
                    if (d["status"].ToString() == "0")
                    {
                        tempString = "IN PROCESS";
                    }
                    else
                    {
                        tempString = "DONE";
                    }
                    if (d["keterangan"].ToString() == "")
                    {
                        ket = "-";
                    }
                    else
                    {
                        ket = d["keterangan"].ToString();
                    }
                    dtPesanan.Rows.Add(d["nama"],"Meja "+d["nomor_meja"],d["jumlah"],ket,tempString);
                }
                dgPesanan.ItemsSource = dtPesanan.DefaultView;
                conn.Close();
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
