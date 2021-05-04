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
        public tunggu(string n)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
            lbNama.Content = "Meja " + n;
        }
        int time,timer;
        DispatcherTimer dt = new DispatcherTimer();
        private void btnSelesai_Click(object sender, RoutedEventArgs e)
        {
            meunggu.Content =  "Terima kasih";
            bayar.Visibility = Visibility.Hidden;
        }

        private void dtTicker(object sender, EventArgs e)
        {
            time++;
            if (time>6)
            {
                meunggu.Content = "Terima kasih";
                bayar.Visibility = Visibility.Hidden;
            }
            if (time>10)
            {
                string temp = lbNama.Content.ToString().Substring(5,1);
                dt.Stop();
                Meja a = new Meja(temp);
                a.Show();
                Close();
            }
            if (timer>3)
            {

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
