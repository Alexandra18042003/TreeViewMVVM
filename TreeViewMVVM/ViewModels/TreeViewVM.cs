﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        public TreeViewVM()
        {
            ItemsCollection = new ObservableCollection<TDL>();
            TDL tdl = new TDL("Scoala");
            tdl.SubTDLs.Add(new TDL("Valentina"));
            tdl.SubTDLs[0].SubTasks.Add(new Task("Rc", "tema1", "status 1", Priority.Low, new DateTime(), categories[0]));
            tdl.SubTDLs[0].SubTasks.Add(new Task("Rc", "tema2", "status 2", Priority.High, new DateTime(), categories[0]));
            tdl.SubTDLs.Add(new TDL("Georgeta"));
            tdl.SubTDLs[1].SubTasks.Add(new Task("Rc", "tema3", "status 3", Priority.Low, new DateTime(), categories[0]));
            tdl.SubTDLs[1].SubTasks.Add(new Task("Rc", "tema4", "status 4", Priority.Medium, new DateTime(), categories[0]));

            ItemsCollection.Add(tdl);


            //SelectedTDL = null;
        }
        public ObservableCollection<TDL> ItemsCollection { get; set; }
        List<string> categories = new List<string>()
        {
            "School", "Cook", "Home"
        };


        //private TDL _selectedTDL;
        //public TDL SelectedTDL
        //{
        //    get { return _selectedTDL; }
        //    set
        //    {
        //        if (_selectedTDL != value)
        //        {
        //            if (_selectedTDL != null)
        //            {
        //                _selectedTDL.IsSelected = false;
        //            }
        //            _selectedTDL = value;
        //            if (_selectedTDL != null)
        //            {
        //                _selectedTDL.IsSelected = true;
        //            }
        //            NotifyPropertyChanged(nameof(SelectedTDL));
        //            NotifyPropertyChanged(nameof(SubTasks));
        //        }
        //    }
        //}

        //public ObservableCollection<Task> SubTasks
        //{
        //    get
        //    {
        //        if (SelectedTDL == null) return null;
        //        return SelectedTDL.SubTasks;
        //    }
        //}

        //private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    TDL item = sender as TDL;
        //    if (item != null && item.IsSelected)
        //        NotifyPropertyChanged("SelectedItem");
        //}


        //public TDL SelectedItem
        //{
        //    get
        //    {
        //        return GetSelectedItem(this.ItemsCollection);
        //    }
        //}

        //private static TDL GetSelectedItem(ObservableCollection<TDL> items)
        //{
        //    ////top-level items:
        //    //TDL item = items.FirstOrDefault(i => i.IsSelected);
        //    //if (item == null)
        //    //{
        //    //    //sub-level items:
        //    //    ObservableCollection<TDL> subItems = items.OfType<TDL>().SelectMany(d => d.SubItems);
        //    //    if (items.Any())
        //    //        item = GetSelectedItem(subItems);
        //    //}
        //    //return item;
        //}

        //public TDL SelectedTDL
        //{
        //    get
        //    {
        //        return selectedTDL;
        //    }
        //    set
        //    {
        //        selectedTDL = value;
        //        NotifyPropertyChanged("SelectedTDL");
        //    }
        //}

        //public ObservableCollection<Task> SubTasks
        //{
        //    get
        //    {
        //        if (selectedTDL == null) return null;
        //        return selectedTDL.SubTasks;
        //    }
        //}
    }
}
