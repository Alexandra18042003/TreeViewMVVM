using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TreeViewMVVM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new TreeViewVM();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedObject = e.NewValue as TDL;
            var treeView = DataContext as TreeViewVM;
            treeView.SelectedTDL = selectedObject;

            addTasks.Visibility = Visibility.Visible;

            if(treeView.checkMainTDL(selectedObject))
                subTDL.Visibility = Visibility.Visible;
            else subTDL.Visibility = Visibility.Hidden;

            //if (selectedObject != null)
            //{
            //    myDataGrid.ItemsSource = selectedObject.SubTasks;
            //}
        }
    }
}
