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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_Setting.xaml
    /// </summary>
    public partial class Admin_Setting : Page
    {
        string imagepath;
        public Admin_Setting()
        {
            InitializeComponent();
            tbJumlahMeja.Text = App.getJumlahMeja() + "";
            tbNamaResto.Text = App.getNamaResto() + "";
            tbJAlamat.Text = App.getAlamat() + "";
            tbNoTelp.Text = App.getTelepon() + "";
            loadimage();
        }
        private void loadimage()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("select logo from profil", App.conn);
                if (cmd.ExecuteScalar() == null || cmd.ExecuteScalar().ToString()=="") { return; }
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Byte[] data = (Byte[])(dr.GetOracleBlob(0)).Value;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
                {
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = ms;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();

                    // Assign the Source property of your image
                    img.Source = imageSource;
                }
            }
            catch (Exception)
            {
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (imagepath=="" || imagepath==null)
                {
                    string qry = "update PROFIL set JUMLAH_MEJA=:jum, NAMA_RESTO=:nama, TELEPON=:telp, ALAMAT=:alamat";
                    OracleCommand cmd = new OracleCommand(qry, App.conn);
                    cmd.Parameters.Add("jum", tbJumlahMeja.Text);
                    cmd.Parameters.Add("nama", tbNamaResto.Text);
                    cmd.Parameters.Add("telp", tbNoTelp.Text);
                    cmd.Parameters.Add("alamat", tbJAlamat.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil menyimpan");
                }
                else
                {
                    string qry = "update PROFIL set JUMLAH_MEJA=:jum, NAMA_RESTO=:nama, TELEPON=:telp, ALAMAT=:alamat, LOGO=:gambar";
                    OracleCommand cmd = new OracleCommand(qry, App.conn);
                    cmd.Parameters.Add("jum", tbJumlahMeja.Text);
                    cmd.Parameters.Add("nama", tbNamaResto.Text);
                    cmd.Parameters.Add("telp", tbNoTelp.Text);
                    cmd.Parameters.Add("alamat", tbJAlamat.Text);

                    FileStream fls;
                    fls = new FileStream(@imagepath, FileMode.Open, FileAccess.Read);
                    byte[] blob = new byte[fls.Length];
                    fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                    fls.Close();
                    cmd.Parameters.Add("gambar", OracleDbType.Blob).Value = blob;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil menyimpan");
                    loadimage();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal menyimpan");
            }
        }

        private void btnPilihLogo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                //specify your own initial directory 
                fldlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                fldlg.Title = "Select an Image";
                //this will allow onlt those file extensions to be added 
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif;*.png";
                if (fldlg.ShowDialog() == true)
                {
                    imagepath = fldlg.FileName;
                    lbDirectory.Content = imagepath;
                }
                fldlg = null;
            }
            catch (System.ArgumentException ae)
            {
                imagepath = " ";
                MessageBox.Show(ae.Message.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
