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
        string nama;
        public tunggu(string na)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;

            nam = na;
            nama = na.Substring(0, 1).ToUpper() + na.Substring(1, 3) + " " + na.Substring(4); ;
            lbNama.Content = nama;
        }
        string nam;
        int time;
        DispatcherTimer dt = new DispatcherTimer();
        private void btnSelesai_Click(object sender, RoutedEventArgs e)
        {
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();
            meunggu.Content =  "Terima kasih";
            bayar.Visibility = Visibility.Hidden;
        }

        private void dtTicker(object sender, EventArgs e)
        {
            time++;
            if (time>3)
            {
                dt.Stop();
                Meja a = new Meja(nam);
                a.Show();
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            time = 0;
        }
    }
}
