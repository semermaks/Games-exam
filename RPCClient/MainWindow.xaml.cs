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

namespace RPCClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button btn1 = new Button();
        private Button btn2 = new Button();

        public MainWindow()
        {
            InitializeComponent();
            AddBtns();
        }

        private void AddBtns()
        {
            btn1 = new Button();
            btn1.Margin = new Thickness(10, 117, 0, 0);
            btn1.Height = 334;
            btn1.Width = 375;
            btn1.VerticalAlignment = VerticalAlignment.Top;
            btn1.HorizontalAlignment = HorizontalAlignment.Left;
            btn1.Opacity = 0.5;
            btn1.Click += Button_Click;
            btn1.Name = "btnPc";

            btn2 = new Button();
            btn2.Margin = new Thickness(400, 117, 0, 0);
            btn2.Height = 333;
            btn2.Width = 400;
            btn2.VerticalAlignment = VerticalAlignment.Top;
            btn2.HorizontalAlignment = HorizontalAlignment.Left;
            btn2.Opacity = 0.5;
            btn2.Click += Button_Click_1;
            btn2.Name = "btnKeb";


            Grid.Children.Add(btn1);
            Grid.Children.Add(btn2);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameClient mc = new GameClient(false);
            mc.Show();
            Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GameClient mc = new GameClient(true);
            mc.Show();
            Close();
        }
    }
}
