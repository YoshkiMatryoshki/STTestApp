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
            //ПЕРЕСОЗДАЕТ ДБ и заполняет данными
            //FeedDB();
            DataContext = new MainWindowViewModel();



        }

        /// <summary>
        /// Заполняет Бд рандомными ребятами (для очень тестов)
        /// </summary>
        private static void FeedDB()
        {
            var EmployeesGroup = new WorkerGroup(15000, 0.03, 0.3, null)
            {
                GroupName = "Employee",
                WorkerGroupId = 1
            };
            var ManagersGroup = new WorkerGroup(18000, 0.05, 0.4, 0.005)
            {
                GroupName = "Manager",
                WorkerGroupId = 2
            };
            var SalesmansGroup = new WorkerGroup(17000, 0.01, 0.35, 0.003)
            {
                GroupName = "Salesman",
                WorkerGroupId = 3
            };

            Salesman top1 = new Salesman(SalesmansGroup)
            {
                WorkerName = "Босс Босович",
                HiringDate = DateTime.Today.AddYears(-2),
            };
            Manager manager = new Manager(ManagersGroup)
            {

                WorkerName = "ЭффективныйМанагер",
                HiringDate = DateTime.Today.AddYears(-5),
                Boss = top1
            };
            Employee employee = new Employee(EmployeesGroup)
            {
                WorkerName = "Работник1",
                HiringDate = DateTime.Today.AddYears(-3),
                Boss = manager
            };

            List<Worker> test = new List<Worker>();
            test.Add(top1);
            test.Add(manager);
            test.Add(employee);

            var rng = new Random();

            Worker newWorker;
            for (int i = 0; i < 100; i++)
            {
                var bosses = test.Where(w => !(w is Employee)).ToList();
                newWorker = rng.Next(0, 3) switch
                {
                    0 => new Employee(EmployeesGroup)
                    {
                        WorkerName = $"Работник{i + 1}",
                    },
                    1 => new Manager(ManagersGroup)
                    {
                        WorkerName = $"Менеджер{i + 1}"
                    },
                    2 => new Salesman(SalesmansGroup)
                    {
                        WorkerName = $"Салесман{i + 1}"
                    },
                    _ => throw new Exception("Ошибка")
                };
                newWorker.HiringDate = DateTime.Today.AddYears(-rng.Next(0, 11));
                newWorker.Boss = bosses[rng.Next(0, bosses.Count)];
                test.Add(newWorker);
            }

            using (var db = new WorkersContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.WorkerGroups.AddRange(EmployeesGroup, SalesmansGroup, ManagersGroup);
                //db.Workers.AddRange(employee, top1, manager);
                db.Workers.AddRange(test);
                db.SaveChanges();
            }
        }
    }
}
