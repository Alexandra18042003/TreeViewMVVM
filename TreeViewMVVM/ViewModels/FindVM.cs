using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TreeViewMVVM.Commands;

namespace TreeViewMVVM.ViewModels
{
    public class FindVM : BaseVM
    {
        public RelayCommand FindTask { get; set; }
        public RelayCommand Close { get; set; }
        private TreeViewVM mainView;
        public TreeViewVM MainView { get { return mainView; } set { mainView = value; } }
        private string path;
        public string Path { get { return path; } set { path = value; } }
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
        public FindVM(TreeViewVM mainVM)
        {
            MainView = mainVM;
            Path = string.Empty;
            Text = string.Empty;
            FindTask = new RelayCommand(o => Find());
            Close = new RelayCommand(o => CloseWindow());
        }
        public void Find()
        {
            if (Text == string.Empty)
                MessageBox.Show("No input");
            else
            {
                foreach(var root in mainView.ItemsCollection)
                {
                    if(root.SubTasks.Count != 0)
                        foreach(var task in root.SubTasks)
                        {
                            if(task.TaskName == Text)
                            {
                                Path = root.TDLName;
                            }

                        }
                    foreach(var tdl in root.SubTDLs)
                    {
                        foreach (var task in tdl.SubTasks)
                        {
                            if (task.TaskName == Text)
                            {
                                Path = root.TDLName + " >> " +tdl.TDLName;
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
