using EnvDTE;
using EnvDTE80;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace PauseTest
{
    public class WindowLogger
    {
        private DTE2 dte;
        private Window nowActiveWindow;
        private Window lastActiveWindow;
        private DateTime startTime;
        private DateTime windowOpened;
        private TimeSpan timeHelper;

        public ObservableCollection<WindowData> data
        {
            get { return data;}
            private set{ data = value;}
        }


        public WindowLogger()
        {
            //Keine Ahnung warum man diesen String angeben muss aber offenbar verweist version 15 auf Visual Studio 2015
            dte = Marshal.GetActiveObject("VisualStudio.DTE.14.0") as DTE2;
            dte.Events.WindowEvents.WindowActivated += WindowListener;

            data = new ObservableCollection<WindowData>();
            startTime = windowOpened = DateTime.Now;

        }

        private void WindowListener(Window GotFocus, Window LostFocus)
        {
            nowActiveWindow = GotFocus;
            lastActiveWindow = LostFocus;

            timeHelper = DateTime.Now - windowOpened;
            windowOpened = DateTime.Now;

            if(data.Any(x => x.window.Equals(lastActiveWindow)))
            {
                data.Where(x => x.window.Equals(lastActiveWindow)).FirstOrDefault().add(timeHelper);
            }
            else
            {
                data.Add(new WindowData(lastActiveWindow, timeHelper));
            }
            

        }

    }
}
