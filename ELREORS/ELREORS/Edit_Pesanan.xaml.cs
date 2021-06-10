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
    /// Interaction logic for Edit_Pesanan.xaml
    /// </summary>
    public partial class Edit_Pesanan : Window
    {
        public Edit_Pesanan()
        {
            InitializeComponent();
            Uri uri = new Uri("Resource/background  no bottom.png", UriKind.Relative);
            BitmapImage img = new BitmapImage(uri);
            ImageBrush b = new ImageBrush(img);

            b.Opacity = 1;
            b.TileMode = TileMode.Tile;
            b.Stretch = Stretch.Fill;
            Edit.Background = b;

            uri = new Uri("Resource/button square wide.png", UriKind.Relative);
            img = new BitmapImage(uri);
            b = new ImageBrush(img);

            b.Opacity = 1;
            b.TileMode = TileMode.Tile;
            b.Stretch = Stretch.Fill;
            btnBersihkan.Background = b;
            btnEditL.Background = b;
            btnSelesai.Background = b;
        }
        private void btnEditL_Click(object sender, RoutedEventArgs e)
        {
            if (tbNo.Text==""|| tbKet.Text=="")
            {
                MessageBox.Show("Harap isi semua field");
            }
            else
            {
                foreach (Window item in Application.Current.Windows)
                {
                    MessageBox.Show(item.Name.ToString());
                    if (item.Name == "MenuMeja")
                    {
                        ((Meja)item).daO.Rows[Convert.ToInt32(tbNo.Text) - 1]["Keterangan"] = tbKet.Text;
                        ((Meja)item).dataOrder.ItemsSource = ((Meja)item).daO.DefaultView;
                        tbNo.Text = "";
                        tbKet.Text = "";
                    }
                }
            }
        }
        private void btnBersihkan_Click(object sender, RoutedEventArgs e)
        {
            tbNo.Text = "";
            tbKet.Text = "";
        }
        private void btnSelesai_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
