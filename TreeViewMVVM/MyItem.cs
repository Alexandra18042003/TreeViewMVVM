using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TreeViewMVVM
{
    internal class MyItem : BaseVM
    {
        public MyItem() 
        { 
            SubCollection = new ObservableCollection<MyItem>();
        }
        public ObservableCollection<MyItem> SubCollection { get; set; }
        private string itemName;
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
                NotifyPropertyChanged("ItemName");
            }
        }
    }
}
