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
using System.Text.RegularExpressions;

namespace ELREORS
{
    /// <summary>
    /// Interaction logic for Admin_History_Chart.xaml
    /// </summary>
    public partial class Admin_History_Report : Window
    {

        public Admin_History_Report()
        {
            InitializeComponent();
            datepicker1.SelectedDate = DateTime.Now;
            datepicker2.SelectedDate = DateTime.Now;
        }


        private void loadCR()
        {
            if (cb_pembatas.IsChecked == true )
            {
                Admin_History_Report_CR rpt = new Admin_History_Report_CR();
                rpt.SetDatabaseLogon(App.userid, App.password, App.datasource, "");
                rpt.SetParameterValue("startdate", datepicker1.SelectedDate.Value.Date);
                rpt.SetParameterValue("enddate", datepicker2.SelectedDate.Value.Date);
                CRViewerChart.ViewerCore.ReportSource = rpt;
            }
            else
            {
                Admin_History_Report_CR rpt = new Admin_History_Report_CR();
                rpt.SetDatabaseLogon(App.userid, App.password, App.datasource, "");
                rpt.SetParameterValue("startdate", new DateTime(0001,1,1).Date );
                rpt.SetParameterValue("enddate", DateTime.Now.Date);
                CRViewerChart.ViewerCore.ReportSource = rpt;
            }
        }

        private void btn_reload_Click(object sender, RoutedEventArgs e)
        {
            loadCR();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CRViewerChart.Owner = Window.GetWindow(this);
            loadCR();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
}
}
