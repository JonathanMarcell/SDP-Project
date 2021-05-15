using Microsoft.Win32;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Admin_Menu_Update.xaml
    /// </summary>
    public partial class Admin_Menu_Update : Window
    {
        string Id;
        OracleConnection conn;
        string imagepath="";
        List<Kategori> listkat = new List<Kategori>();
        public Admin_Menu_Update(string id, string kd,string kategori,string nm,string status,string hrg,string keterangan)
        {
            InitializeComponent();
            conn = App.conn;
            loadKategori();
            Id = id;
            label_id.Content += kd;
            tb_harga.Text = hrg;
            tb_keterangan.Text = keterangan;
            tb_nama.Text = nm;
            if (status.ToLower() == "aktif")
            {
                cb_status.IsChecked = true;
            }
            int temp=0;
            foreach (Kategori item in listkat)
            {
                if (item.nama==kategori)
                {
                    cbKategori.SelectedIndex = temp;
                }
                temp++;
            }
        }
        private void loadKategori()
        {
            try
            {
                listkat = new List<Kategori>();
                cbKategori.Items.Clear();
                OracleCommand cmd = new OracleCommand("select ID, NAMA from Kategori", conn);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listkat.Add(new Kategori(reader.GetValue(0).ToString(), reader.GetValue(1).ToString() ) );
                }
                reader.Close();
                cbKategori.ItemsSource=listkat;
                cbKategori.DisplayMemberPath = "nama";
                cbKategori.SelectedValuePath = "id";
            }
            catch (Exception)
            {
                MessageBox.Show("failed to load kategori");
            }
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            string idkat;
            if (tb_harga.Text=="" || tb_nama.Text == "" || cbKategori.SelectedIndex==-1)
            {
                MessageBox.Show("Ada field kosong");
                return;
            }
            idkat = cbKategori.SelectedValue.ToString();
            bool success = false;
            try
            {
                if (imagepath !="")
                {
                    string qry = $"update menu set ID_KATEGORI={idkat}, NAMA=:NAMA, HARGA=:HARGA , STATUS=:STATUS , KETERANGAN=:KETERANGAN , GAMBAR=:GAMBAR where id={Id}";
                    FileStream fls;
                    fls = new FileStream(@imagepath, FileMode.Open, FileAccess.Read);
                    //a byte array to read the image 
                    byte[] blob = new byte[fls.Length];
                    fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                    fls.Close();

                    OracleCommand cmd = new OracleCommand(qry, conn);
                    cmd.Parameters.Add("NAMA", tb_nama.Text);
                    cmd.Parameters.Add("HARGA", tb_harga.Text);
                    cmd.Parameters.Add("STATUS", cb_status.IsChecked == true ? 1 : 0);
                    cmd.Parameters.Add("KETERANGAN", tb_keterangan.Text);
                    cmd.Parameters.Add("GAMBAR", OracleDbType.Blob).Value = blob;
                    cmd.ExecuteNonQuery();
                    success = true;
                }
                else
                {
                    string qry = $"update menu set ID_KATEGORI={idkat}, NAMA=:NAMA, HARGA=:HARGA , STATUS=:STATUS , KETERANGAN=:KETERANGAN where id={Id}";
                    OracleCommand cmd = new OracleCommand(qry, conn);
                    cmd.Parameters.Add("NAMA", tb_nama.Text);
                    cmd.Parameters.Add("HARGA", tb_harga.Text);
                    cmd.Parameters.Add("STATUS", cb_status.IsChecked == true ? 1 : 0);
                    cmd.Parameters.Add("KETERANGAN", tb_keterangan.Text);
                    cmd.ExecuteNonQuery();
                    success = true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (success)
            {
                MessageBox.Show("Update Berhasil!");
                this.Close();
            }
        }

        private void btn_pilihBahan_Click(object sender, RoutedEventArgs e)
        {
            Admin_Menu_PilihBahan w = new Admin_Menu_PilihBahan(Id, tb_nama.Text);
            w.Show();
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
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
                    labelimgpath.Content = "Gambar : " + imagepath;
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

        string _prevText = string.Empty;

        private void cbKategori_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var item in cbKategori.Items)
            {
                if (item.ToString().StartsWith(cbKategori.Text))
                {
                    _prevText = cbKategori.Text;
                    return;
                }
            }
            cbKategori.Text = _prevText;
        }
    }
}
