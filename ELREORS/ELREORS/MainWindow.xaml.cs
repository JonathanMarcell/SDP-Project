using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
        //password
        string AdminPass = "admin";
        string ChefPass = "list";
        string TablePass = "meja";
        string CashierPass = "kasir";
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
            string pass = pb_password.Password;
            string uname = tb_username.Text;
            bool found = false; 
            string CurrentUser = "";
            string isadmin = "";
            //check login di pegawai, lihat dari username, status, isAdmin. tidak ada password (hardcode password)
            if (uname=="" || pass=="")
            {
                System.Windows.Forms.MessageBox.Show("Harap isi semua field");
                return;
            }
            try
            {
                DataTable tempData = new DataTable();
                OracleDataAdapter da = new OracleDataAdapter("select id,kode_pegawai,nama,isadmin,status from pegawai", App.conn);
                da.Fill(tempData);
                foreach (DataRow d in tempData.Rows)
                {
                    if (d["kode_pegawai"].ToString() == uname) //login pake kode pegawai
                    {
                        if (d["status"].ToString() == "1")
                        {
                            found = true;
                            CurrentUser = d["kode_pegawai"].ToString();
                            isadmin = d["isadmin"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Gagal mencari user!");
                return;
            }

            if (found)
            {
                if (CurrentUser=="")
                {
                    System.Windows.Forms.MessageBox.Show("Username diblokir!");
                }
                else
                {
                    if (pass == AdminPass && isadmin.Trim()=="1")
                    {
                        Admin a = new Admin();
                        a.ShowDialog();
                    }
                    else if (pass == ChefPass)
                    {
                        ListPesanan a = new ListPesanan();
                        a.ShowDialog();
                    }
                    else if (pass == TablePass)
                    {
                        NoMeja n = new NoMeja();
                        n.ShowDialog();
                    }
                    else if (pass == CashierPass)
                    {
                        Kasir mm = new Kasir(CurrentUser);
                        mm.ShowDialog();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Password Salah!");
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Username tidak ditemukan!");
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
        static void thread_test()
        {
            Console.WriteLine("Culture: {0}", System.Globalization.CultureInfo.CurrentCulture.DisplayName);
        }

    }
}
