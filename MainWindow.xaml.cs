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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int Settingtime = 1 * 60;
        private int Breaktime = 1 * 60;
        private int Remainingtime;
        private bool _isTimerRunning = false; // タイマーが動作中かどうかを示す
        private bool mode = false; // trueなら作業, falseなら休憩

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isTimerRunning == false)
            {
                StartWork();
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
            if(mode == true)
            {
                MessageBoxResult result = MessageBox.Show("作業終了。休憩をしますか？", "作業終了", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    StartBreak();
                }
                else
                {
                    _isTimerRunning = false;
                    Timer.Text = "25:00";
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("休憩終了。作業をしますか？", "休憩終了", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    StartWork();
                }
                else
                {
                    _isTimerRunning = false;
                    Timer.Text = "25:00";
                }
            }
        }

        private string Timer_TextChanged(int Remainingtime)
        {
            return Remainingtime / 60 + ":" + Remainingtime % 60;
        }
        private async void StartWork()
        {
            this.Title = "作業中";
            mode = true;
            Remainingtime = Settingtime;
            await TimerTick();
        }
        private async void StartBreak()
        {
            this.Title = "休憩中";
            mode = false;
            Remainingtime = Breaktime;
            await TimerTick();
        }
    }
}