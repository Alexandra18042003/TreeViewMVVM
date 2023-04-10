using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TreeViewMVVM
{
    internal class TDL : BaseVM
    {
        public TDL() 
        { 
            SubTDLs = new ObservableCollection<TDL>();
            SubTasks = new ObservableCollection<Task>();
        }
        public ObservableCollection<TDL> SubTDLs { get; set; }
        public ObservableCollection<Task> SubTasks { get; set; }

        private string m_TDLName;
        public string TDLName
        {
            get
            {
                return m_TDLName;
            }
            set
            {
                m_TDLName = value;
                NotifyPropertyChanged("TDLName");
            }
        }
    }
}
