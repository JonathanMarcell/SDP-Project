﻿using System;
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
    /// Interaction logic for Admin_Menu.xaml
    /// </summary>
    public partial class Admin_Menu : Page
    {
        OracleConnection conn;
        DataTable dt;
        public Admin_Menu()
        {
            InitializeComponent();
            conn = App.conn;
            loaddata();
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            Admin_Menu_Insert a = new Admin_Menu_Insert();
            a.ShowDialog();
            loaddata();
        }
        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            int idx = dg_Menu.SelectedIndex;
            if (idx<0)
            {
                return;
            }
            string id  = dt.Rows[idx]["ID"].ToString();
            string kode = dt.Rows[idx]["KODE"].ToString();
            string nm  = dt.Rows[idx]["NAMA"].ToString();
            string stat  = dt.Rows[idx]["STATUS"].ToString();
            string hrg  = dt.Rows[idx]["HARGA"].ToString();
            string ketr  = dt.Rows[idx]["KETERANGAN"].ToString();
            string kategori = dt.Rows[idx]["KATEGORI"].ToString();
            Admin_Menu_Update a = new Admin_Menu_Update(id,kode,kategori,nm,stat,hrg,ketr);
            a.ShowDialog();
            loaddata();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            loaddata();
        }

        public void loaddata()
        {
            if (tb_search.Text == "")
            {
                OracleDataAdapter da = new OracleDataAdapter(
                    "select m.ID, KODE_MENU as KODE , m.NAMA , k.NAMA as KATEGORI , CASE when m.STATUS = 1 then 'Aktif' ELSE 'NonAktif' END as STATUS , HARGA , KETERANGAN from menu m join kategori k on k.id=m.id_kategori order by 1", conn);
                dt = new DataTable();
                da.Fill(dt);
                dg_Menu.ItemsSource = dt.DefaultView;
            }
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select m.ID, KODE_MENU as KODE , m.NAMA , k.NAMA as KATEGORI , CASE when m.STATUS = 1 then 'Aktif' ELSE 'NonAktif' END as STATUS , HARGA , KETERANGAN from menu m join kategori k on k.id=m.id_kategori where lower(m.nama) like :param order by 1";
                cmd.Parameters.Add(":param", '%'+ tb_search.Text.ToLower() + '%');
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                dg_Menu.ItemsSource = dt.DefaultView;
            }
        }

        private void dg_Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = dg_Menu.SelectedIndex+1;
            if (idx == 0) {
                img.Source=null; 
                return; 
            }
            try
            {
                OracleCommand cmd = new OracleCommand($"select gambar from menu where id={idx}", App.conn);

                if (cmd.ExecuteScalar() == null || cmd.ExecuteScalar().ToString() == "")
                {
                    img.Source = null;
                    return; 
                }
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
            catch (Exception ex)
            {
                MessageBox.Show(idx + " - "+ ex);
            }
        }
    }
}
