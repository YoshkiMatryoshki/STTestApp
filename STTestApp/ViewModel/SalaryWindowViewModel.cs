using STTestApp.Model;
using STTestApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace STTestApp.ViewModel
{
    class SalaryWindowViewModel : WorkerBasedViewModel
    {
        #region Поля и Св-ва

        /// <summary>
        /// Заработная плата
        /// </summary>
        private double salary;
        public double Salary
        {
            get => salary;
            set
            {
                if (salary == value)
                    return;
                salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        /// <summary>
        /// Дата, на которую будет произведен расчет ЗП.
        /// </summary>
        private DateTime countDate;
        /// <summary>
        /// В сеттере запрет на установку даты из прошлого (неплохо бы выдавать сбщ об ошибке)
        /// </summary>
        public DateTime CountDate
        {
            get => countDate;
            set
            {
                if (countDate == value || value < DateTime.Today)
                    return;
                countDate = value;
                //Сразу обновляем и стаж!
                WorkExp = Convert.ToInt32(worker.GetWorkTime(countDate));
                OnPropertyChanged(nameof(CountDate));
            }
        }

        /// <summary>
        /// Стаж (на текущем предприятии)
        /// на выбранную дату
        /// </summary>
        private int workExp;
        public int WorkExp
        {
            get => workExp;
            set
            {
                if (workExp == value)
                    return;
                workExp = value;
                OnPropertyChanged(nameof(WorkExp));
            }
        }
        
        #endregion

        #region Команды
        /// <summary>
        /// Команда запуска расчета зп
        /// </summary>
        public ICommand CountSalary { get; private set; } 
        #endregion




        #region Конструкторы
        /// <summary>
        /// Конструктор с доп инфой соответствующей данной VM
        /// </summary>
        public SalaryWindowViewModel(Worker worker) : base(worker)
        {
            CountDate = DateTime.Today;    
            //Команда
            CountSalary = new BaseCommand(LookAtThisDudeSalary);
        }

        #endregion

        #region Методы
        /// <summary>
        /// Расчет ЗП конкретного сотрудника
        /// </summary>
        private void LookAtThisDudeSalary()
        {
            Salary = worker.GetSalary(CountDate);
        }
        #endregion
    }
}
