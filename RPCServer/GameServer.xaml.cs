using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Windows.Threading;

namespace RPCServer
{
    /// <summary>
    /// Interaction logic for GameServer.xaml
    /// </summary>
    public partial class GameServer : Window
    {
        int Rounds = 0;
        string let;
        UdpClient udpClient = null;
        public bool IsPerson { get; set; }
        Button Rock = new Button();
        Button Papper = new Button();
        Button Scissors = new Button();
        public GameServer(bool IsPerson)
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
            Rock.Background = null;
            Rock.BorderThickness = new Thickness(20);
            Rock.Click += Button_Click_R;

            Papper.HorizontalAlignment = HorizontalAlignment.Center;
            Papper.Margin = new Thickness(0, 115, 20, 0);
            Papper.VerticalAlignment = VerticalAlignment.Top;
            Papper.Height = 256;
            Papper.Width = 251;
            Papper.Opacity = 0.5;
            Papper.Background = null;
            Papper.BorderThickness = new Thickness(20);
            Papper.Click += Button_Click_P;

            Scissors.HorizontalAlignment = HorizontalAlignment.Right;
            Scissors.Margin = new Thickness(0, 115, 54, 0);
            Scissors.VerticalAlignment = VerticalAlignment.Top;
            Scissors.Height = 256;
            Scissors.Width = 251;
            Scissors.Opacity = 0.5;
            Scissors.Background = null;
            Scissors.BorderThickness = new Thickness(20);
            Scissors.Click += Button_Click_S;

            Grid.Children.Add(Rock);
            Grid.Children.Add(Papper);
            Grid.Children.Add(Scissors);

            if (!IsPerson)
            {
                lblState.Content = "Машина делает выбор";
                Rock.IsEnabled = false;
                Papper.IsEnabled = false;
                Scissors.IsEnabled = false;

                Rock.Opacity = 0;
                Papper.Opacity = 0;
                Scissors.Opacity = 0;

                Rock.Background = Brushes.Green;
                Papper.Background = Brushes.Green;
                Scissors.Background = Brushes.Green;

            }
        }
        private void CheckRounds()
        {
            bool? IWin = ScoreToRes();

            if (Rounds == 5)
            {
                if (IWin == null)
                {
                    MessageBox.Show("Ничья!!");
                    Close();
                }
                else if (IWin == true)
                {
                    MessageBox.Show("Вы выиграли!!");
                    Close();
                }
                else if (IWin == false)
                {
                    MessageBox.Show("Вы проиграли!!");
                    Close();
                }
            }
        }

        private void Button_Click_R(object sender, RoutedEventArgs e)
        {
            Rounds++;
            ShowResult(SendChoose("R"));
            CheckRounds();
        }
        private void Button_Click_P(object sender, RoutedEventArgs e)
        {
            Rounds++;
            ShowResult(SendChoose("P"));
            CheckRounds();
        }
        private void Button_Click_S(object sender, RoutedEventArgs e)
        {
            Rounds++;
            ShowResult(SendChoose("S"));
            CheckRounds();
        }

        private bool? ScoreToRes()
        {
            int MySc = int.Parse(lblMyStore.Content.ToString());
            int EnSc = int.Parse(lblEnemyScore.Content.ToString());
            if (MySc > EnSc) return true;
            else if (MySc < EnSc) return false;
            else return null;
        }

        private void ShowResult(bool? v)
        {

            if (v == null)
            {
                MyScoreUp();
                EnemyScoreUp();
            }
            else if (v == true)
            {
                MyScoreUp();
            }
            else if (v == false)
            {
                EnemyScoreUp();
            }
        }

        private void MyScoreUp()
        {
            int num;
            num = int.Parse(lblMyStore.Content.ToString());
            num++;
            lblMyStore.Content = num;
        }

        private void EnemyScoreUp()
        {
            int num;
            num = int.Parse(lblEnemyScore.Content.ToString());
            num++;
            lblEnemyScore.Content = num;
        }



        private bool? SendChoose(string v)
        {
            bool? IWin = null;
            try
            {
                string HisChoose;
                byte[] message = new byte[1];

                IPEndPoint clientEndPoint = null;

                byte[] bytes = udpClient.Receive(ref clientEndPoint);

                HisChoose = Encoding.UTF8.GetString(bytes);

                switch (HisChoose)
                {
                    case "R":
                        if (v == "R") message[0] = 2;
                        else if (v == "P")
                        {
                            message[0] = 1;
                            IWin = false;
                        }
                        else if (v == "S")
                        {
                            message[0] = 0;
                            IWin = true;
                        }
                        break;
                    case "P":
                        if (v == "R")
                        {
                            message[0] = 0;
                            IWin = true;
                        }
                        else if (v == "P") message[0] = 2;
                        else if (v == "S")
                        {
                            message[0] = 1;
                            IWin = false;
                        }
                        break;
                    case "S":
                        if (v == "R")
                        {
                            message[0] = 0;
                            IWin = true;
                        }
                        else if (v == "P")
                        {
                            message[0] = 1;
                            IWin = false;
                        }
                        else if (v == "S") message[0] = 2;
                        break;
                }

                udpClient.Send(message, message.Length, clientEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return IWin;
        }

        public void BotStep()
        {
            Rounds++;
            ShowResult(SendChoose(let));
            CheckRounds();
        }

        public static void DelayAction(int millisecond, Action action)
        {
            var timer = new DispatcherTimer();
            timer.Tick += delegate

            {
                action.Invoke();
                timer.Stop();
            };

            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Start();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsPerson)
            {
                Random random = new Random();
                int rnd;
                for (int i = 0; i < 5; i++)
                {
                    rnd = random.Next(0, 4);
                    if (rnd == 0) let = "R";
                    else if (rnd == 1) let = "P";
                    else let = "S";
                    DelayAction(1000, BotStep);
                }
            }
            udpClient = new UdpClient(new IPEndPoint(IPAddress.Loopback, 52345));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            udpClient?.Close();
        }
    }
}
