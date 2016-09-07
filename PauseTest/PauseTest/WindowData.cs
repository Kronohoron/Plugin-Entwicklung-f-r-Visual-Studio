using EnvDTE;
using System.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauseTest
{
    class WindowData
    {
        public TimeSpan time { get; private set; }

        public Window window { get; private set; }

        public WindowData(Window w, TimeSpan t)
        {
            window = w;
            time = t;
        }

        public void add (TimeSpan t)
        {
            time += t;
        }
    }
}
