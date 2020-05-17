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


namespace WPF_TRY
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public FontFamily Resources { get; set; }

        public MainWindow()
        {
            //FontFamily fontFamily = new FontFamily("Calibri");
            //Resources.Add("myfont", fontFamily);
            InitializeComponent();

        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Window_Click");

        }

        private void ChangeFont_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Resources["CustomFont"].ToString());
            this.DataContext = Resources["CustomFont"];
            Resources["CustomFont"] = "Arial";
            MessageBox.Show(Resources["CustomFont"].ToString());

        }
    }
}
