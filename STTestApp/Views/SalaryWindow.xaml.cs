using STTestApp.Model;
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
    /// Interaction logic for SalaryWindow.xaml
    /// </summary>
    public partial class SalaryWindow : Window
    {
        public Worker Worker { get; private set; }

        public SalaryWindow(Worker worker)
        {
            InitializeComponent();
            Worker = worker;
            DataContext = Worker;
        }
        /// <summary>
        /// Возврат к предыдущему окну
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        //TEST Расчет зп
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var bttn = (Button)sender;
            bttn.Content = Worker.GetSalary(DateTime.Today);
        }
    }
}
