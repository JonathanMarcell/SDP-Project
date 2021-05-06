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

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for ShowNota.xaml
    /// </summary>
    public partial class ShowNota : Window
    {
        public static OracleConnection conn;
        public ShowNota()
        {
            InitializeComponent();
            App.openconn();
            conn = App.conn;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NotaViewer.Owner = Window.GetWindow(this);
            Nota nota = new Nota();
            nota.SetDatabaseLogon(App.userid, App.password, App.datasource, "");
            NotaViewer.ViewerCore.ReportSource = nota;
        }
    }
}
