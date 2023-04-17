using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TreeViewMVVM;

namespace TreeViewMVVM
{
    [Serializable]
    public class Task : BaseVM
    {
        private string name;
        private string description;
        private bool status;
        private Priority priority;
        private string category;
        private DateTime deadline;


        public Task(string m_name, Priority m_priority, string m_category, DateTime m_deadline = default)
        {
            name = m_name;
            description = "Add a description";
            status = false;
            priority = m_priority;
            deadline = m_deadline;
            category = m_category;
        }
        public string TaskName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string TaskDescription
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
        public bool TaskStatus
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public DateTime TaskDeadline
        {
            get
            {
                return deadline;
            }
            set
            {
                deadline = value;
                OnPropertyChanged("ItemDeadline");
            }
        }
        public string TaskCategory
        {
            get
            {
                return category;
            }

        }
        public string TaskPriority
        {
            get
            {
                return priority.ToString();
            }
        }
    }
}
