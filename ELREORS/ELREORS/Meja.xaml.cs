using Oracle.DataAccess.Client;
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

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Meja.xaml
    /// </summary>
    public partial class Meja : Window
    {
        OracleConnection conn;
        string no;
        string temp;
        List<menu> m = new List<menu>();
        int max = 0;
        int halaman = 0;
        int jumlah = 3;
        public Meja(string n)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            no =  n.Substring(0,1).ToUpper() + n.Substring(1, 3) + " " + n.Substring(4);
            temp = n;
            lbNama.Content = no;
            conn = App.conn;
            refresh();
            tampil();
        }
        void refresh()
        {
            try
            {
                float hasil = 0; ;
                conn.Open();
                max = 0;
                OracleCommand cmd = new OracleCommand();
                string qry = "select nama as \"nama\", harga as \"harga\" , status as \"status\", keterangan as \"keterangan\" from menu where status = 1";
                cmd = new OracleCommand(qry, conn);
                cmd.ExecuteReader();
                OracleDataReader dr = cmd.ExecuteReader();
                m.Clear();
                while (dr.Read())
                {
                    m.Add(new menu(dr[0].ToString(), Convert.ToInt32(dr[1]), Convert.ToInt32(dr[2]), dr[3].ToString()));
                }
                for (int i = 0; i < m.Count(); i++)
                {
                    max++;
                }
                hasil = max % 3;
                if (hasil != 0) max = (max / 3) + 1;
                else max = max / 3;
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
        void tampil()
        {
            int i1=0, i2=1, i3=2, iT = 3;
            try
            {
                if (halaman == 0)
                {
                    lbN1.Content = m[i1].getNama();
                    lbK1.Content = m[i1].getKeterangan();
                    lbH1.Content = m[i1].getHarga();

                    lbN2.Content = m[i2].getNama();
                    lbK2.Content = m[i2].getKeterangan();
                    lbH2.Content = m[i2].getHarga();

                    lbN3.Content = m[i3].getNama();
                    lbK3.Content = m[i3].getKeterangan();
                    lbH3.Content = m[i3].getHarga();
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

                    if ((iT * halaman) + i2 >= 0 && (iT * halaman) + i2 < m.Count() )
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
        private void btnPesan_Click(object sender, RoutedEventArgs e)
        {
            tunggu t = new tunggu(temp);
            t.Show();
            Close();
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (halaman<max-1)
            {
                tampilLb();
                halaman = halaman + 1;
                tampil();
            }
            
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (halaman>0)
            {
                tampilLb();
                halaman = halaman - 1;
                tampil();
            }
        }
    }
}
