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


namespace Pomodoro
{
    public partial class MainWindow : Window
    {
        private int Worktime = 1 * 60;
        private int Breaktime = 1 * 60;
        private int Remainingtime;
        private bool _isTimerRunning = false; // タイマーが動作中かどうかを示す
        private bool Workmode = true; // trueなら作業, falseなら休憩

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isTimerRunning)
            {
                StartTimer();
                StartButton.IsEnabled = false;
            }
        }

        private async Task TimerTick()
        {
            _isTimerRunning = true;
            while(Remainingtime > 0)
            {
                await Task.Delay(1000);
                Remainingtime--;
                Timer.Text = Timer_TextChanged(Remainingtime);
            }
            HandleEndOfSession();
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
                _isTimerRunning = false;
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
            this.Title = Workmode ? "作業中" : "休憩中";
            if(Workmode == true)
            {
                Remainingtime = Worktime;
                await TimerTick();
            }
            else
            {
                Remainingtime = Breaktime;
                await TimerTick();
            }
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
        }
    }