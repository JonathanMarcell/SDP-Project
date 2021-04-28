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
using System.IO;
using Microsoft.Win32;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_Menu_Insert.xaml
    /// </summary>
    public partial class Admin_Menu_Insert : Window
    {
        string newId;
        OracleConnection conn;
        string imagepath;
        public Admin_Menu_Insert()
        {
            InitializeComponent();
            conn = App.conn;
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            try
            {
                string qry = $"INSERT INTO MENU (ID, KODE_MENU, NAMA, HARGA, STATUS, KETERANGAN ,GAMBAR) VALUES " +
                    $"(null, null, :NAMA, :HARGA, :STATUS, :KETERANGAN , :GAMBAR)";

                FileStream fls;
                fls = new FileStream(@imagepath, FileMode.Open, FileAccess.Read);
                //a byte array to read the image 
                byte[] blob = new byte[fls.Length];
                fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                fls.Close();

                OracleCommand cmd = new OracleCommand(qry, conn); //tetap lakukan oracle command.
                cmd.Parameters.Add("NAMA",tb_nama.Text);
                cmd.Parameters.Add("HARGA",tb_harga.Text);
                cmd.Parameters.Add("STATUS", cb_status.IsChecked==true ? 1 : 0 );
                cmd.Parameters.Add("KETERANGAN",tb_keterangan.Text);
                cmd.Parameters.Add("GAMBAR",OracleDbType.Blob).Value=blob;
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (success)
            {
                MessageBox.Show("Insert Berhasil!");
                this.Close();
            }
        }

        private void btn_pilihBahan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fldlg= new OpenFileDialog();
                //specify your own initial directory 
                fldlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                fldlg.Title = "Select an Image";
                //this will allow onlt those file extensions to be added 
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == true)
                {
                    imagepath = fldlg.FileName;
                    labelimgpath.Content = "Gambar : "+imagepath;
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
