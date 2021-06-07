using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Oracle.DataAccess.Client;
using System.Globalization;
using System.Threading;
using System.Windows.Markup;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static string datasource = "orcl";

        //kuubah supaya bisa diambil buat report

        public static string userid = "bryant"; public static string password = "bryant";

        //public static string userid = "sdp"; public static string password = "sdp";

        //public static string userid = "coba"; public static string password = "1";

        public static OracleConnection conn =
        new OracleConnection($"Data Source = {datasource} ; User Id = {userid} ; Password = {password}");

        public static void openconn()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static int getJumlahMeja()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("select JUMLAH_MEJA from PROFIL", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return 0;
        }
        public static string getNamaResto()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("select NAMA_RESTO from PROFIL", conn);
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return "";
        }
        public static string getAlamat()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("select ALAMAT from PROFIL", conn);
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return "";
        }
        public static string getTelepon()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("select TELEPON from PROFIL", conn);
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return "";
        }


        //ngubah locale jd indo
        protected override void OnStartup(StartupEventArgs e)
        {
            var vCulture = new CultureInfo("id-ID");

            Thread.CurrentThread.CurrentCulture = vCulture;
            Thread.CurrentThread.CurrentUICulture = vCulture;
            CultureInfo.DefaultThreadCurrentCulture = vCulture;
            CultureInfo.DefaultThreadCurrentUICulture = vCulture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(
            XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);
        }

    }
}
