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
    /// Interaction logic for Admin_Stock.xaml
    /// </summary>
    public partial class Admin_Stock : Page
    {
        OracleConnection conn;
        DataTable dt;
        OracleCommandBuilder builder;
        public Admin_Stock()
        {
            InitializeComponent();
            conn = App.conn;
            loaddata();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            loaddata();
        }
        public void loaddata()
        {
            if (tb_search.Text=="")
            {
                OracleDataAdapter da = new OracleDataAdapter(
                    "select ID, KODE_BAHAN AS KODE, NAMA , STOK , SATUAN from bahan order by id", conn);
                dt = new DataTable();
                da.Fill(dt);
                dg_Stock.ItemsSource = dt.DefaultView;
            }
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select ID, KODE_BAHAN AS KODE, NAMA , STOK , SATUAN from bahan where lower(nama) like :param order by id ";
                cmd.Parameters.Add(":param", '%' + tb_search.Text.ToLower() + '%');
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                dg_Stock.ItemsSource = dt.DefaultView;
            }
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            Admin_Stock_Insert w = new Admin_Stock_Insert();
            w.ShowDialog();
            loaddata();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            int idx = dg_Stock.SelectedIndex;
            if (idx < 0)
            {
                return;
            }

            string id = dt.Rows[idx]["ID"].ToString();
            string kode = dt.Rows[idx]["KODE"].ToString();
            string nama = dt.Rows[idx]["NAMA"].ToString();
            string stok = dt.Rows[idx]["STOK"].ToString();
            string satuan = dt.Rows[idx]["SATUAN"].ToString();

            Admin_Stock_Update w = new Admin_Stock_Update(id,kode,nama,stok,satuan);
            w.ShowDialog();
            loaddata();
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dg_Stock_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void dg_Stock_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dg_Stock.Columns[0].Width = new DataGridLength(50);
        }
    }
}
