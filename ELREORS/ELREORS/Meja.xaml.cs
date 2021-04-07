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
        string no;
        string temp;
        public Meja(string n)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;

            no =  n.Substring(0,1).ToUpper() + n.Substring(1, 3) + " " + n.Substring(4);
            temp = n;
            lbNama.Content = no;
        }
        private void btnPesan_Click(object sender, RoutedEventArgs e)
        {
            tunggu t = new tunggu(temp);
            t.Show();
            Close();
        }
    }
}
