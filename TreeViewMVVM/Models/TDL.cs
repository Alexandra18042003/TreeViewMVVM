using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TreeViewMVVM
{
    public class TDL : BaseVM
    {
        public TDL(string name) 
        { 
            m_TDLName = name;
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

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    NotifyPropertyChanged(nameof(IsSelected));
                }
            }
        }
    }
}
