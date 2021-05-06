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
using System.Data;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_Menu_PilihBahan.xaml
    /// </summary>
    public partial class Admin_Menu_PilihBahan : Window
    {
        string idMenu;
        List<Bahan> listbhn;
        DataTable dtBahanTerpilih = new DataTable();
        public Admin_Menu_PilihBahan(string id , string namamenu)
        {
            InitializeComponent();
            idMenu = id;
            lbMenu.Content = namamenu;
            listbhn = new List<Bahan>();
            loadCBbahan();
            dtBahanTerpilih.Columns.Add("IdBahan");
            dtBahanTerpilih.Columns.Add("Bahan");
            dtBahanTerpilih.Columns.Add("Jumlah");
            dtBahanTerpilih.Columns.Add("satuan");

            dtBahanTerpilih.Columns["IdBahan"].ReadOnly = true;
            dtBahanTerpilih.Columns["Bahan"].ReadOnly = true;
            dtBahanTerpilih.Columns["satuan"].ReadOnly = true;

            dgbahan.ItemsSource = dtBahanTerpilih.DefaultView;
            //dgbahan.Columns[0].Visibility = Visibility.Collapsed;
            loadDGbahan();
        }

        class Bahan
        {
            public string id { get; set; }
            public string nama { get; set; }
            public Bahan(string id,string nama)
            {
                this.id = id;
                this.nama = nama;
            }
        }

        private void loadCBbahan()
        {
            try
            {
                listbhn = new List<Bahan>();
                cbBahan.Items.Clear();
                OracleCommand cmd = new OracleCommand("select ID, NAMA from Bahan", App.conn);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listbhn.Add(new Bahan(reader.GetValue(0).ToString(), reader.GetValue(1).ToString()));
                }
                reader.Close();
                cbBahan.ItemsSource = listbhn;
                cbBahan.DisplayMemberPath = "nama";
                cbBahan.SelectedValuePath = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }


        private void loadDGbahan()
        {
            OracleCommand cmd = new OracleCommand($"select mb.id_bahan ,b.nama , mb.jumlah , b.satuan from menu_bahan mb join bahan b on b.id=mb.id_bahan where mb.id_menu={idMenu}",App.conn);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DataRow newRow = dtBahanTerpilih.NewRow();

                newRow["IdBahan"] = dr.GetValue(0).ToString();
                newRow["Bahan"] = dr.GetValue(1).ToString();
                newRow["Jumlah"] = dr.GetValue(2).ToString();
                newRow["Satuan"] = dr.GetValue(3).ToString();
                dtBahanTerpilih.Rows.Add(newRow);
            }
            dr.Close();
            dgbahan.ItemsSource = dtBahanTerpilih.DefaultView;

        }
        string satuan = "";
        private void cbBahan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = cbBahan.SelectedIndex+1;
            satuan = "";
            if (idx>0)
            {
                OracleCommand cmd = new OracleCommand($"select satuan from bahan where id={idx}",App.conn);
                satuan = cmd.ExecuteScalar().ToString();
            }

        }

        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {
            int idBhn = Convert.ToInt32( cbBahan.SelectedValue );
            if (idBhn == 0)
            {
                return;
            }
            //insert dg
            foreach (DataRow item in dtBahanTerpilih.Rows)
            {
                if ( Convert.ToInt32( item["IdBahan"] ) == idBhn)
                {
                    item["Jumlah"] = Convert.ToInt32(item["Jumlah"])+1;
                    return;
                }
            }
            DataRow newRow = dtBahanTerpilih.NewRow();

            newRow["IdBahan"] = idBhn;
            newRow["Bahan"] = listbhn.Find(x => x.id==idBhn.ToString()).nama;
            newRow["Jumlah"] = 1;
            newRow["Satuan"] = satuan;
            
            dtBahanTerpilih.Rows.Add(newRow);
        }

        private void btnSimpan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //delete  where id menu
                OracleCommand cmd = new OracleCommand($"delete from menu_bahan where id_menu={idMenu}", App.conn);
                cmd.ExecuteNonQuery();

                //insert again
                foreach (DataRow rows in dtBahanTerpilih.Rows)
                {
                    cmd = new OracleCommand($"insert into menu_bahan values(:idmenu,:idbahan,:jumlah)", App.conn);
                    cmd.Parameters.Add("idmenu", idMenu);
                    cmd.Parameters.Add("idbahan", rows["IdBahan"]);
                    cmd.Parameters.Add("jumlah", rows["Jumlah"]);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Berhasil pilih bahan"); ;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex+"");
            }

        }

        private void btnBersihkan_Click(object sender, RoutedEventArgs e)
        {
            dtBahanTerpilih.Clear();
        }

    }
}
