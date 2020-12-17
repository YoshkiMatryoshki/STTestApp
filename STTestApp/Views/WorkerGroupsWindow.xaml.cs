using STTestApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace STTestApp.Views
{
    /// <summary>
    /// Interaction logic for WorkerGroupsWindow.xaml
    /// </summary>
    public partial class WorkerGroupsWindow : Window
    {
        public WorkerGroupsWindow()
        {
            InitializeComponent();
            DataContext = new WorkerGroupsVM();
        }

        /// <summary>
        /// Возврат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
