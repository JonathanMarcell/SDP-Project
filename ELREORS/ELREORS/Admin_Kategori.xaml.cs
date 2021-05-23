using System;
using System.Collections.Generic;
using System.Data;
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

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_Kategori.xaml
    /// </summary>
    public partial class Admin_Kategori : Page
    {
        DataTable dt;
        OracleConnection conn;
        public Admin_Kategori()
        {
            conn = App.conn;
            dt = new DataTable(); 
            InitializeComponent();
            loadKategori();
        }

        private void loadKategori()
        {
            try
            {
                if (tb_search.Text == "")
                {
                    OracleDataAdapter da = new OracleDataAdapter("select ID,NAMA AS \"Nama Kategori\" from Kategori order by ID", conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    dgKategori.ItemsSource = dt.DefaultView;
                }
                else
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select ID,NAMA AS \"Nama Kategori\" from Kategori where lower(NAMA) like :param order by ID";
                    cmd.Parameters.Add(":param", '%' + tb_search.Text.ToLower() + '%');
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    dgKategori.ItemsSource = dt.DefaultView;
                }
                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message); 
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            loadKategori();
        }

        private void dgKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgKategori.SelectedIndex>=0)
            {
                string idkat = dt.Rows[dgKategori.SelectedIndex]["ID"].ToString();  
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"select NAMA from Menu where id_kategori = {idkat}";
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                OracleDataReader reader = cmd.ExecuteReader();
                listMenu.Items.Clear();
                while (reader.Read())
                {
                    listMenu.Items.Add(reader.GetValue(0));
                }
                reader.Close();
            }
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            Admin_Kategori_InputBox w = new Admin_Kategori_InputBox();
            w.ShowDialog();
            string result = w.inputText;
            try
            {
                if (result != "")
                {
                    OracleCommand cmd = new OracleCommand($"insert into kategori values(null, '{result}' )", conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            loadKategori();
        }
        private void btnRename_Click(object sender, RoutedEventArgs e)
        {
            if (dgKategori.SelectedIndex >= 0)
            {
                try
                {
                    string idkat = dt.Rows[dgKategori.SelectedIndex]["ID"].ToString();
                    Admin_Kategori_InputBox w = new Admin_Kategori_InputBox(dt.Rows[dgKategori.SelectedIndex][1].ToString());
                    w.ShowDialog();
                    string result = w.inputText;
                    if (result!="")
                    {
                        OracleCommand cmd = new OracleCommand($"update kategori set nama='{result}' where id={idkat}", conn);
                        cmd.ExecuteNonQuery();
                    }
                    loadKategori();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgKategori.SelectedIndex>=0)
            {
                string idkat = dt.Rows[dgKategori.SelectedIndex]["ID"].ToString();
                if (listMenu.Items.IsEmpty)
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand($"delete from kategori where id = {idkat}", conn);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Tidak bisa menghapus Kategori yang dimiliki suatu Menu");
                }
            }
            loadKategori();
        }


        private void dgKategori_Loaded(object sender, EventArgs e)
        {
            dgKategori.Columns[0].Width = new DataGridLength(50);
            dgKategori.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    }
}
