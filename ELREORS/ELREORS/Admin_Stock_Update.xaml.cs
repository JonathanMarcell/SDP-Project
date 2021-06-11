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
    /// Interaction logic for Admin_Stock_Update.xaml
    /// </summary>
    public partial class Admin_Stock_Update : Window
    {
        private string id;
        public Admin_Stock_Update(string id,string kode,string nama, string stok ,string satuan)
        {
            InitializeComponent();
            labelid.Content += kode;
            this.id = id;
            tb_nama.Text = nama;
            tb_satuan.Text = satuan;
            tb_stok.Text = stok;
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
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
                string qry = $"UPDATE BAHAN SET " +
                    "NAMA = :NAMA ,SATUAN = :SATUAN, STOK = :STOK " +
                    "WHERE ID=:ID";

                OracleCommand cmd = new OracleCommand(qry, App.conn); //tetap lakukan oracle command.
                cmd.Parameters.Add("NAMA", tb_nama.Text);
                cmd.Parameters.Add("SATUAN", OracleDbType.Varchar2).Value = tb_satuan.Text;
                cmd.Parameters.Add("STOK", OracleDbType.Int32).Value = stok;
                cmd.Parameters.Add("ID", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Berhasil");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
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
