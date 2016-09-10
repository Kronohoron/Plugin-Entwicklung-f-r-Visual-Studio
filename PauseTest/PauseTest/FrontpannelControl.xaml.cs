//------------------------------------------------------------------------------
// <copyright file="FrontpannelControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace PauseTest
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Timers;
    using System;
    /// <summary>
    /// Interaction logic for FrontpannelControl.
    /// </summary>
    public partial class FrontpannelControl : UserControl
    {


        


        Timer daTimer;
        DateTime activeT;
        DateTime lastCatch; //  this refers to activeT, not realT because we want to evaluate ov    er the active time, not real time
        bool isPaused;
        Evaluation evaData = new Evaluation();

        /// <summary>
        /// Initializes a new instance of the <see cref="FrontpannelControl"/> class.
        /// </summary>
        public FrontpannelControl()
        {
            
            InitializeComponent();
            this.DataContext = new MainViewModel();
            Button.Na

        }



        public void init()
        {
            activeT = DateTime.MinValue;
            lastCatch = DateTime.MinValue;
            daTimer = new Timer(1000);  //  every second
            daTimer.Elapsed += new ElapsedEventHandler(everySecond);
            isPaused = false;
        }
        public void everySecond(object sender, ElapsedEventArgs e)
        {
            activeT = activeT.AddSeconds(1);
            timerL.Content = lastCatch.ToString() + "_" + activeT.ToString();
        }

        public void clear()
        {
            daTimer.Dispose();
            listTS.Items.Clear();
            pauseTimer.IsEnabled = false;
        }

        public void pause()
        {
            daTimer.Enabled = false;
        }

        public void resume()
        {
            daTimer.Enabled = true;
        }

        public void addCatch()
        {
            string insertLB = "str1_str2_lastTS:" + lastCatch.ToString() + "_now:" + activeT.ToString() + "_diff:" + (activeT - lastCatch).ToString();
            lastCatch = activeT;
            listTS.Items.Add(insertLB);
        }

        private void startTimer_Click(object sender, RoutedEventArgs e)
        {
            init();
            resume();
            pauseTimer.IsEnabled = true;
        }

        private void pauseTimer_Click(object sender, RoutedEventArgs e)
        {
            if (isPaused)
            {
                isPaused = true;
                pause();
                pauseTimer.Content = "Resume Timer";
            }
            else
            {
                isPaused = false;
                resume();
                pauseTimer.Content = "Pause Timer";
            }
        }

        private void clearAll_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void addTS_Click(object sender, RoutedEventArgs e)
        {
            addCatch();
        }

    }
}