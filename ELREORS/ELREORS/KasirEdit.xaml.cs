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
    /// Interaction logic for KasirEdit.xaml
    /// </summary>
    public partial class KasirEdit : Window
    {
        private int nomeja;
        public KasirEdit(int nomeja)
        {
            InitializeComponent();
            this.nomeja = nomeja;

            txtNoMeja.Text = nomeja.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
