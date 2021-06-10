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
using System.Windows.Threading;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for tunggu.xaml
    /// </summary>
    public partial class tunggu : Window
    {
        OracleConnection conn;
        public tunggu(string n)
        {
            InitializeComponent();
            //this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            lbNama.Content = "Meja " + n;
            pesanL = false;
            temp1 = n;
            conn = App.conn;
            try
            {
                //nambah gambar di Background (pake brush) / Image (pake ImageSource)
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
                btnSelesai.Background = b; 
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        int time,timer;
        DispatcherTimer dt = new DispatcherTimer();
        string temp1;
        List<status> s = new List<status>();
        bool pesanL = false;
        void selesai()
        {
            OracleCommand cmd = new OracleCommand();
            string qry = "select status as \"status\" from djual where nomor_meja = "+temp1 ;
            cmd = new OracleCommand(qry, conn);
            cmd.ExecuteReader();
            OracleDataReader dr = cmd.ExecuteReader();
            s.Clear();
            while (dr.Read())
            {
                s.Add(new status(Convert.ToInt32(dr[0])));
            }
            int max = s.Count();
            int ctr = 0;
            foreach (var item in s)
            {
                if (item.getStatusM() == 1)
                {
                    ctr++;
                }
            }
            if (ctr==max)
            {
                string temp = lbNama.Content.ToString().Substring(5, 1);
                dt.Stop();
                Meja a = new Meja(temp,pesanL);
                a.Show();
                Close();
            }
        }
        private void btnSelesai_Click(object sender, RoutedEventArgs e)
        {
            pesanL = true;
            meunggu.Content =  "Terima kasih";
            bayar.Visibility = Visibility.Hidden;
            timer = 0;
        }
        private void dtTicker(object sender, EventArgs e)
        {
            timer++;
            selesai();
            if (pesanL)
            {
                if (timer>=3)
                {
                    string temp = lbNama.Content.ToString().Substring(5, 1);
                    dt.Stop();
                    Meja a = new Meja(temp,pesanL);
                    a.Show();
                    Close();
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            time = 0;
            timer = 0;
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();
        }
    }
}
