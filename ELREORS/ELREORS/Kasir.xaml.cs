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
    /// Interaction logic for Kasir.xaml
    /// </summary>
    public partial class Kasir : Window
    {
        public Kasir()
        {
            InitializeComponent();
            labelTanggal.Content = "Tanggal : "+ DateTime.Now.ToString().Substring(0,10);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 1";
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 2";
        }

        private void btn3_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 3";
        }

        private void btn4_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 4";
        }

        private void btn5_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 5";
        }

        private void btn6_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 6";
        }

        private void btn7_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 7";
        }

        private void btn8_Click_1(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA 8";
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mejudul.Content = "MEJA";
        }
    }
}
