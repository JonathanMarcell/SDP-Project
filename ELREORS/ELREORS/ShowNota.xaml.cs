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
        private int nomeja, idpegawai;
        public ShowNota(int nomeja, string idpegawai)
        {
            InitializeComponent();
            App.openconn();
            conn = App.conn;
            this.nomeja = nomeja;
            this.idpegawai = Convert.ToInt32(idpegawai);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NotaViewer.Owner = Window.GetWindow(this);
            Nota nota = new Nota();
            nota.SetDatabaseLogon(App.userid, App.password, App.datasource, "");
            nota.SetParameterValue("noMeja", nomeja);
            nota.SetParameterValue("idPegawai", idpegawai);
            nota.SetParameterValue("tglNow", DateTime.Now.ToShortDateString());
            NotaViewer.ViewerCore.ReportSource = nota;
        }
    }
}
