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
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedObject = e.NewValue as TDL;
            if (selectedObject != null)
            {
                myDataGrid.ItemsSource = selectedObject.SubTasks;
            }
        }
    }
}
