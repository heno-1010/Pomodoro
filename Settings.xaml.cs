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

namespace Pomodoro
{
    /// <summary>
    /// Settings.xaml の相互作用ロジック
    /// </summary>
    public partial class Settings : Page
    {
        private MainWindow _mainwindow;
        public Settings(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainwindow = mainWindow;
            UpdateSetting.Visibility = Visibility.Collapsed;
        }

        private async void  SaveTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(WorkTimeInput.Text, out int worktimeText) && int.TryParse(BreakTimeInput.Text, out int breaktimeText))
            {
                _mainwindow.Worktime = worktimeText * 60;
                _mainwindow.Breaktime = breaktimeText * 60;
                if (_mainwindow.Workmode)
                {
                    _mainwindow.TimerDisplay = $"{worktimeText}:00";
                }
                else
                {
                    _mainwindow.TimerDisplay = $"{breaktimeText}:00";
                }
            }
            UpdateSetting.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            UpdateSetting.Visibility = Visibility.Collapsed;
        }
    }
}
