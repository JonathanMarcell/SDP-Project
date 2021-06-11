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
    /// Interaction logic for Admin_Stock_Insert.xaml
    /// </summary>
    public partial class Admin_Stock_Insert : Window
    {
        public Admin_Stock_Insert()
        {
            InitializeComponent();
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            int stok;
            bool isNumeric = int.TryParse(tb_stok.Text.Trim(), out stok);

            if (tb_nama.Text == "" || tb_satuan.Text == "" || !isNumeric || tb_stok.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Isi Kosong atau salah");
                return;
            }
            try
            {
                string qry = $"INSERT INTO BAHAN(ID, KODE_BAHAN, NAMA, STOK, SATUAN, STATUS) VALUES" +
                    $"(null , null , :NAMA , :STOK , :SATUAN , 1)";

                OracleCommand cmd = new OracleCommand(qry, App.conn); //tetap lakukan oracle command.
                cmd.Parameters.Add("NAMA", tb_nama.Text);
                cmd.Parameters.Add("STOK", OracleDbType.Int32).Value = stok;
                cmd.Parameters.Add("SATUAN", OracleDbType.Varchar2).Value= tb_satuan.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Berhasil");
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        private void clear()
        {
            tb_nama.Text = "";
            tb_satuan.Text = "";
            tb_stok.Text = "";
        }
    }
}
