using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TreeViewMVVM
{
    /// <summary>
    /// Interaction logic for Notes.xaml
    /// </summary>
    public partial class Notes : Window
    {
        public Notes(string file)
        {
            InitializeComponent();
            this.DataContext = new TreeViewVM(file+".bin");
        }
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedObject = e.NewValue as TDL;
            var treeView = DataContext as TreeViewVM;
            treeView.SelectedTDL = selectedObject;

            addTasks.Visibility = Visibility.Visible;

            if (treeView.checkMainTDL(selectedObject))
                subTDL.Visibility = Visibility.Visible;
            else subTDL.Visibility = Visibility.Hidden;
        }
    }
}
