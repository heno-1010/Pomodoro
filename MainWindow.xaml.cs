using System;
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
using System.Windows.Threading;


namespace Pomodoro
{
    public partial class MainWindow : Window
    {
        private int Worktime = 25 * 60;
        private int Breaktime = 10 * 60;
        private int Remainingtime;
        private bool Workmode = true; // trueなら作業, falseなら休憩
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            StartTimer();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if(Remainingtime <= 0)
            {
                timer.Stop();
                HandleEndOfSession();
                return;
            }
            Remainingtime--;
            Timer.Text = Timer_TextChanged(Remainingtime);
        }

        private void HandleEndOfSession()
        {
            string message = Workmode ? "作業終了。休憩をしますか？" : "休憩終了。作業をしますか？";
            string title = Workmode ? "作業終了" : "休憩終了";

            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Workmode = !Workmode;
                StartTimer();
            }
            else
            {
                Timer.Text = "25:00";
                this.Title = "ポモドーロタイマー";
                StartButton.IsEnabled = true;
            }
        }

        private string Timer_TextChanged(int Remainingtime)
        {
            return Remainingtime / 60 + ":" + Remainingtime % 60;
        }
        private async void StartTimer()
        {
            timer.Start();
            this.Title = Workmode ? "作業中" : "休憩中";
            Remainingtime = Workmode ? Worktime : Breaktime;
            Timer.Text = Timer_TextChanged(Remainingtime);
        }
        private void SaveTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(WorkTimeInput.Text, out int worktimeText) && int.TryParse(BreakTimeInput.Text, out int breaktimeText))
            {
                    Worktime = worktimeText * 60;
                    Breaktime = breaktimeText * 60;
                    if (Workmode)
                    {
                        Timer.Text = Worktime / 60 + ":" + Worktime % 60 + 0;
                    }
                    else
                    {
                        Timer.Text = Breaktime / 60 + ":" + Breaktime % 60 + 0;
                    }
                }
            }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                SwitchButton.Content = "スタート";
            }
            else
            {
                timer.Start();
                SwitchButton.Content = "ストップ";
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Timer.Text = Worktime / 60 + ":" + Worktime % 60 + 0;
            this.Title = "ポモドーロタイマー";
            StartButton.IsEnabled = true;
        }
    }
    }