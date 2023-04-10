using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewMVVM
{
    internal class TreeViewVM : BaseVM
    {
        public TreeViewVM()
        {
            ItemsCollection = new ObservableCollection<MyItem>();
            ItemsCollection.Add(new MyItem
            {
                ItemName = "a",
                SubCollection = new ObservableCollection<MyItem>()
                {
                    new MyItem { ItemName = "b", SubCollection = new ObservableCollection<MyItem>()
                    {
                        new MyItem() { ItemName = "d", SubCollection = new ObservableCollection<MyItem>() },
                        new MyItem() { ItemName = "e", SubCollection = new ObservableCollection<MyItem>() }
                    }
                    },
                    new MyItem { ItemName = "c", SubCollection = new ObservableCollection<MyItem>()
                    {
                        new MyItem() { ItemName = "f", SubCollection = new ObservableCollection<MyItem>() },
                        new MyItem() { ItemName = "g", SubCollection = new ObservableCollection<MyItem>() }
                    }
                    }
                }
            });
            ItemsCollection.Add(new MyItem()
            {
                ItemName = "h",
                SubCollection = new ObservableCollection<MyItem>()
                {
                    new MyItem { ItemName = "i", SubCollection = new ObservableCollection<MyItem>()
                    {
                        new MyItem() { ItemName = "j", SubCollection = new ObservableCollection<MyItem>() }
                    }
                    }
                }
            });
        }
        public ObservableCollection<MyItem> ItemsCollection { get; set; }
        private MyItem selectedItem;
        public MyItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }
    }
}
