﻿using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //fullscreen & no window control, nanti kasi ini ke semua wpf
            this.WindowState= WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = "Data Source = orcl; User ID= coba; Password= 1";
            OracleConnection conn1 = new OracleConnection(connectionString);
            if (tb_username.Text=="admin")
            {
                Admin a = new Admin();
                a.Show();
                Close();
            }
            else if (tb_username.Text=="meja1" || tb_username.Text == "meja2" || tb_username.Text == "meja3" || tb_username.Text == "meja4" || tb_username.Text == "meja5" ||
                tb_username.Text == "meja6" || tb_username.Text == "meja7" || tb_username.Text == "meja8" || tb_username.Text == "meja9" || tb_username.Text == "meja10")
            {
                Meja a = new Meja(tb_username.Text);
                a.Show();
                Close();
            }
            else if (tb_username.Text=="kasir")
            {
                try
                {
                    conn1.Open();
                    conn1.Close();
                    Kasir mm = new Kasir(conn1);
                    mm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    conn1.Close();
                }
            }
            else if (tb_username.Text=="list")
            {
                ListPesanan a = new ListPesanan();
                a.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Failed to Login");
            }
            
        }

        //nyoba bisa manggil keyboard tp masi ga bisa
        private Process _p = null;
        private void cmdToggle_Click(object sender, RoutedEventArgs e)
        {
            if (_p == null)
            {
                ProcessStartInfo p = new ProcessStartInfo(((Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\osk.exe")));
                try
                {
                    _p = Process.Start("osk");
                }
                catch (Exception)
                {
                    try
                    {
                        p.UseShellExecute = true;
                        _p = Process.Start(p);
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Failed to open on screen keyboard");
                        Console.WriteLine("Failed to open on screen keyboard");
                    }
                }
            }
            else
            {
                _p.Kill();
                _p.Dispose();
                _p = null;
            }
        }

    }
}
