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
    /// Interaction logic for ListPesanan.xaml
    /// </summary>
    public partial class ListPesanan : Window
    {
        public ListPesanan()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;

            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Nama Pesanan";
            c1.Binding = new Binding("Nama Pesananan");
            c1.Width = 320;
            dataGridView1.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Jumlah";
            c2.Width = 200;
            c2.Binding = new Binding("Jumlah");
            dataGridView1.Columns.Add(c2);
            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "Meja";
            c3.Width = 200;
            c3.Binding = new Binding("Meja");
            dataGridView1.Columns.Add(c3);
        }
        
        
    }
}
