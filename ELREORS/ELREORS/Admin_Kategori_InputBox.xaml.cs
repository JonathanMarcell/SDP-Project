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
    /// Interaction logic for Admin_Kategori_InputBox.xaml
    /// </summary>
    public partial class Admin_Kategori_InputBox : Window
    {
        public string inputText { get {
                if (textbox1.Text == "") return string.Empty;
                return textbox1.Text;
            }
        }
        public Admin_Kategori_InputBox()
        {
            InitializeComponent();
        }
        public Admin_Kategori_InputBox(string text)
        {
            InitializeComponent();
            textbox1.Text = text;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            textbox1.Text = "";
            Close();
        }
    }
}
