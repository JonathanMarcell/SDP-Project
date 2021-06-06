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
    public partial class Admin_History_Chart : Window
    {
        int day, month, year;
        string mode="Tahunan";

        public Admin_History_Chart()
        {
            InitializeComponent();
            cb_mode.Items.Add("Tahunan");
            cb_mode.Items.Add("Bulanan");
            cb_mode.Items.Add("Mingguan");
            cb_mode.Items.Add("Harian");
            cb_mode.SelectedIndex =0;
            setCalendar2(false);
            setDateFilter(false);
            setYearFilter(true);
            datepicker1.SelectedDate = DateTime.Now;
            datepicker2.SelectedDate = DateTime.Now;
            Calendar2.SelectedDates.Add( DateTime.Now);
            tb_tahun.Text = DateTime.Now.Year + "";
            tb_tahun2.Text = DateTime.Now.Year + "";
        }

        
        private void cb_mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mode = cb_mode.SelectedValue.ToString();
            if (mode=="Tahunan")
            {
                setCalendar2(false);
                setDateFilter(false);
                setYearFilter(true);
            }
            else if (mode == "Bulanan")
            {
                setYearFilter(true);
                tb_tahun2.Visibility = Visibility.Hidden;
                setDateFilter(false);
                setCalendar2(false);
            }
            else if (mode == "Mingguan")
            {
                setYearFilter(false);
                setDateFilter(false);
                setCalendar2(true);

            }
            else if (mode == "Harian")
            {
                setYearFilter(false);
                setCalendar2(false);
                setDateFilter(true);
            }
        }

        private void loadCR()
        {
            //mode -> harian , mingguan, bulan, tahun
            if (mode == "Tahunan")
            {
                try
                {
                    year = Convert.ToInt32(tb_tahun.Text);
                    int year2 = Convert.ToInt32(tb_tahun2.Text);

                    Admin_History_Chart_CrystalReport rpt = new Admin_History_Chart_CrystalReport();
                    rpt.SetDatabaseLogon(App.userid, App.password, App.datasource, "");
                    rpt.SetParameterValue("StartYear", year);
                    rpt.SetParameterValue("EndYear", year2);
                    CRViewerChart.ViewerCore.ReportSource = rpt;
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Tahun salah!");
                }
            }
            else if (mode == "Bulanan")
            {
                try
                {
                    year = Convert.ToInt32(tb_tahun.Text);
                    Admin_History_Chart_CrystalReport1 rpt = new Admin_History_Chart_CrystalReport1();
                    rpt.SetDatabaseLogon(App.userid, App.password, App.datasource, "");
                    rpt.SetParameterValue("tahunSpesifik", year);
                    CRViewerChart.ViewerCore.ReportSource = rpt;
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Tahun salah!");
                }
            }
            else if (mode == "Mingguan")
            {
                Admin_History_Chart_CrystalReport2 rpt = new Admin_History_Chart_CrystalReport2();
                rpt.SetDatabaseLogon(App.userid, App.password, App.datasource, "");
                rpt.SetParameterValue("bulan", month);
                rpt.SetParameterValue("tahun", year);
                CRViewerChart.ViewerCore.ReportSource = rpt;
            }
            else if (mode == "Harian")
            {
                Admin_History_Chart_CrystalReport3 rpt = new Admin_History_Chart_CrystalReport3();
                rpt.SetDatabaseLogon(App.userid, App.password, App.datasource, "");
                rpt.SetParameterValue("startdate", datepicker1.SelectedDate.Value.Date);
                rpt.SetParameterValue("enddate", datepicker2.SelectedDate.Value.Date);
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


        private void setDateFilter(bool flag)
        {
            if (!flag)
            {
                lb_chooseDates.Visibility = Visibility.Hidden;
                lb_from.Visibility = Visibility.Hidden;
                lb_to.Visibility = Visibility.Hidden;
                datepicker1.Visibility = Visibility.Hidden;
                datepicker2.Visibility = Visibility.Hidden;
            }
            else
            {
                lb_chooseDates.Visibility = Visibility.Visible;
                lb_from.Visibility = Visibility.Visible;
                lb_to.Visibility = Visibility.Visible;
                datepicker1.Visibility = Visibility.Visible;
                datepicker2.Visibility = Visibility.Visible;
            }
        }
        private void setYearFilter(bool flag)
        {
            if (!flag)
            {
                lb_chooseYear.Visibility = Visibility.Hidden;
                lb_from.Visibility = Visibility.Hidden;
                lb_to.Visibility = Visibility.Hidden;
                tb_tahun.Visibility = Visibility.Hidden;
                tb_tahun2.Visibility = Visibility.Hidden;
            }
            else
            {
                lb_chooseYear.Visibility = Visibility.Visible;
                lb_from.Visibility = Visibility.Visible;
                lb_to.Visibility = Visibility.Visible;
                tb_tahun.Visibility = Visibility.Visible;
                tb_tahun2.Visibility = Visibility.Visible;
            }
        }

        private void setCalendar2(bool flag)
        {
            if (!flag)
            {
                lb_chooseMonth.Visibility = Visibility.Hidden;
                Calendar2.Visibility = Visibility.Hidden;
            }
            else
            {
                lb_chooseMonth.Visibility = Visibility.Visible;
                Calendar2.Visibility = Visibility.Visible;
            }
        }

        private void Calendar2_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if (Calendar2.DisplayMode == CalendarMode.Month)
            {
                month = Calendar2.DisplayDate.Month;
                year = Calendar2.DisplayDate.Year;
                Calendar2.DisplayMode = CalendarMode.Year;
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
}
}
