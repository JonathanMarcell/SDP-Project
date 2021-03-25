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
    /// Interaction logic for Admin_History.xaml
    /// </summary>
    public partial class Admin_History : Page
    {
        OracleConnection conn;
        DataTable dt;
        public Admin_History()
        {
            InitializeComponent();
            conn = App.conn;
            loaddata();
        }

        public void loaddata()
        {
            OracleDataAdapter da = new OracleDataAdapter("select * from hjual", conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
        }

    }
}
