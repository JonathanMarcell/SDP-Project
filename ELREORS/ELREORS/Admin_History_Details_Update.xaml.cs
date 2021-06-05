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
using Oracle.DataAccess.Client;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_History_Details_Update.xaml
    /// </summary>
    public partial class Admin_History_Details_Update : Window
    {
        string iddjual;
        string idhjual;
        string kodenota;
        OracleConnection conn;
        List<m> listmenu = new List<m>();

        public Admin_History_Details_Update(string idhj,string kodenota,string iddj, string idmenu, int harga, int jumlah)
        {
            InitializeComponent();
            conn = App.conn;
            this.kodenota = kodenota;
            iddjual = iddj;
            idhjual = idhj;
            tbHarga.Text = harga.ToString();
            tbJumlah.Text = jumlah.ToString();
            loadmenu();
            cbMenu.SelectedValue = idmenu;
        }

        private class m
        {
            public string id { get; set; }
            public string name { get; set; }
            public m(string id,string name)
            {
                this.id = id;
                this.name = name;
            }
        }
        private void loadmenu()
        {
            try
            {
                listmenu = new List<m>();
                cbMenu.Items.Clear();
                OracleCommand cmd = new OracleCommand("select id, nama from Menu order by ID", conn);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listmenu.Add(new m(reader.GetValue(0).ToString(), reader.GetValue(1).ToString() ));
                }
                reader.Close();
                cbMenu.ItemsSource = listmenu;
                cbMenu.SelectedValuePath = "id";
                cbMenu.DisplayMemberPath = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Admin_History_Details w = new Admin_History_Details(kodenota, idhjual);
            w.ShowDialog();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbHarga.Text==""||tbJumlah.Text==""||cbMenu.SelectedIndex<0)
            {
                System.Windows.Forms.MessageBox.Show("Ada field kosong!");
                return;
            }
            string idmenu = cbMenu.SelectedValue.ToString();
            string qry = $"update djual set id_menu=:IDMENU , HARGA=:HARGA, JUMLAH=:JUMLAH where id={iddjual}";
            OracleCommand cmd = new OracleCommand(qry, conn);
            cmd.Parameters.Add("IDMENU", idmenu);
            cmd.Parameters.Add("HARGA", tbHarga.Text);
            cmd.Parameters.Add("JUMLAH", tbJumlah.Text);
            cmd.ExecuteNonQuery();

            cmd = new OracleCommand($"select status from hjual where id={idhjual}", conn);
            string result = cmd.ExecuteScalar().ToString();
            //asumsi penggantian terjadi bila status = 2 atau completed,
            //karena ketika confirmed (menunggu masak) belum tentu bisa
            // tetapi di admin tetap diberi kontrol penuh agar lebih fleksibel.
            if (result == "2" ) //dari completed ke editted
            {
                updateStatusHjual(idhjual,3);
            }
            Close();

            Admin_History_Details w = new Admin_History_Details(kodenota, idhjual);
            w.ShowDialog();
        }

        string _prevText = string.Empty;
        private void cbMenu_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var item in cbMenu.Items)
            {
                if (item.ToString().StartsWith(cbMenu.Text))
                {
                    _prevText = cbMenu.Text;
                    return;
                }
            }
            cbMenu.Text = _prevText;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string qry = $"delete from djual where id={iddjual}";
            OracleCommand cmd = new OracleCommand(qry, conn);
            cmd.ExecuteNonQuery();
            Close();
            Admin_History_Details w = new Admin_History_Details(kodenota,idhjual);
            w.ShowDialog();
        }
        public void updateStatusHjual(string id, int status)
        {
            string qry = $"update hjual set status={status} where id={id}";
            OracleCommand cmd = new OracleCommand(qry, App.conn);
            cmd.ExecuteNonQuery();
        }
    }
}
