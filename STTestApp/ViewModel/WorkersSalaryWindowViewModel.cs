using STTestApp.Model;
using System;
using System.Collections.Generic;
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
                OnPropertyChanged(nameof(CountDate));
            }
        }


        #endregion


        #region Конструктор

        public WorkersSalaryWindowViewModel(IEnumerable<Worker> workersToCount)
        {
            workers = workersToCount;
            countDate = DateTime.Today;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Расчет Зп для всех workers
        /// </summary>
        private void CountSalary()
        {

        }
        #endregion
    }
}
