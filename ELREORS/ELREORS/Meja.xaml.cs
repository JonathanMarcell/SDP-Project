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
        List<menu> mt = new List<menu>();
        int max = 0;
        int halaman = 0;
        int jumlah = 3;
        DataTable daO;
        int indexing = 0;
        int sekarang = 0;
        public Meja(string n)
        {
            InitializeComponent();
            lbM.Content = n;
            btnPrev.IsEnabled = false;
            btnApp.IsEnabled = false;
            sekarang = 1;
            lbResto.Content = App.getNamaResto();
            //this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            conn = App.conn;
            refresh();
            tampil();
            awal();
            OracleCommand cmd = new OracleCommand();
            string qry = "select nama from kategori order by id";
            cmd = new OracleCommand(qry, conn);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbKategori.Items.Add(dr[0]);
            }
            cbKategori.SelectedIndex = 0;
            countH();
            lbSek.Content = sekarang;
            lbMax.Content = max;
            bg();
        }
        void bg()
        {
            try
            {
                Uri uri = new Uri("Resource/background.png", UriKind.Relative);
                BitmapImage img = new BitmapImage(uri);
                ImageBrush b = new ImageBrush(img);

                b.Opacity = 1;
                b.TileMode = TileMode.Tile;
                b.Stretch = Stretch.Fill;
                win.Background = b;

                uri = new Uri("Resource/button square wide.png", UriKind.Relative);
                img = new BitmapImage(uri);
                b = new ImageBrush(img);

                b.Opacity = 1;
                b.TileMode = TileMode.Tile;
                b.Stretch = Stretch.Fill;
                btnBersih.Background = b;
                btnPesan.Background = b;
                cbKategori.Background = b;

                uri = new Uri("Resource/button square.png", UriKind.Relative);
                img = new BitmapImage(uri);
                b = new ImageBrush(img);

                b.Opacity = 1;
                b.TileMode = TileMode.Tile;
                b.Stretch = Stretch.Fill;
                btnPlus1.Background = b;
                btnPlus2.Background = b;
                btnPlus3.Background = b;
                btnMin1.Background = b;
                btnMin2.Background = b;
                btnMin3.Background = b;

                uri = new Uri("Resource/button square-bold-arrow-left.png", UriKind.Relative);
                img = new BitmapImage(uri);
                b = new ImageBrush(img);

                b.Opacity = 1;
                b.TileMode = TileMode.Tile;
                b.Stretch = Stretch.Fill;
                btnPrev.Background = b;

                uri = new Uri("Resource/button square-bold-arrow-right.png", UriKind.Relative);
                img = new BitmapImage(uri);
                b = new ImageBrush(img);

                b.Opacity = 1;
                b.TileMode = TileMode.Tile;
                b.Stretch = Stretch.Fill;
                btnNext.Background = b;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
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
                float hasil = 0; 
                max = 0;
                OracleCommand cmd = new OracleCommand();
                string qry = "select id as \"id\",nama as \"nama\", harga as \"harga\", id_kategori as \"kategori\",status as \"status\", keterangan as \"keterangan\", gambar as\"gambar\" from menu where status = 1";
                cmd = new OracleCommand(qry, conn);
                cmd.ExecuteReader();
                OracleDataReader dr = cmd.ExecuteReader();
                m.Clear();
                while (dr.Read())
                {
                    if (dr.GetValue(6).ToString() !="")
                    {
                        m.Add(new menu(Convert.ToInt32(dr[0]), dr[1].ToString(), Convert.ToInt32(dr[2]), Convert.ToInt32(dr[3]), Convert.ToInt32(dr[4]), dr[5].ToString(), (Byte[])(dr.GetOracleBlob(6)).Value));
                    }
                    else
                    {
                        m.Add(new menu(Convert.ToInt32(dr[0]), dr[1].ToString(), Convert.ToInt32(dr[2]), Convert.ToInt32(dr[3]), Convert.ToInt32(dr[4]), dr[5].ToString(), null));
                    }
                }
                mt.Clear();
                foreach (var item in m)
                {
                    if (item.getKategori() == 2)
                    {
                        mt.Add(new menu(item.getId(),item.getNama(),item.getHarga(),item.getKategori(),item.getStatus(),item.getKeterangan(), item.getGambar()) );
                    }
                }
                for (int i = 0; i < mt.Count(); i++)
                {
                    max++;
                    jumlah++;
                }
                hasil = max % 3;
                if (hasil != 0) max = (max / 3) + 1;
                else max = max / 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void countH()
        {
            btnNext.IsEnabled = true;
            float hasil = 0;
            halaman = 0;
            max = 0;
            jumlah = 0;
            sekarang = 1;
            for (int i = 0; i < mt.Count(); i++)
            {
                max++;
                jumlah++;
            }
            hasil = max % 3;
            if (hasil != 0) max = (max / 3) + 1;
            else max = max / 3;
            if (sekarang==max)
            {
                btnNext.IsEnabled = false;
                btnPrev.IsEnabled = false;
            }
            lbMax.Content = max;
            lbSek.Content = 1;
        }
        void tampil()
        {
            int i1 = 0, i2 = 1, i3 = 2, iT = 3;
            try
            {
                if (halaman == 0)
                {
                    if ((iT * halaman) + i1 >= 0 && (iT * halaman) + i1 < mt.Count())
                    {
                        if (mt[i1].getGambar() == null || mt[i1].getGambar().Equals("") )
                        {
                            img1.Source = null;
                        }
                        else
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(mt[i1].getGambar()))
                            {
                                var imageSource = new BitmapImage();
                                imageSource.BeginInit();
                                imageSource.StreamSource = ms;
                                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                imageSource.EndInit();

                                img1.Source = imageSource;
                            }
                        }
                        lbN1.Content = mt[i1].getNama();
                        lbK1.Content = mt[i1].getKeterangan();
                        lbH1.Content = mt[i1].getHarga();
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

                    if ((iT * halaman) + i2 >= 0 && (iT * halaman) + i2 < mt.Count())
                    {
                        if (mt[i2].getGambar() == null || mt[i2].getGambar().Equals(""))
                        {
                            img2.Source = null;
                        }
                        else
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(mt[i2].getGambar()))
                            {
                                var imageSource = new BitmapImage();
                                imageSource.BeginInit();
                                imageSource.StreamSource = ms;
                                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                imageSource.EndInit();

                                img2.Source = imageSource;
                            }
                        }
                        lbN2.Content = mt[i2].getNama();
                        lbK2.Content = mt[i2].getKeterangan();
                        lbH2.Content = mt[i2].getHarga();
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

                    if ((iT * halaman) + i3 >= 0 && (iT * halaman) + i3 < mt.Count())
                    {
                        if (mt[i3].getGambar() == null || mt[i3].getGambar().Equals(""))
                        {
                            img3.Source = null;
                        }
                        else
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(mt[i3].getGambar()))
                            {
                                var imageSource = new BitmapImage();
                                imageSource.BeginInit();
                                imageSource.StreamSource = ms;
                                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                imageSource.EndInit();

                                img3.Source = imageSource;
                            }
                        }
                        lbN3.Content = mt[i3].getNama();
                        lbK3.Content = mt[i3].getKeterangan();
                        lbH3.Content = mt[i3].getHarga();
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
                    if ((iT * halaman) + i1 >= 0 && (iT * halaman) + i1 < mt.Count())
                    {
                        if (mt[(iT * halaman) + i1].getGambar() == null || mt[(iT * halaman) + i1].getGambar().Equals(""))
                        {
                            img1.Source = null;
                        }
                        else
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(mt[(iT * halaman) + i1].getGambar()))
                            {
                                var imageSource = new BitmapImage();
                                imageSource.BeginInit();
                                imageSource.StreamSource = ms;
                                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                imageSource.EndInit();

                                img1.Source = imageSource;
                            }
                        }
                        lbN1.Content = mt[(iT * halaman) + i1].getNama();
                        lbK1.Content = mt[(iT * halaman) + i1].getKeterangan();
                        lbH1.Content = mt[(iT * halaman) + i1].getHarga();
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

                    if ((iT * halaman) + i2 >= 0 && (iT * halaman) + i2 < mt.Count())
                    {
                        if (mt[(iT * halaman) + i2].getGambar() == null || mt[(iT * halaman) + i2].getGambar().Equals(""))
                        {
                            img2.Source = null;
                        }
                        else
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(mt[(iT * halaman) + i2].getGambar()))
                            {
                                var imageSource = new BitmapImage();
                                imageSource.BeginInit();
                                imageSource.StreamSource = ms;
                                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                imageSource.EndInit();

                                img2.Source = imageSource;
                            }
                        }
                        lbN2.Content = mt[(iT * halaman) + i2].getNama();
                        lbK2.Content = mt[(iT * halaman) + i2].getKeterangan();
                        lbH2.Content = mt[(iT * halaman) + i2].getHarga();
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

                    if ((iT * halaman) + i3 >= 0 && (iT * halaman) + i3 < mt.Count())
                    {
                        if (mt[(iT * halaman) + i3].getGambar() == null || mt[(iT * halaman) + i3].getGambar().Equals(""))
                        {
                            img3.Source = null;
                        }
                        else
                        {
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(mt[(iT * halaman) + i3].getGambar()))
                            {
                                var imageSource = new BitmapImage();
                                imageSource.BeginInit();
                                imageSource.StreamSource = ms;
                                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                imageSource.EndInit();

                                img3.Source = imageSource;
                            }
                        }
                        lbN3.Content = mt[(iT * halaman) + i3].getNama();
                        lbK3.Content = mt[(iT * halaman) + i3].getKeterangan();
                        lbH3.Content = mt[(iT * halaman) + i3].getHarga();
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
                                qry = "insert into djual values(null,null," + id + "," + Convert.ToInt32(lbM.Content) + ",'" + daO.Rows[i]["Harga"] + "','" + daO.Rows[i]["Jumlah"] + "','" + daO.Rows[i]["Keterangan"].ToString() + "',0)";
                                cmd = new OracleCommand(qry, conn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    trans.Commit();
                    string temp = lbM.Content.ToString();
                    tunggu t = new tunggu(temp);
                    t.Show();
                    this.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    trans.Rollback();
                    
                }

            }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            gantiJ();
            btnPrev.IsEnabled = true;
            if (halaman < max - 1)
            {
                sekarang = sekarang + 1;
                tampilLb();
                halaman = halaman + 1;
                tampil();
                cekJ();
            }
            if (halaman == max - 1)
            {
                btnNext.IsEnabled = false;
            }
            
            lbSek.Content = sekarang;
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            gantiJ();
            btnNext.IsEnabled = true;
            if (halaman > 0)
            {
                tampilLb();
                sekarang = sekarang - 1;
                halaman = halaman - 1;
                tampil();
                cekJ();
            }
            if (halaman == 0)
            {
                btnPrev.IsEnabled = false;
            }
            lbSek.Content = sekarang;
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
        private void btnApp_Click(object sender, RoutedEventArgs e)
        {
            btnApp.IsEnabled = false;
            btnMain.IsEnabled = true;
            btnDes.IsEnabled = true;
            mt.Clear();
            foreach (var item in m)
            {
                if (item.getKategori() == 2)
                {
                    mt.Add(new menu(item.getId(), item.getNama(), item.getHarga(), item.getKategori(), item.getStatus(), item.getKeterangan(), item.getGambar()));
                }
            }
            gantiJ();
            tampilLb();
            tampil();
            cekJ();
        }
        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            btnApp.IsEnabled = true;
            btnMain.IsEnabled = false;
            btnDes.IsEnabled = true;
            mt.Clear();
            foreach (var item in m)
            {
                if (item.getKategori() == 3)
                {
                    mt.Add(new menu(item.getId(), item.getNama(), item.getHarga(), item.getKategori(), item.getStatus(), item.getKeterangan(),item.getGambar()));
                }
            }
            gantiJ();
            tampilLb();
            tampil();
            cekJ();
        }
        private void btnDes_Click(object sender, RoutedEventArgs e)
        {
            btnApp.IsEnabled = true;
            btnMain.IsEnabled = true;
            btnDes.IsEnabled = false;
            mt.Clear();
            foreach (var item in m)
            {
                if (item.getKategori() == 4)
                {
                    mt.Add(new menu(item.getId(), item.getNama(), item.getHarga(), item.getKategori(), item.getStatus(), item.getKeterangan(),item.getGambar()));
                }
            }
            gantiJ();
            tampilLb();
            tampil();
            cekJ();
        }
        private void cbKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbKategori.SelectedIndex==0)
            {
                mt.Clear();
                foreach (var item in m)
                {
                    if (item.getKategori() == 1)
                    {
                        mt.Add(new menu(item.getId(), item.getNama(), item.getHarga(), item.getKategori(), item.getStatus(), item.getKeterangan(), item.getGambar()));
                    }
                }
                countH();
            }
            else if (cbKategori.SelectedIndex == 1)
            {
                mt.Clear();
                foreach (var item in m)
                {
                    if (item.getKategori() == 2)
                    {
                        mt.Add(new menu(item.getId(), item.getNama(), item.getHarga(), item.getKategori(), item.getStatus(), item.getKeterangan(), item.getGambar()));
                    }
                }
                countH();
            }
            else if (cbKategori.SelectedIndex == 2)
            {
                mt.Clear();
                foreach (var item in m)
                {
                    if (item.getKategori() == 3)
                    {
                        mt.Add(new menu(item.getId(), item.getNama(), item.getHarga(), item.getKategori(), item.getStatus(), item.getKeterangan(), item.getGambar()));
                    }
                }
                countH();
            }
            else if (cbKategori.SelectedIndex == 3)
            {
                mt.Clear();
                foreach (var item in m)
                {
                    if (item.getKategori() == 4)
                    {
                        mt.Add(new menu(item.getId(), item.getNama(), item.getHarga(), item.getKategori(), item.getStatus(), item.getKeterangan(), item.getGambar()));
                    }
                }
                countH();
            }
            gantiJ();
            tampilLb();
            tampil();
            cekJ();
        }
    }
}
