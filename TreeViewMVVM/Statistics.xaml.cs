using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TreeViewMVVM.ViewModels;

namespace TreeViewMVVM
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics(ObservableCollection<TDL> ItemsCollection)
        {
            InitializeComponent();
            this.DataContext = new StatisticsVM(ItemsCollection);
        }
    }
}
