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
        }

        private void SaveTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(WorkTimeInput.Text, out int worktimeText) && int.TryParse(BreakTimeInput.Text, out int breaktimeText))
            {
                _mainwindow.Worktime = worktimeText * 60;
                _mainwindow.Breaktime = breaktimeText * 60;
                if (_mainwindow.Workmode)
                {
                    _mainwindow.Timer.Text = _mainwindow.Worktime / 60 + ":" + _mainwindow.Worktime % 60 + 0;
                }
                else
                {
                    _mainwindow.Timer.Text = _mainwindow.Breaktime / 60 + ":" + _mainwindow.Breaktime % 60 + 0;
                }
            }
        }
    }
}
