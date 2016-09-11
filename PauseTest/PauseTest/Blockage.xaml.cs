using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PauseTest
{
    /// <summary>
    /// Interaction logic for Blockage.xaml
    /// </summary>
    public partial class Blockage : UserControl
    {

        public Timer daBlockTimer;
        int secCounter;
        Window disWindow;
        public Blockage(int daCountdownTime, Window dis)
        {
            InitializeComponent();

            disWindow = dis;
            dis.Closing += new System.ComponentModel.CancelEventHandler(CloseWindowLogic);
            secCounter = daCountdownTime;
            daBlockTimer = new Timer(1000); //  every second 
            daBlockTimer.Elapsed += new ElapsedEventHandler(TimerElapse);
            daBlockTimer.Enabled = true;
            daBlockTimer.AutoReset = true;
            //rest.Background = new SolidColorBrush(Color.FromArgb(100, 255, 100, 100));

        }

        public void TimerElapse(object sender, ElapsedEventArgs e)
        {
            secCounter -= 1;
            //rest.Content = secCounter+" seconds left";
            if (secCounter == 0)
            {
                daBlockTimer.Enabled = false;
                //rest.Background = new SolidColorBrush(Color.FromArgb(100, 100, 255, 100));

            }
        }

        public void CloseWindowLogic(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (secCounter <= 0)
                e.Cancel = false;
            else e.Cancel = true;
        }

        private void weiter_Click(object sender, RoutedEventArgs e)
        {
            disWindow.Close();
        }

        private void weiter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (secCounter >= 0)
                weiter.Focus();
        }
    }

}
