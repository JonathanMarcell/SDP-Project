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
        //ganti pny kalian, add reference oracle.dataAccess nya juga beda directory

        //new OracleConnection("Data Source =  ; User Id =  ; Password = ");

        //new OracleConnection("Data Source =  ; User Id =  ; Password = ");

        //new OracleConnection("Data Source =  ; User Id =  ; Password = ");

        new OracleConnection("Data Source = orcl ; User Id = jonathan ; Password = jonathan");

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
