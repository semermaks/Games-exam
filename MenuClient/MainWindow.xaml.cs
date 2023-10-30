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

namespace MenuClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsPerson { get; set; }
        Button Rock = new Button();
        Button Papper = new Button();
        Button Scissors = new Button();
        public MainWindow(bool IsPerson)
        {
            InitializeComponent();
            this.IsPerson = IsPerson;
            AddBtns();
        }

        private void AddBtns()
        {
            Rock.HorizontalAlignment = HorizontalAlignment.Left;
            Rock.Margin = new Thickness(36, 115, 0, 0);
            Rock.VerticalAlignment = VerticalAlignment.Top;
            Rock.Height = 256;
            Rock.Width = 251;
            Rock.Opacity = 0.5;

            Papper.HorizontalAlignment = HorizontalAlignment.Center;
            Papper.Margin = new Thickness(0, 115, 20, 0);
            Papper.VerticalAlignment = VerticalAlignment.Top;
            Papper.Height = 256;
            Papper.Width = 251;
            Papper.Opacity = 0.5;

            Scissors.HorizontalAlignment = HorizontalAlignment.Right;
            Scissors.Margin = new Thickness(0, 115, 54, 0);
            Scissors.VerticalAlignment = VerticalAlignment.Top;
            Scissors.Height = 256;
            Scissors.Width = 251;
            Scissors.Opacity = 0.5;

            Grid.Children.Add(Rock);
            Grid.Children.Add(Papper);
            Grid.Children.Add(Scissors);
            
        }
    }
}
