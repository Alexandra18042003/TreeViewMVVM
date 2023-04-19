using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TreeViewMVVM.Commands;

namespace TreeViewMVVM
{
    public class MenuVM : BaseVM
    {
        public ObservableCollection<string> databases;
        public ObservableCollection<string> Databases
        {
            get { return databases; }
            set
            {
                if (value != databases)
                {
                    databases = value;
                    OnPropertyChanged(nameof(databases));
                }
            }
        }
        public Manager Manager { get ; set; }
        public string selectedBin;
        public string SelectedBin
        {
            get { return selectedBin; }
            set
            {
                if (value != selectedBin)
                {
                    selectedBin = value;
                    OnPropertyChanged(nameof(selectedBin));
                }
            }
        }
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
        public RelayCommand OpenDB { get; set; }
        public RelayCommand CreateDB { get; set; }

        public MenuVM()
        {
            databases= new ObservableCollection<string>();

            Manager = new Manager();
            databases = Manager.DeserializeDataBases("databases.bin");

            OpenDB = new RelayCommand(o => OpenDatabase());
            CreateDB = new RelayCommand(o => CreateDatabase());
        }

        private void CreateDatabase()
        {
            databases.Add(Text);
            Manager.SerializeDataBases(databases, "databases.bin");
            var createStats = new Notes(Text);
            Text = string.Empty;
            createStats.Show();
        }

        private void OpenDatabase()
        {
            var createStats = new Notes(SelectedBin);
            createStats.Show();
        }
    }
}
