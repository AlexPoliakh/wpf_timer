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
using System.Windows.Threading;

namespace WpfTimer
{
 
    public partial class MainWindow : Window
    {
        private int seconds = 0;
        private int minutes = 0;
        private int hours = 0;

        private DispatcherTimer Timer;

        public MainWindow()
        {
            InitializeComponent();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0,0,1);
            Timer.Tick += Timer_Tick;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (hours >= 0)
            {
                if (hours >= 0 && minutes >= 0)
                {
                    if (hours >=0 && minutes >= 0 && seconds > 0)
                    {
                        seconds--;
                    }
                    else if (hours >= 0 && minutes > 0 && seconds == 0)
                    {
                        minutes--;
                        seconds = 59;
                    }
                    else if (hours > 0 && minutes == 0 && seconds == 0)
                    {
                        hours--;
                        minutes = 59;
                        seconds = 59;
                    }
                    else if (hours == 0 && minutes == 0 && seconds == 0)
                    {
                        TimerEnd();
                        ShowInput();
                    }
                }
            }
            DrawTime();
        }
        
        private void TimerEnd()
        {
            Timer.Stop();
            Topmost = true;// show messagebox over the all windows
            MessageBox.Show(this, "Take a rest for while","Timer ends", MessageBoxButton.OK, MessageBoxImage.Information);
            WindowState = WindowState.Normal;// if timer is minimized - set it to normal state
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            //cathcing error - if users input is not a number
            try
            {
                seconds = int.Parse(input_seconds.Text);
                minutes = int.Parse(input_minutes.Text);
                hours = int.Parse(input_hours.Text);
 
                if (hours < 24 && minutes < 60 && seconds < 60)
                {
                    Timer.Start();
                    HideInput();
                }
                else { MessageBox.Show(this, "неверно введенно время", "", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            catch (FormatException) { MessageBox.Show(this,"введенное значение должно быть числом","",MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void HideInput()
        {
            input_seconds.Visibility = Visibility.Hidden;
            input_minutes.Visibility = Visibility.Hidden;
            input_hours.Visibility = Visibility.Hidden;
        }
        private void ShowInput()
        {
            input_seconds.Visibility = Visibility.Visible;
            input_minutes.Visibility = Visibility.Visible;
            input_hours.Visibility = Visibility.Visible;
        }

        private void DrawTime()
        {
            textBlocksecond.Text = string.Format("{0:00}", seconds);
            textBlockminute.Text = string.Format("{0:00}", minutes);
            textBlockhour.Text = string.Format("{0:00}", hours);

        }

        private void reset_button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowInput();
            //set values in input fields to 0
            input_seconds.Text = "0";
            input_minutes.Text = "0";
            input_hours.Text = "0";
            
            Timer.Stop();
            //set timers value to 0
            seconds = 0;
            minutes = 0;
            hours = 0;

            DrawTime();
        }
    }
}
