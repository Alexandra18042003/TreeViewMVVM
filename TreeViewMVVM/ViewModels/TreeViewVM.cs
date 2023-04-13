using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TreeViewMVVM.Commands;

namespace TreeViewMVVM
{
    public enum Priority
    {
        High,
        Medium,
        Low
    };

    public class TreeViewVM : BaseVM
    {
        public RelayCommand AddToDoList { get; set; }
        public RelayCommand AddSubToDo { get; set; }
        private TDL _selectedTDL;
        public TDL SelectedTDL { get { return _selectedTDL; } set { _selectedTDL = value; OnPropertyChanged(); } }
        public ObservableCollection<TDL> ItemsCollection { get; set; }
        public ObservableCollection<Priority> Priorities { get; set; }
        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories { get { return _categories; } set { _categories = value; OnPropertyChanged(); } }
        public TreeViewVM()
        {
            Categories = new ObservableCollection<string>()
            {
            "School", "Cook", "Home"
            };
            Priorities = new ObservableCollection<Priority>(Enum.GetValues(typeof(Priority)).Cast<Priority>());
            ItemsCollection = new ObservableCollection<TDL>();
            TDL tdl = new TDL("Scoala");
            tdl.SubTDLs.Add(new TDL("Valentina"));
            tdl.SubTDLs[0].SubTasks.Add(new Task("Rc", "tema1", Priority.Low, Categories[0], DateTime.Now));
            tdl.SubTDLs[0].SubTasks.Add(new Task("Rc", "tema2", Priority.High, Categories[0]));
            tdl.SubTDLs.Add(new TDL("Georgeta"));
            tdl.SubTDLs[1].SubTasks.Add(new Task("Rc", "tema3", Priority.Low, Categories[0]));
            tdl.SubTDLs[1].SubTasks.Add(new Task("Ac", "tema4", Priority.Medium, Categories[0]));
            tdl.SubTDLs[1].SubTasks.Add(new Task("Cc", "tema4", Priority.Medium, Categories[0]));

            ItemsCollection.Add(tdl);

            AddToDoList = new RelayCommand(o => AddTDL());
            AddSubToDo = new RelayCommand(o => AddSubTDL());
        }

        public void AddTDL()
        {
            var newItem = new TDL(Text);
            ItemsCollection.Add(newItem);
            Text = String.Empty;
        }
        public void AddSubTDL()
        {
            foreach (var item in ItemsCollection)
            {
                if (item.TDLName == SelectedTDL.TDLName)
                {
                    item.SubTDLs.Add(new TDL(Text));
                    Text = String.Empty;
                    break;
                }
            }
        }

        public bool checkMainTDL(TDL tdl)
        {
            foreach(var item in ItemsCollection)
            {
                if (item.TDLName == tdl.TDLName)
                    return true;
            }
            return false;
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }
}
