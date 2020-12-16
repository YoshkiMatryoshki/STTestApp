using System;
using System.Collections.Generic;
using System.Text;

namespace STTestApp.Model
{
    /// <summary>
    /// Вспомогательный класс для отображения информации о зп в ListView
    /// </summary>
    class WorkerInfo
    {
        public int Id { get; private set; }
        /// <summary>
        /// Имя работника
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Группа работников к которой относится
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// Стаж в годах на момент расчета
        /// </summary>
        public int Exp { get; set; }
        /// <summary>
        /// Зп на момент расчета
        /// </summary>
        public double Salary { get; set; }
        /// <summary>
        /// Базовая ставка
        /// </summary>
        public double BaseSalary { get; set; }
        /// <summary>
        /// Надбавка от всех источников
        /// </summary>
        public double Bonus { get; set; }

        public WorkerInfo(Worker worker, DateTime dateTime)
        {
            Id = worker.WorkerId;
            Name = worker.WorkerName;
            Group = worker?.WorkerGroup.GroupName;
            if (worker.WorkerGroup != null)
            {
                BaseSalary = worker.WorkerGroup.BaseValue;
            }
            Salary= Math.Round(worker.GetSalary(dateTime),2);
            Exp = Convert.ToInt32(worker.GetWorkTime(dateTime));
            Bonus = Math.Round((Salary - BaseSalary),2);
        }

    }
}
