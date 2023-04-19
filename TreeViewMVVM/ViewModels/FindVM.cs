using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TreeViewMVVM.Commands;

namespace TreeViewMVVM.ViewModels
{
    public class FullPath
    {
        private string _path;
        private string _text;

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public FullPath(string path, string text) {
        _path = path;
        _text = text;
        }
    }
    public class FindVM : BaseVM
    {
        public RelayCommand FindTask { get; set; }
        public RelayCommand Close { get; set; }
        private TreeViewVM mainView;
        public TreeViewVM MainView { get { return mainView; } set { mainView = value; } }
        private string path;
        public string Path { get { return path; } set { path = value; OnPropertyChanged(Path); } }
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
        private ObservableCollection<FullPath> _fullPath;
        public ObservableCollection<FullPath> FullPath
        {
            get { return _fullPath; }
            set { _fullPath=value; OnPropertyChanged(); }
        }

        public FindVM(TreeViewVM mainVM)
        {
            MainView = mainVM;
            Path = string.Empty;
            Text = string.Empty;
            FindTask = new RelayCommand(o => Find());
            Close = new RelayCommand(o => CloseWindow());
            FullPath = new ObservableCollection<FullPath>();
        }
        public void Find()
        {
            if (Text == string.Empty)
                MessageBox.Show("No input");
            else
            {
                FullPath.Clear();
                foreach(var root in mainView.ItemsCollection)
                {
                    if(root.SubTasks.Count != 0)
                        foreach(var task in root.SubTasks)
                        {
                            if(task.TaskName == Text)
                            {
                                Path = root.TDLName;
                                FullPath.Add(new FullPath(Path,Text));
                            }

                        }
                    foreach(var tdl in root.SubTDLs)
                    {
                        foreach (var task in tdl.SubTasks)
                        {
                            if (task.TaskName == Text)
                            {
                                Path = root.TDLName + " >> " +tdl.TDLName;
                                FullPath.Add(new FullPath(Path, Text));
                            }
                        }
                    }
                }
                Text = string.Empty;
            }
        }
        public void CloseWindow()
        {

        }
    }
}
