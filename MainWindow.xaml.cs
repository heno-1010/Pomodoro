using Microsoft.Toolkit.Uwp.Notifications;
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
        public int Worktime = 25 * 60;
        public int Breaktime = 10 * 60;
        private int Remainingtime;
        public bool Workmode = true; // trueなら作業, falseなら休憩
        private DispatcherTimer timer;
        private int Countpomodoro = 0;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            SwitchButton.IsEnabled = false;
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
            this.Topmost = true;
            string message = Workmode ? "作業終了。休憩をしますか？" : "休憩終了。作業をしますか？";
            string title = Workmode ? "作業終了" : "休憩終了";
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            if (!Workmode)
            {
                Countpomodoro++;
                CountPomodoro.Text = "ポモドーロ数: " + Countpomodoro;
            }
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
            int minutes = Remainingtime / 60;
            int seconds = Remainingtime % 60;

            return string.Format("{0}:{1:D2}", minutes, seconds);
        }
        private async void StartTimer()
        {
            timer.Start();
            this.Title = Workmode ? "作業中" : "休憩中";
            Remainingtime = Workmode ? Worktime : Breaktime;
            ModeText.Text = Workmode ? "作業中" : "休憩中";
            SwitchButton.IsEnabled = true;
            Timer.Text = Timer_TextChanged(Remainingtime);
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
            SwitchButton.Content = "ストップ";
            SwitchButton.IsEnabled = false;
            StartButton.IsEnabled = true;
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingFrame.Visibility = SettingFrame.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            if (SettingFrame.Content is Settings)
            {
                SettingFrame.Navigate(null);
                return;
            }
            else
            {
                Settings settingsPage = new Settings(this);
                SettingFrame.Navigate(settingsPage);
            }

        }
    }
    }