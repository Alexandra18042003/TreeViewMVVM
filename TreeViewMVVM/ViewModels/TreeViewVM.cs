using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public RelayCommand AddCategory { get; set; }
        public RelayCommand SaveChanges { get; set; }
        public RelayCommand DeleteTDL { get; set; }
        public RelayCommand DeleteTask { get; set; }
        public RelayCommand Find { get; set; }
        public Manager Manager { get; set; }

        private TDL _selectedTDL;
        public TDL SelectedTDL { get { return _selectedTDL; } set { _selectedTDL = value; OnPropertyChanged(); } }

        private Task selectedTask;
        public Task SelectedTask { get { return selectedTask; } set { selectedTask = value; IsSelectedTask = selectedTask != null; OnPropertyChanged(); } }

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
                OnPropertyChanged();
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
        private bool isSelectedTask;
        public bool IsSelectedTask
        {
            get { return isSelectedTask; }
            set
            {
                isSelectedTask = value;
                OnPropertyChanged();
            }
        }

        public TreeViewVM()
        {
            Categories = new ObservableCollection<string>();
            Priorities = new ObservableCollection<Priority>(Enum.GetValues(typeof(Priority)).Cast<Priority>());
            TempDate = DateTime.Now;
            ItemsCollection = new ObservableCollection<TDL>();
            Manager = new Manager();

            try
            {
                ItemsCollection = Manager.Deserialize("tdls.bin");
                Categories = Manager.DeserializeCategories("categories.bin");
            }
            catch { }

            AddToDoList = new RelayCommand(o => AddTDL());
            AddSubToDo = new RelayCommand(o => AddSubTDL());
            AddTask = new RelayCommand(o => AddTaskTDL());
            OpenStats = new RelayCommand(o => OpenStatistics());
            Save = new RelayCommand(o => Serialize());
            AddCategory = new RelayCommand(o => AddCat());
            SaveChanges = new RelayCommand(o => SaveCh());
            DeleteTDL = new RelayCommand(o => Deletetdl());
            DeleteTask = new RelayCommand(o => Deletetask());
            Find = new RelayCommand(o => Search());
        }

        public void AddTDL()
        {
            var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == Text));
            if (indexRootTdl == -1)
            {
                var newItem = new TDL(Text);
                ItemsCollection.Add(newItem);
            }
            else MessageBox.Show("Can't have duplicates!");
            Text = String.Empty;
        }
        public void AddSubTDL()
        {
            var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == Text));
            if (indexRootTdl == -1)
            {
                var item = ItemsCollection.FirstOrDefault(tdl => tdl.TDLName == SelectedTDL.TDLName);
                item.SubTDLs.Add(new TDL(Text, SelectedTDL.TDLName));
            }
            else MessageBox.Show("Can't have duplicates!");
            Text = String.Empty;
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
        public void AddCat()
        {
            var indexCaategory = Categories.IndexOf(Categories.FirstOrDefault(category => category == Text));
            if (indexCaategory == -1)
                Categories.Add(Text);
            else MessageBox.Show("Can't have duplicates!");
            Text = String.Empty;
        }
        public void SaveCh()
        {
            var updatedDescription = SelectedTask.TaskDescription;
            if (SelectedTDL.RootName != null)
            {
                var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == SelectedTDL.RootName));
                if (indexRootTdl != -1)
                {
                    var indexSubTdl = ItemsCollection[indexRootTdl].SubTDLs.IndexOf(ItemsCollection[indexRootTdl].SubTDLs.FirstOrDefault(root => root.TDLName == SelectedTDL.TDLName));
                    var indexTask = ItemsCollection[indexRootTdl].SubTDLs[indexSubTdl].SubTasks.IndexOf(ItemsCollection[indexRootTdl].SubTDLs[indexSubTdl].SubTasks.FirstOrDefault(task => task.TaskName == SelectedTask.TaskName));
                    ItemsCollection[indexRootTdl].SubTDLs[indexSubTdl].SubTasks[indexTask] = SelectedTask;
                }
            }
            else
            {
                var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == SelectedTDL.TDLName));
                var indexTask = ItemsCollection[indexRootTdl].SubTasks.IndexOf(ItemsCollection[indexRootTdl].SubTasks.FirstOrDefault(task => task.TaskName == SelectedTask.TaskName));
                ItemsCollection[indexRootTdl].SubTasks[indexTask] = SelectedTask;
            }
        }
        public void Deletetdl()
        {
            if (SelectedTDL.RootName != null)
            {
                var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == SelectedTDL.RootName));
                if (indexRootTdl != -1)
                {
                    ItemsCollection[indexRootTdl].SubTDLs.Remove(ItemsCollection[indexRootTdl].SubTDLs.SingleOrDefault(tdl => tdl.TDLName == SelectedTDL.TDLName));
                }
            }
            else
            {
                ItemsCollection.Remove(ItemsCollection.SingleOrDefault(tdl => tdl.TDLName == SelectedTDL.TDLName));
            }
        }
        public void Deletetask()
        {
            if (SelectedTDL.RootName != null)
            {
                var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == SelectedTDL.RootName));
                if (indexRootTdl != -1)
                {
                    var indexSubTdl = ItemsCollection[indexRootTdl].SubTDLs.IndexOf(ItemsCollection[indexRootTdl].SubTDLs.FirstOrDefault(root => root.TDLName == SelectedTDL.TDLName));
                    ItemsCollection[indexRootTdl].SubTDLs[indexSubTdl].SubTasks.Remove(ItemsCollection[indexRootTdl].SubTDLs[indexSubTdl].SubTasks.SingleOrDefault(task => task.TaskName == SelectedTask.TaskName));
                }
            }
            else
            {
                var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == SelectedTDL.TDLName));
                ItemsCollection[indexRootTdl].SubTasks.Remove(ItemsCollection[indexRootTdl].SubTasks.SingleOrDefault(task => task.TaskName == SelectedTask.TaskName));
            }
        }
        public void Serialize()
        {
            Manager.Serialize(ItemsCollection, "tdls.bin");
            Manager.SerializeCategories(Categories, "categories.bin");
        }
        public void Search()
        {
            var createSearch = new Find(this);
            createSearch.Show();
        }
        public bool checkMainTDL(TDL tdl)
        {
            var indexRootTdl = ItemsCollection.IndexOf(ItemsCollection.FirstOrDefault(root => root.TDLName == tdl.TDLName));
            if (indexRootTdl != -1)
                return true;
            return false;
        }

    }
}
