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
    public class Task: BaseVM
    {
        private string name;
        private string description;
        private Priority priority;
        private string category;
        private DateTime deadline;


        public Task(string m_name, string m_description, Priority m_priority, string m_category, DateTime m_deadline = default)
        {
            name = m_name;
            description= m_description;
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
                OnPropertyChanged("ItemName");
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
                name = value;
                OnPropertyChanged("ItemDescription");
            }
        } 
        public string TaskStatus
        {
            get
            {
                return status;
            }
            set
            {
                name = value;
                OnPropertyChanged("ItemStatus");
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
