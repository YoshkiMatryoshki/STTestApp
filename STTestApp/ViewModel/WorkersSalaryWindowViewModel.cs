using STTestApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace STTestApp.ViewModel
{
    /// <summary>
    /// VM для расчета заработной платы для всех сотрудников, отображаемых на экране
    /// </summary>
    class WorkersSalaryWindowViewModel : BaseViewModel
    {
        #region Св-ва

        /// <summary>
        /// Список всех работников для расчета зп
        /// </summary>
        private IEnumerable<Worker> workers;

        private ObservableCollection<WorkerInfo> workerInfos;
        /// <summary>
        /// Информация о работничках
        /// </summary>
        public ObservableCollection<WorkerInfo> WorkerInfos
        {
            get => workerInfos;
            set
            {
                workerInfos = value;
                OnPropertyChanged(nameof(WorkerInfos));
            }
        }

        private double sumSalary;
        /// <summary>
        /// Сумма зарплат отображаемых сотрудников
        /// </summary>
        public double SumSalary
        {
            get => sumSalary;
            set
            {
                if (sumSalary == value)
                    return;
                sumSalary = value;
                OnPropertyChanged(nameof(SumSalary));
            }
        }

        private DateTime countDate;
        /// <summary>
        /// Дата, на которую производится расчет
        /// </summary>
        public DateTime CountDate
        {
            get => countDate;
            set
            {
                if (countDate == value || value < DateTime.Today)
                    return;
                countDate = value;
                CountSalary();
                OnPropertyChanged(nameof(CountDate));
            }
        }


        #endregion


        #region Конструктор

        public WorkersSalaryWindowViewModel(IEnumerable<Worker> workersToCount)
        {
            workers = workersToCount;
            countDate = DateTime.Today;
            CountSalary();
        }


        #endregion

        #region Методы
        /// <summary>
        /// Расчет Зп для всех workers
        /// Перезаполняемся каждый раз....такое себе
        /// </summary>
        private void CountSalary()
        {
            WorkerInfos?.Clear();
            WorkerInfos ??= new ObservableCollection<WorkerInfo>();
            double sum = 0;
            foreach(var worker in workers)
            {
                WorkerInfo newRecord = new WorkerInfo(worker, CountDate);
                WorkerInfos.Add(newRecord);
                sum += newRecord.Salary;
            }
            SumSalary = Math.Round(sum,2);
        }



        #endregion
    }
}
