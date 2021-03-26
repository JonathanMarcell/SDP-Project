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
            //DataGridTextColumn dtKasir1 = new DataGridTextColumn();
            //dtKasir1.Header = "Nama Menu";
            //dtKasir1.Binding = new Binding("Nama Menu");
            //dtKasir1.Width = 650;
            //dataGrid.Columns.Add(dtKasir1);
            //DataGridTextColumn dtKasir2 = new DataGridTextColumn();
            //dtKasir2.Header = "Jumlah";
            //dtKasir2.Width = 250;
            //dtKasir2.Binding = new Binding("Jumlah");
            //dataGrid.Columns.Add(dtKasir2);
            //DataGridTextColumn dtKasir3 = new DataGridTextColumn();
            //dtKasir3.Header = "Harga";
            //dtKasir3.Width = 390;
            //dtKasir3.Binding = new Binding("Harga");
            //dataGrid.Columns.Add(dtKasir3);
            //DataGridTextColumn dtKasir4 = new DataGridTextColumn();
            //dtKasir4.Header = "Subtotal";
            //dtKasir4.Width = 390;
            //dtKasir4.Binding = new Binding("Subtotal");
            //dataGrid.Columns.Add(dtKasir4);
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
            MainWindow mainwin = new MainWindow();
            mainwin.Show();
        }
    }
}
