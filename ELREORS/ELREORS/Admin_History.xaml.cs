﻿using System;
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
    /// Interaction logic for Admin_History.xaml
    /// </summary>
    public partial class Admin_History : Page
    {
        OracleConnection conn;
        DataTable dt;
        public Admin_History()
        {
            InitializeComponent();
            conn = App.conn;
            dp_filter1.SelectedDate = DateTime.ParseExact("2019-01-01 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            dp_filter2.SelectedDate = DateTime.Now;
            loaddata();
        }

        public void loaddata()
        {
            if (dp_filter1.SelectedDate == null || dp_filter2.SelectedDate == null)
            {
                OracleDataAdapter da = new OracleDataAdapter(
                    "select h.ID , h.KODE_HJUAL as \"NO NOTA\", h.TANGGAL , h.NOMOR_MEJA as \"NOMOR MEJA\", p.NAMA as PEGAWAI , h.TOTAL_HARGA as TOTAL " +
                    "from hjual h " +
                    "join pegawai p on p.id = h.id_pegawai " +
                    "where h.status=1",conn);
                /*
                OracleDataAdapter da = new OracleDataAdapter(
                    "select h.ID as ID, m.nama as NAMA, count(h.total_harga) as Jumlah ,sum(h.total_harga) as Total " +
                    "from hjual h " +
                    "join djual d on d.ID_header = h.ID " +
                    "join menu m on d.id_menu = m.id " +
                    "group by m.nama, h.id", conn);
                */
                dt = new DataTable();
                da.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
            }
            else if(dp_filter1.SelectedDate != null && dp_filter2.SelectedDate != null)
            {
                string tgl1 = dp_filter1.SelectedDate.ToString().Split(' ')[0];
                string tgl2 = dp_filter2.SelectedDate.ToString().Split(' ')[0];
                OracleDataAdapter da = new OracleDataAdapter(
                    "select h.ID , h.KODE_HJUAL as \"NO NOTA\", h.TANGGAL , h.NOMOR_MEJA as \"NOMOR MEJA\", p.NAMA as PEGAWAI , h.TOTAL_HARGA as TOTAL " +
                    "from hjual h " +
                    "join pegawai p on p.id = h.id_pegawai " +
                    "where h.status=1 and " +
                    $"h.tanggal <= to_date('{tgl2}','mm/dd/yyyy') and h.tanggal >=to_date('{tgl1}','mm/dd/yyyy') " , conn);
                /*
                OracleDataAdapter da = new OracleDataAdapter("select h.ID as ID, m.nama as NAMA, count(h.total_harga) as Jumlah ,sum(h.total_harga) as Total " +
                            "from hjual h " +
                            "join djual d on d.ID_header = h.ID " +
                            "join menu m on d.id_menu = m.id " +
                            $"where h.tanggal <= to_date('{tgl2}','mm/dd/yyyy') and h.tanggal >=to_date('{tgl1}','mm/dd/yyyy') " +
                            "group by m.nama, h.id ", conn);
                */
                dt = new DataTable();
                da.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
            }

            int total=0;
            foreach (DataRow item in dt.Rows)
            {
                total += Convert.ToInt32(item[5]);
            }
            textTotal.Text = "Total : Rp."+total;
            textJumlahNota.Text = "Jumlah Nota : "+dt.Rows.Count;
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DateChange(object sender, SelectionChangedEventArgs e)
        {
            loaddata();
        }

        private void btnShowChart_Click(object sender, RoutedEventArgs e)
        {
            Admin_History_Chart w = new Admin_History_Chart();
            w.ShowDialog();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int idx = dataGrid.SelectedIndex;
            if (idx<0)
            {
                return;
            }
            string kodenota = dt.Rows[idx]["NO NOTA"].ToString();
            string id = dt.Rows[idx]["ID"].ToString();
            Admin_History_Details w = new Admin_History_Details(kodenota,id);
            w.ShowDialog();
        }
    }
}
