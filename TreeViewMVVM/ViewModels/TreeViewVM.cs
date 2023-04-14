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
        public RelayCommand AddTask { get; set; }
        public RelayCommand OpenStats { get; set; }
        public RelayCommand Save { get; set; }
        public Manager Manager { get; set; }

        private TDL _selectedTDL;
        public TDL SelectedTDL { get { return _selectedTDL; } set { _selectedTDL = value; OnPropertyChanged(); } }

        private Task selectedTask;
        public Task SelectedTask { get { return selectedTask; } set { selectedTask = value; OnPropertyChanged(); }}

        public ObservableCollection<TDL> ItemsCollection { get; set; }
        public ObservableCollection<Priority> Priorities { get; set; }

        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories { get { return _categories; } set { _categories = value; OnPropertyChanged(); } }

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

        private string _tempCategory;
        public string TempCategory
        {
            get { return _tempCategory; }
            set
            {
                _tempCategory = value;
                OnPropertyChanged();
            }
        }

        private Priority _tempPriority;
        public Priority TempPriority
        {
            get { return _tempPriority; }
            set
            {
                _tempPriority = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tempDate;
        public DateTime TempDate
        {
            get { return _tempDate; }
            set
            {
                _tempDate = value;
                OnPropertyChanged();
            }
        }

        public TreeViewVM()
        {
            Categories = new ObservableCollection<string>() {"School", "Cook", "Home"};
            Priorities = new ObservableCollection<Priority>(Enum.GetValues(typeof(Priority)).Cast<Priority>());
            TempDate = DateTime.Now;
            ItemsCollection = new ObservableCollection<TDL>();

            //TDL tdl = new TDL("Scoala");
            //tdl.SubTDLs.Add(new TDL("Valentina", tdl.TDLName));
            //tdl.SubTDLs[0].SubTasks.Add(new Task("Rc", Priority.Low, Categories[0], DateTime.Now));
            //tdl.SubTDLs[0].SubTasks.Add(new Task("Rc", Priority.High, Categories[0], DateTime.Now.Date.AddDays(-1)));
            //tdl.SubTDLs.Add(new TDL("Georgeta", tdl.TDLName));
            //tdl.SubTDLs[1].SubTasks.Add(new Task("Rc", Priority.Low, Categories[0], DateTime.Now.Date.AddDays(1)));
            //tdl.SubTDLs[1].SubTasks.Add(new Task("Ac", Priority.Medium, Categories[0], DateTime.Now.Date.AddDays(2)));
            //tdl.SubTDLs[1].SubTasks.Add(new Task("Cc", Priority.Medium, Categories[0], DateTime.Now.Date.AddDays(3)));
            //ItemsCollection.Add(tdl);

            Manager = new Manager();
            //Manager.Serialize(ItemsCollection, "tdls.bin");
            ItemsCollection = Manager.Deserialize("tdls.bin");

            AddToDoList = new RelayCommand(o => AddTDL());
            AddSubToDo = new RelayCommand(o => AddSubTDL());
            AddTask = new RelayCommand(o => AddTaskTDL());
            OpenStats = new RelayCommand(o => OpenStatistics());
            Save = new RelayCommand(o => Serialize());
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
                    item.SubTDLs.Add(new TDL(Text, SelectedTDL.TDLName));
                    Text = String.Empty;
                    break;
                }
            }
        }
        public void AddTaskTDL()
        {
            Task task = new Task(Text, TempPriority, TempCategory, TempDate);
            Text = String.Empty;
            if (SelectedTDL.RootName != null)
            {
                var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == SelectedTDL.RootName));
                if (indexRootTdl != -1)
                {
                    var indexSubTdl = ItemsCollection[indexRootTdl].SubTDLs.IndexOf(ItemsCollection[indexRootTdl].SubTDLs.FirstOrDefault(root => root.TDLName == SelectedTDL.TDLName));
                    ItemsCollection[indexRootTdl].SubTDLs[indexSubTdl].SubTasks.Add(task);
                }
            }
            else
            {
                var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == SelectedTDL.TDLName));
                ItemsCollection[indexRootTdl].SubTasks.Add(task);
            }
        }
        public void OpenStatistics()
        {
            var createStats = new Statistics(this.ItemsCollection);
            createStats.Show();
        }
        public void Serialize()
        {
            Manager.Serialize(ItemsCollection, "tdls.bin");
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

    }
}
