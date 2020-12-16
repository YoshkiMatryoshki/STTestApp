using STTestApp.Model;
using STTestApp.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AllSalaryWindow.xaml
    /// </summary>
    public partial class AllSalaryWindow : Window
    {
        public AllSalaryWindow(IEnumerable<Worker> workersToCount)
        {
            InitializeComponent();
            DataContext = new WorkersSalaryWindowViewModel(workersToCount);
        }
        /// <summary>
        /// Возврат к пред окну
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
