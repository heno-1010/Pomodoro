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
        private int Settingtime = 25 * 60;
        private int Remainingtime;
        private bool _isTimerRunning = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(_isTimerRunning == true)
            {
                return;
            }
            Remainingtime = Settingtime;
            await TimerTick();
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
            MessageBox.Show("ポモドーロ終了");
            _isTimerRunning = false;
        }

        private string Timer_TextChanged(int Remainingtime)
        {
            return Remainingtime / 60 + ":" + Remainingtime % 60;
        }
    }
}