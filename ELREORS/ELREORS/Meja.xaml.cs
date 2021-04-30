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
    /// Interaction logic for Meja.xaml
    /// </summary>
    public partial class Meja : Window
    {
        OracleConnection conn;
        List<menu> m = new List<menu>();
        int max = 0;
        int halaman = 0;
        int jumlah = 3;
        DataTable daO;
        int indexing = 0;
        public Meja()
        {
            InitializeComponent();
            btnPrev.IsEnabled = false;
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            conn = App.conn;
            refresh();
            tampil();
            awal();
        }
        void awal()
        {
            daO = new DataTable();
            daO.Columns.Add("Nama");
            daO.Columns.Add("Jumlah");
            daO.Columns.Add("Harga");
            daO.Columns.Add("Keterangan");
        }
        void refresh()
        {
            try
            {
                float hasil = 0; ;
                conn.Open();
                max = 0;
                OracleCommand cmd = new OracleCommand();
                string qry = "select id as \"id\",nama as \"nama\", harga as \"harga\" , status as \"status\", keterangan as \"keterangan\" from menu where status = 1";
                cmd = new OracleCommand(qry, conn);
                cmd.ExecuteReader();
                OracleDataReader dr = cmd.ExecuteReader();
                m.Clear();
                while (dr.Read())
                {
                    m.Add(new menu(Convert.ToInt32(dr[0]), dr[1].ToString(), Convert.ToInt32(dr[2]), Convert.ToInt32(dr[3]), dr[4].ToString()));
                }
                for (int i = 0; i < m.Count(); i++)
                {
                    max++;
                    jumlah++;
                }
                hasil = max % 3;
                if (hasil != 0) max = (max / 3) + 1;
                else max = max / 3;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
        void tampil()
        {
            int i1 = 0, i2 = 1, i3 = 2, iT = 3;
            try
            {
                if (halaman == 0)
                {
                    if ((iT * halaman) + i1 >= 0 && (iT * halaman) + i1 < m.Count())
                    {
                        lbN1.Content = m[i1].getNama();
                        lbK1.Content = m[i1].getKeterangan();
                        lbH1.Content = m[i1].getHarga();
                    }
                    else
                    {
                        img1.Visibility = Visibility.Hidden;
                        lbN1.Visibility = Visibility.Hidden;
                        lbK1.Visibility = Visibility.Hidden;
                        lbH1.Visibility = Visibility.Hidden;
                        lbJ1.Visibility = Visibility.Hidden;
                        btnMin1.Visibility = Visibility.Hidden;
                        btnPlus1.Visibility = Visibility.Hidden;
                        Rp1.Visibility = Visibility.Hidden;
                    }

                    if ((iT * halaman) + i2 >= 0 && (iT * halaman) + i2 < m.Count())
                    {
                        lbN2.Content = m[i2].getNama();
                        lbK2.Content = m[i2].getKeterangan();
                        lbH2.Content = m[i2].getHarga();
                    }
                    else
                    {
                        img2.Visibility = Visibility.Hidden;
                        lbN2.Visibility = Visibility.Hidden;
                        lbK2.Visibility = Visibility.Hidden;
                        lbH2.Visibility = Visibility.Hidden;
                        lbJ2.Visibility = Visibility.Hidden;
                        btnMin2.Visibility = Visibility.Hidden;
                        btnPlus2.Visibility = Visibility.Hidden;
                        Rp2.Visibility = Visibility.Hidden;
                    }

                    if ((iT * halaman) + i3 >= 0 && (iT * halaman) + i3 < m.Count())
                    {
                        lbN3.Content = m[i3].getNama();
                        lbK3.Content = m[i3].getKeterangan();
                        lbH3.Content = m[i3].getHarga();
                    }
                    else
                    {
                        img3.Visibility = Visibility.Hidden;
                        lbN3.Visibility = Visibility.Hidden;
                        lbK3.Visibility = Visibility.Hidden;
                        lbH3.Visibility = Visibility.Hidden;
                        lbJ3.Visibility = Visibility.Hidden;
                        btnMin3.Visibility = Visibility.Hidden;
                        btnPlus3.Visibility = Visibility.Hidden;
                        Rp3.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    if ((iT * halaman) + i1 >= 0 && (iT * halaman) + i1 < m.Count())
                    {
                        lbN1.Content = m[(iT * halaman) + i1].getNama();
                        lbK1.Content = m[(iT * halaman) + i1].getKeterangan();
                        lbH1.Content = m[(iT * halaman) + i1].getHarga();
                    }
                    else
                    {
                        img1.Visibility = Visibility.Hidden;
                        lbN1.Visibility = Visibility.Hidden;
                        lbK1.Visibility = Visibility.Hidden;
                        lbH1.Visibility = Visibility.Hidden;
                        lbJ1.Visibility = Visibility.Hidden;
                        btnMin1.Visibility = Visibility.Hidden;
                        btnPlus1.Visibility = Visibility.Hidden;
                        Rp1.Visibility = Visibility.Hidden;
                    }

                    if ((iT * halaman) + i2 >= 0 && (iT * halaman) + i2 < m.Count())
                    {
                        lbN2.Content = m[(iT * halaman) + i2].getNama();
                        lbK2.Content = m[(iT * halaman) + i2].getKeterangan();
                        lbH2.Content = m[(iT * halaman) + i2].getHarga();
                    }
                    else
                    {
                        img2.Visibility = Visibility.Hidden;
                        lbN2.Visibility = Visibility.Hidden;
                        lbK2.Visibility = Visibility.Hidden;
                        lbH2.Visibility = Visibility.Hidden;
                        lbJ2.Visibility = Visibility.Hidden;
                        btnMin2.Visibility = Visibility.Hidden;
                        btnPlus2.Visibility = Visibility.Hidden;
                        Rp2.Visibility = Visibility.Hidden;
                    }

                    if ((iT * halaman) + i3 >= 0 && (iT * halaman) + i3 < m.Count())
                    {
                        lbN3.Content = m[(iT * halaman) + i3].getNama();
                        lbK3.Content = m[(iT * halaman) + i3].getKeterangan();
                        lbH3.Content = m[(iT * halaman) + i3].getHarga();
                    }
                    else
                    {
                        img3.Visibility = Visibility.Hidden;
                        lbN3.Visibility = Visibility.Hidden;
                        lbK3.Visibility = Visibility.Hidden;
                        lbH3.Visibility = Visibility.Hidden;
                        lbJ3.Visibility = Visibility.Hidden;
                        btnMin3.Visibility = Visibility.Hidden;
                        btnPlus3.Visibility = Visibility.Hidden;
                        Rp3.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void tampilLb()
        {
            img1.Visibility = Visibility.Visible;
            lbN1.Visibility = Visibility.Visible;
            lbK1.Visibility = Visibility.Visible;
            lbH1.Visibility = Visibility.Visible;
            lbJ1.Visibility = Visibility.Visible;
            btnMin1.Visibility = Visibility.Visible;
            btnPlus1.Visibility = Visibility.Visible;
            Rp1.Visibility = Visibility.Visible;

            img2.Visibility = Visibility.Visible;
            lbN2.Visibility = Visibility.Visible;
            lbK2.Visibility = Visibility.Visible;
            lbH2.Visibility = Visibility.Visible;
            lbJ2.Visibility = Visibility.Visible;
            btnMin2.Visibility = Visibility.Visible;
            btnPlus2.Visibility = Visibility.Visible;
            Rp2.Visibility = Visibility.Visible;

            img3.Visibility = Visibility.Visible;
            lbN3.Visibility = Visibility.Visible;
            lbK3.Visibility = Visibility.Visible;
            lbH3.Visibility = Visibility.Visible;
            lbJ3.Visibility = Visibility.Visible;
            btnMin3.Visibility = Visibility.Visible;
            btnPlus3.Visibility = Visibility.Visible;
            Rp3.Visibility = Visibility.Visible;
        }
        void gantiJ()
        {
            lbJ1.Content = "0";
            lbJ2.Content = "0";
            lbJ3.Content = "0";
        }
        void cekJ()
        {
            for (int i = 0; i < daO.Rows.Count; i++)
            {
                if (daO.Rows[i]["Nama"] == lbN1.Content)
                {
                    lbJ1.Content = daO.Rows[i]["Jumlah"];
                }
                else if (daO.Rows[i]["Nama"] == lbN2.Content)
                {
                    lbJ2.Content = daO.Rows[i]["Jumlah"];
                }
                else if (daO.Rows[i]["Nama"] == lbN3.Content)
                {
                    lbJ3.Content = daO.Rows[i]["Jumlah"];
                }
            }
        }
        void tambah()
        {
            if (indexing == 1)
            {
                bool gak = true;
                int ctr = 0;
                if (daO.Rows.Count > 0)
                {
                    for (int i = 0; i < daO.Rows.Count; i++)
                    {
                        if (daO.Rows[i]["Nama"] == lbN1.Content)
                        {
                            daO.Rows[i]["Jumlah"] = Convert.ToInt32(daO.Rows[i]["Jumlah"]) + 1;
                            lbJ1.Content = Convert.ToInt32(lbJ1.Content) + 1;
                            daO.Rows[i]["Harga"] = Convert.ToInt32(lbJ1.Content) * Convert.ToInt32(lbH1.Content);
                            ctr++;
                        }
                    }
                    if (ctr == 0) gak = false;
                }
                else gak = false;
                if (!gak)
                {
                    lbJ1.Content = Convert.ToInt32(lbJ1.Content) + 1;
                    DataRow dr = daO.NewRow();
                    dr[0] = lbN1.Content;
                    dr[1] = lbJ1.Content;
                    dr[2] = Convert.ToInt32(lbH1.Content) * Convert.ToInt32(lbJ1.Content);
                    daO.Rows.Add(dr);
                }
                dataOrder.ItemsSource = daO.DefaultView;
            }
            else if (indexing == 2)
            {
                bool gak = true;
                int ctr = 0;
                if (daO.Rows.Count > 0)
                {
                    for (int i = 0; i < daO.Rows.Count; i++)
                    {
                        if (daO.Rows[i]["Nama"] == lbN2.Content)
                        {
                            daO.Rows[i]["Jumlah"] = Convert.ToInt32(daO.Rows[i]["Jumlah"]) + 1;
                            lbJ2.Content = Convert.ToInt32(lbJ2.Content) + 1;
                            daO.Rows[i]["Harga"] = Convert.ToInt32(lbJ2.Content) * Convert.ToInt32(lbH2.Content);
                            ctr++;
                        }

                    }
                    if (ctr == 0) gak = false;
                }
                else gak = false;
                if (!gak)
                {
                    lbJ2.Content = Convert.ToInt32(lbJ2.Content) + 1;
                    DataRow dr = daO.NewRow();
                    dr[0] = lbN2.Content;
                    dr[1] = lbJ2.Content;
                    dr[2] = Convert.ToInt32(lbH2.Content) * Convert.ToInt32(lbJ2.Content);
                    daO.Rows.Add(dr);
                }
                dataOrder.ItemsSource = daO.DefaultView;
            }
            else
            {
                bool gak = true;
                int ctr = 0;
                if (daO.Rows.Count > 0)
                {
                    for (int i = 0; i < daO.Rows.Count; i++)
                    {
                        if (daO.Rows[i]["Nama"] == lbN3.Content)
                        {
                            daO.Rows[i]["Jumlah"] = Convert.ToInt32(daO.Rows[i]["Jumlah"]) + 1;
                            lbJ3.Content = Convert.ToInt32(lbJ3.Content) + 1;
                            daO.Rows[i]["Harga"] = Convert.ToInt32(lbJ3.Content) * Convert.ToInt32(lbH3.Content);
                            ctr++;
                        }
                    }
                    if (ctr == 0) gak = false;
                }
                else gak = false;
                if (!gak)
                {
                    lbJ3.Content = Convert.ToInt32(lbJ3.Content) + 1;
                    DataRow dr = daO.NewRow();
                    dr[0] = lbN3.Content;
                    dr[1] = lbJ3.Content;
                    dr[2] = Convert.ToInt32(lbH3.Content) * Convert.ToInt32(lbJ3.Content);
                    daO.Rows.Add(dr);
                }
                dataOrder.ItemsSource = daO.DefaultView;
            }
            int harga = 0;
            for (int i = 0; i < daO.Rows.Count; i++)
            {
                harga = Convert.ToInt32(daO.Rows[i]["Harga"]) + harga;
                lbTH.Content = harga + "";
            }

        }
        void kurang()
        {
            if (indexing == 1)
            {
                if (Convert.ToInt32(lbJ1.Content) > 0)
                {
                    if (daO.Rows.Count > 0)
                    {
                        for (int i = 0; i < daO.Rows.Count; i++)
                        {
                            if (daO.Rows[i]["Nama"] == lbN1.Content)
                            {
                                daO.Rows[i]["Jumlah"] = Convert.ToInt32(daO.Rows[i]["Jumlah"]) - 1;
                                lbJ1.Content = Convert.ToInt32(lbJ1.Content) - 1;
                                daO.Rows[i]["Harga"] = Convert.ToInt32(lbJ1.Content) * Convert.ToInt32(lbH1.Content);
                            }
                            if (Convert.ToInt32(daO.Rows[i]["Jumlah"]) == 0)
                            {
                                daO.Rows.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            else if (indexing == 2)
            {
                if (Convert.ToInt32(lbJ2.Content) > 0)
                {
                    if (daO.Rows.Count > 0)
                    {
                        for (int i = 0; i < daO.Rows.Count; i++)
                        {
                            if (daO.Rows[i]["Nama"] == lbN2.Content)
                            {
                                daO.Rows[i]["Jumlah"] = Convert.ToInt32(daO.Rows[i]["Jumlah"]) - 1;
                                lbJ2.Content = Convert.ToInt32(lbJ2.Content) - 1;
                                daO.Rows[i]["Harga"] = Convert.ToInt32(lbJ2.Content) * Convert.ToInt32(lbH2.Content);
                            }
                            if (Convert.ToInt32(daO.Rows[i]["Jumlah"]) == 0)
                            {
                                daO.Rows.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(lbJ3.Content) > 0)
                {
                    if (daO.Rows.Count > 0)
                    {
                        for (int i = 0; i < daO.Rows.Count; i++)
                        {
                            if (daO.Rows[i]["Nama"] == lbN3.Content)
                            {
                                daO.Rows[i]["Jumlah"] = Convert.ToInt32(daO.Rows[i]["Jumlah"]) - 1;
                                lbJ3.Content = Convert.ToInt32(lbJ3.Content) - 1;
                                daO.Rows[i]["Harga"] = Convert.ToInt32(lbJ3.Content) * Convert.ToInt32(lbH3.Content);
                            }
                            if (Convert.ToInt32(daO.Rows[i]["Jumlah"]) == 0)
                            {
                                daO.Rows.RemoveAt(i);
                            }
                        }
                    }
                }
            }

            int harga = 0;
            if (daO.Rows.Count == 0)
            {
                harga = 0;
                lbTH.Content = "";
            }
            for (int i = 0; i < daO.Rows.Count; i++)
            {
                if (daO.Rows.Count <= 0)
                {
                    harga = 0;
                    lbTH.Content = "";
                }
                else
                {
                    harga = Convert.ToInt32(daO.Rows[i]["Harga"]) + harga;
                    lbTH.Content = harga + "";
                }
            }
        }
        private void btnPesan_Click(object sender, RoutedEventArgs e)
        {
            if (dataOrder.Items.Count <= 1)
            {
                MessageBox.Show("Silahkan memesan makanan terlebih dahulu");
            }
            else
            {
                conn.Open();
                int ctr = 0;
                OracleTransaction trans;
                trans = conn.BeginTransaction();
                try
                {
                    string qry = "insert into hjual values(null,null,null," + Convert.ToInt32(lbTH.Content) + "," + Convert.ToInt32(lbM.Content) + ",1,0) ";
                    OracleCommand cmd = new OracleCommand(qry, conn);
                    cmd.ExecuteNonQuery();
                    int id = 0;
                    for (int i = 0; i < daO.Rows.Count; i++)
                    {
                        for (int j = 0; j < m.Count; j++)
                        {
                            if (daO.Rows[i]["Nama"] == m[j].getNama())
                            {
                                id = m[j].getId();
                                qry = "insert into djual values(null,null," + id + ",'" + daO.Rows[i]["Keterangan"].ToString() + "')";
                                cmd = new OracleCommand(qry, conn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    trans.Commit();
                    tunggu t = new tunggu();
                    t.Show();
                    this.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    trans.Rollback();
                    conn.Close();
                }

            }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            gantiJ();
            btnPrev.IsEnabled = true;
            if (halaman < max - 1)
            {
                tampilLb();
                halaman = halaman + 1;
                tampil();
                cekJ();
            }
            if (halaman == max - 1)
            {
                btnNext.IsEnabled = false;
            }
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            gantiJ();
            btnNext.IsEnabled = true;
            if (halaman > 0)
            {
                tampilLb();
                halaman = halaman - 1;
                tampil();
                cekJ();
            }
            if (halaman == 0)
            {
                btnPrev.IsEnabled = false;
            }
        }
        private void btnMin1_Click(object sender, RoutedEventArgs e)
        {
            indexing = 1;
            kurang();
        }
        private void btnPlus1_Click(object sender, RoutedEventArgs e)
        {
            indexing = 1;
            tambah();
        }
        private void btnMin2_Click(object sender, RoutedEventArgs e)
        {
            indexing = 2;
            kurang();
        }
        private void btnPlus2_Click(object sender, RoutedEventArgs e)
        {
            indexing = 2;
            tambah();
        }
        private void btnMin3_Click(object sender, RoutedEventArgs e)
        {
            indexing = 3;
            kurang();
        }
        private void btnPlus3_Click(object sender, RoutedEventArgs e)
        {
            indexing = 3;
            tambah();
        }
        private void btnBersih_Click(object sender, RoutedEventArgs e)
        {
            daO.Clear();
            dataOrder.ItemsSource = daO.DefaultView;
            gantiJ();
        }
        private void lbNama_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbM.Visibility = Visibility.Visible;
            tbM.Text = "Silahkan masukan no meja";
        }
        private void lbM_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbM.Visibility = Visibility.Visible;
            tbM.Text = "Silahkan masukan no meja";
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                lbM.Content = tbM.Text;
                tbM.Visibility = Visibility.Hidden;
            }
        }
    }
}
