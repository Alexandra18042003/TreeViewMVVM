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
    internal class Task: BaseVM
    {
        private string name;
        private string description;
        private string status;
        private Priority priority;
        private string category;
        private DateTime deadline;



        Task(string m_name, string m_description, string m_status, Priority m_priority, DateTime m_deadline, string m_category)
        {
            name = m_name;
            description= m_description;
            status = m_status;
            priority = m_priority;    
            deadline = m_deadline;
            category = m_category;
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("ItemName");
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("ItemDescription");
            }
        } 
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("ItemStatus");
            }
        } 
        public DateTime Deadline
        {
            get
            {
                return deadline;
            }
            set
            {
                deadline = value;
                NotifyPropertyChanged("ItemDeadline");
            }
        }
        public string Category
        {
            get
            {
                return category;
            }
           
        }
    }
}
