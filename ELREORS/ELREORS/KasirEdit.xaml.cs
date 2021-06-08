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
    /// Interaction logic for KasirEdit.xaml
    /// </summary>
    public partial class KasirEdit : Window
    {
        private int nomeja, jumlah, idmenu;
        private string idhjual, menu;

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public KasirEdit(int nomeja, string idhjual, string menu, int jumlah, int idmenu)
        {
            InitializeComponent();
            try
            {
                //nambah gambar di Background (pake brush) / Image (pake ImageSource)
                Uri uri = new Uri("Resource/background.png", UriKind.Relative);
                BitmapImage img = new BitmapImage(uri);
                ImageBrush b = new ImageBrush(img);
                //mode 
                b.Opacity = 1; //default 1
                b.TileMode = TileMode.Tile; //default None
                b.Stretch = Stretch.Fill; //default Fill

                winKE.Background = b; // win ini x:Name nya Window
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            this.nomeja = nomeja;
            this.idhjual = idhjual;
            this.menu = menu;
            this.jumlah = jumlah;
            this.idmenu = idmenu;

            txtNoMeja.Text = nomeja.ToString();
            txtId.Text = idhjual;
            txtMenu.Text = menu;
            txtJumlah.Text = jumlah.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string qry = $"update djual set jumlah={txtJumlah.Text} where id_menu={idmenu}";
            OracleCommand cmd = new OracleCommand(qry, App.conn);
            cmd.ExecuteNonQuery();
            this.Close();
        }

        private void btnHapus_Click(object sender, RoutedEventArgs e)
        {
            string qry = $"delete from djual where id_menu={idmenu}";
            OracleCommand cmd = new OracleCommand(qry, App.conn);
            cmd.ExecuteNonQuery();
            this.Close();
        }
    }
}
