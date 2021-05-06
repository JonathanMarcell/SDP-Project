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
    /// Interaction logic for Admin_History_Chart.xaml
    /// </summary>
    public partial class Admin_History_Chart : Window
    {
        public Admin_History_Chart()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CRViewerChart.Owner = Window.GetWindow(this);
            Admin_History_Chart_CrystalReport rpt = new Admin_History_Chart_CrystalReport();
            rpt.SetDatabaseLogon(App.userid, App.password, App.datasource, "");

            //rpt.SetParameterValue("paramKategori", kategori.SelectedVal);

            CRViewerChart.ViewerCore.ReportSource = rpt;
        }
    }
}
