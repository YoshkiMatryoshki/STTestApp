using Microsoft.EntityFrameworkCore;
using STTestApp.Model;
using STTestApp.ViewModel;
using STTestApp.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            var EmployeesGroup = new WorkerGroup(15000,0.03,0.3,null)
            {
                GroupName = "Employee"
            };
            var ManagersGroup = new WorkerGroup(18000,0.05,0.4,0.005)
            {
                GroupName = "Manager"
            };
            var SalesmansGroup = new WorkerGroup(17000,0.01,0.35,0.003)
            {
                GroupName = "Salesman"
            };

            Salesman top1 = new Salesman(SalesmansGroup)
            {
                WorkerName = "ALFABOSS",
                HiringDate = DateTime.Today.AddYears(-2),
            };
            Manager manager = new Manager(ManagersGroup)
            {

                WorkerName = "EffectiveManager",
                HiringDate = DateTime.Today.AddYears(-5),
                Boss = top1
            };
            Employee employee = new Employee(EmployeesGroup)
            {
                WorkerName = "Работяга",
                HiringDate = DateTime.Today.AddYears(-1),
                Boss = manager
            };


        }
        /// <summary>
        /// Вызов диалогового окна для расчета зп выбранного сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            salaryWindow.ShowDialog();
        }
    }
}
