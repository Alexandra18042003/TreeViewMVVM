using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewMVVM
{
    enum Priority
    {
        High,
        Medium,
        Low
    };

    internal class TreeViewVM : BaseVM
    {
        public TreeViewVM()
        {
            ItemsCollection = new ObservableCollection<TDL>();
            ItemsCollection.Add(new TDL
            {
                ItemName = "a",
                SubCollection = new ObservableCollection<TDL>()
                {
                    new TDL { ItemName = "b", SubCollection = new ObservableCollection<TDL>()
                    {
                        new TDL() { ItemName = "d", SubCollection = new ObservableCollection<TDL>() },
                        new TDL() { ItemName = "e", SubCollection = new ObservableCollection<TDL>() }
                    }
                    },
                    new TDL { ItemName = "c", SubCollection = new ObservableCollection<TDL>()
                    {
                        new TDL() { ItemName = "f", SubCollection = new ObservableCollection<TDL>() },
                        new TDL() { ItemName = "g", SubCollection = new ObservableCollection<TDL>() }
                    }
                    }
                }
            });
            ItemsCollection.Add(new TDL()
            {
                ItemName = "h",
                SubCollection = new ObservableCollection<TDL>()
                {
                    new TDL { ItemName = "i", SubCollection = new ObservableCollection<TDL>()
                    {
                        new TDL() { ItemName = "j", SubCollection = new ObservableCollection<TDL>() }
                    }
                    }
                }
            });
        }
        public ObservableCollection<TDL> ItemsCollection { get; set; }
        private TDL selectedItem;
        List<string> categories = new List<string>()
        {
            "School", "Cook", "Home"
        };

        public TDL SelectedItem
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
