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
using Oracle.DataAccess.Client;
using System.Data;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_Pegawai.xaml
    /// </summary>
    public partial class Admin_Pegawai : Page
    {

        OracleConnection conn;
        DataTable dt;
        public Admin_Pegawai()
        {
            InitializeComponent();
            conn = App.conn;
            cbShowAdmin.IsChecked = true;
            cbShowKaryawan.IsChecked = true;
            cbShowAktif.IsChecked = true;
            cbShowNonAktif.IsChecked = true;
            loaddg();
        }

        private void loaddg()
        {
            string query = "select p.kode_pegawai as \"KODE\" ,p.nama as \"NAMA\" , " +
                    "case when p.isadmin = 0 then 'Karyawan' " +
                    "when p.isadmin = 1 then 'Admin' " +
                    "else '-' end as \"ROLE\" , " +
                    "case when p.status = 0 then 'Non-Aktif' " +
                    "when p.status = 1 then 'Aktif' " +
                    "else '-' end as \"STATUS\" " +
                    "from pegawai p where (p.id is null ";

            if (cbShowAdmin.IsChecked == true)
            {
                query += "or p.isadmin = 1 ";
            }
            if (cbShowKaryawan.IsChecked == true)
            {
                query += "or p.isadmin = 0 ";
            }
            if (cbShowAktif.IsChecked == true)
            {
                query += "or p.status = 1 ";
            }
            if (cbShowNonAktif.IsChecked == true)
            {
                query += "or p.status = 0 ";
            }
            query += ") ";
            if (tb_search.Text=="")
            {
                OracleDataAdapter da = new OracleDataAdapter(
                    query + "order by p.nama", conn);

                dt = new DataTable();
                da.Fill(dt);
                dg_pegawai.ItemsSource = dt.DefaultView;
            }
            else
            {
                OracleCommand cmd = new OracleCommand(query + $"and nama like :namacari order by p.nama",conn);
                cmd.Parameters.Add("namacari","%"+tb_search.Text+"%");
                OracleDataAdapter da = new OracleDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dg_pegawai.ItemsSource = dt.DefaultView;
            }
        }

        private void dg_pegawai_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int idx = dg_pegawai.SelectedIndex;
            if (idx < 0)
            {
                return;
            }

            string kode = dt.Rows[idx]["KODE"].ToString();
            string nama = dt.Rows[idx]["NAMA"].ToString();
            string role = dt.Rows[idx]["ROLE"].ToString();
            string status = dt.Rows[idx]["STATUS"].ToString();

            OracleCommand cmd = new OracleCommand($"select jenis_kelamin from pegawai where kode_pegawai='{kode}'" ,conn);
            string jk = cmd.ExecuteScalar().ToString();

            lb_kode.Content = kode;
            tb_nama.Text = nama;
            if (jk.ToUpper() =="L")
            {
                rb_setGenderL.IsChecked = true;
                rb_setGenderP.IsChecked = false;
            }
            else if (jk.ToUpper() == "P")
            {
                rb_setGenderP.IsChecked = true;
                rb_setGenderL.IsChecked = false;
            }

            if (status == "Non-Aktif")
            {
                rb_Status0.IsChecked = true;
                rb_Status1.IsChecked = false;
            }
            else if (status == "Aktif")
            {
                rb_Status1.IsChecked = true;
                rb_Status0.IsChecked = false;
            }

            if (role == "Karyawan")
            {
                rb_setAdminFalse.IsChecked= true;
                rb_setAdminTrue.IsChecked= false;
            }
            else if (role == "Admin")
            {
                rb_setAdminTrue.IsChecked = true;
                rb_setAdminFalse.IsChecked = false;
            }
            btn_Insert.IsEnabled = false;
            btn_update.IsEnabled = true;
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            loaddg();
        }

        private void cbShowKaryawan_Checked(object sender, RoutedEventArgs e)
        {
            loaddg();
        }

        private void cbShowAdmin_Checked(object sender, RoutedEventArgs e)
        {
            loaddg();
        }

        private void cbShowAktif_Checked(object sender, RoutedEventArgs e)
        {
            loaddg();
        }

        private void cbShowNonAktif_Checked(object sender, RoutedEventArgs e)
        {
            loaddg();
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            lb_kode.Content = "";
            tb_nama.Text = "";
            rb_setAdminTrue.IsChecked = false;
            rb_setAdminFalse.IsChecked = true;
            rb_Status0.IsChecked = false;
            rb_Status1.IsChecked = true;
            btn_Insert.IsEnabled = true;
            btn_update.IsEnabled = false;
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            if (tb_nama.Text == "")
            {
                MessageBox.Show("Data tidak lengkap!");
                return;
            }
            string qry = $"INSERT INTO PEGAWAI (ID, KODE_PEGAWAI, NAMA, JENIS_KELAMIN, ISADMIN, STATUS) VALUES " +
                    $"(null, null, :NM, :JK, :ADMIN, :STAT )";

            OracleCommand cmd = new OracleCommand(qry, conn);
            cmd.Parameters.Add("NM", tb_nama.Text);
            cmd.Parameters.Add("JK", rb_setGenderL.IsChecked == true ? "L" : "P");
            cmd.Parameters.Add("ADMIN", rb_setAdminTrue.IsChecked == true ? 1 : 0);
            cmd.Parameters.Add("STAT", rb_Status1.IsChecked == true ? 1 : 0);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Berhasil insert");
            loaddg();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            if (lb_kode.Content.ToString()=="" || tb_nama.Text=="")
            {
                MessageBox.Show("Data tidak lengkap!");
                return;
            }
            string qry = $"update PEGAWAI set NAMA = :NM, JENIS_KELAMIN = :JK, ISADMIN = :ADMIN, STATUS=:STAT WHERE KODE_PEGAWAI='{lb_kode.Content}' ";
            OracleCommand cmd = new OracleCommand(qry, conn);
            cmd.Parameters.Add("NM", tb_nama.Text);
            cmd.Parameters.Add("JK", rb_setGenderL.IsChecked == true ? "L" : "P");
            cmd.Parameters.Add("ADMIN", rb_setAdminTrue.IsChecked == true ? 1 : 0);
            cmd.Parameters.Add("STAT", rb_Status1.IsChecked == true ? 1 : 0);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Berhasil update");
            loaddg();
        }

        private void btn_insertmode_Click(object sender, RoutedEventArgs e)
        {
            lb_kode.Content = "";
            btn_Insert.IsEnabled = true;
            btn_update.IsEnabled = false;
        }

        private void dg_pegawai_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dg_pegawai.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    }
}
