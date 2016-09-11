using EnvDTE;
using System.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PauseTest
{
    public class WindowData : INotifyPropertyChanged
    {
        private TimeSpan _time;
        public TimeSpan time {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                Notify("time");
            }
        }

        private Window _window;
        public Window window {
            get
            {
                return _window;
            }
            set
            {
                _window = value;
                Notify("window"); //just in Case. The Window is never changed once created.
            }
        }

        public WindowData(Window w, TimeSpan t)
        {
            window = w;
            time = t;

            //Notify("window");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void add (TimeSpan t)
        {
            time += t;
        }

        private void Notify (string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); //Intellisense made this syntax. Aparently it checks wether PropertyChanged is null an if it is not invokes it.
        }
    }
}
