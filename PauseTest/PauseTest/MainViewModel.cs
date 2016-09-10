using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauseTest
{
    class MainViewModel
    {

        public ShowCurrentWindow listData = new ShowCurrentWindow();
        private readonly ObservableCollection<WindowData> _data = new ObservableCollection<WindowData>();
        public ObservableCollection<WindowData> Populations
        {
            get
            {
                return _data;
            }
        }

        public MainViewModel()
        {
            foreach(ObservableCollection)
            _data = listData.getData();

            
        }
    }
}
