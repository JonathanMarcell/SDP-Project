using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Oracle.DataAccess.Client;
namespace ELREORS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static OracleConnection conn =

            //ini masukin user oracle , mending bikin user baru sih menurutku

        //new OracleConnection("Data Source =  ; User Id = bryant ; Password = ");

        new OracleConnection("Data Source =  ; User Id = sdp ; Password = sdp");

        //new OracleConnection("Data Source =  ; User Id = fernando ; Password = ");

        //new OracleConnection("Data Source = orcl ; User Id = jo2 ; Password = jo2");

        public static void openconn()
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
