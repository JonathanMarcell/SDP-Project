using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //fullscreen & no window control, nanti kasi ini ke semua wpf
            this.WindowState= WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;

            //misc
            try
            {
                //nambah gambar di Background (pake brush) / Image (pake ImageSource)
                Uri uri = new Uri("Resource/bg1.jpg", UriKind.Relative);
                BitmapImage img = new BitmapImage(uri);
                ImageBrush b = new ImageBrush(img);
                //mode 
                b.Opacity = 1 ; //default 1
                b.TileMode = TileMode.Tile; //default None
                b.Stretch = Stretch.Fill; //default Fill

                win.Background = b; // win ini x:Name nya Window
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            //cara ngakses objek dari suatu parent , harus diTypeCast
            //MessageBox.Show(    ((TextBlock)win.FindName("Title")).Text    );
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (tb_username.Text=="admin")
            {
                Admin a = new Admin();
                a.Show();
                Close();
            }
            else if (tb_username.Text=="meja1" || tb_username.Text == "meja2" || tb_username.Text == "meja3" || tb_username.Text == "meja4" || tb_username.Text == "meja5" ||
                tb_username.Text == "meja6" || tb_username.Text == "meja7" || tb_username.Text == "meja8" || tb_username.Text == "meja9" || tb_username.Text == "meja10")
            {
                Meja a = new Meja(tb_username.Text);
                a.Show();
                Close();
            }
            else if (tb_username.Text=="kasir")
            {
                Kasir a = new Kasir();
                a.Show();
                Close();
            }
            else if (tb_username.Text=="list")
            {
                ListPesanan a = new ListPesanan();
                a.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Failed to Login");
            }
            
        }

        //nyoba bisa manggil keyboard tp masi ga bisa
        private Process _p = null;
        private void cmdToggle_Click(object sender, RoutedEventArgs e)
        {
            if (_p == null)
            {
                ProcessStartInfo p = new ProcessStartInfo(((Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\osk.exe")));
                try
                {
                    _p = Process.Start("osk");
                }
                catch (Exception)
                {
                    try
                    {
                        p.UseShellExecute = true;
                        _p = Process.Start(p);
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Failed to open on screen keyboard");
                        Console.WriteLine("Failed to open on screen keyboard");
                    }
                }
            }
            else
            {
                _p.Kill();
                _p.Dispose();
                _p = null;
            }
        }

    }
}
