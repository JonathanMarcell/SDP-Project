using Oracle.DataAccess.Client;
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
                Uri uri = new Uri("Resource/bglogin.png", UriKind.Relative);
                BitmapImage img = new BitmapImage(uri);
                ImageBrush b = new ImageBrush(img);
                //mode 
                b.Opacity = 1; //default 1
                b.TileMode = TileMode.Tile; //default None
                b.Stretch = Stretch.Fill; //default Fill

                win.Background = b; // win ini x:Name nya Window
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.ToString());
            }
            //cara ngakses objek dari suatu parent , harus diTypeCast
            //MessageBox.Show(    ((TextBlock)win.FindName("Title")).Text    );
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            //var connectionString = "Data Source = orcl; User ID= coba; Password= 1"; //nando
            //var connectionString = "Data Source = orcl; User ID= ; Password= ";
            //var connectionString = "Data Source = orcl; User ID= ; Password= ";
            //var connectionString = "Data Source = orcl; User ID= ; Password= ";
            //OracleConnection conn1 = new OracleConnection(connectionString);
            if (tb_username.Text=="admin")
            {
                Admin a = new Admin();
                a.Show();
                Close();
            }
            else if (tb_username.Text=="meja")
            {
                NoMeja n = new NoMeja();
                n.Show();
                this.Close();
            }
            else if (tb_username.Text == "kasir")
            {
                try
                {
                    Kasir mm = new Kasir();
                    mm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
