using System;
using System.Collections.Generic;
using System.Text;

namespace STTestApp.Model
{
    class Salesman : Worker
    {
        #region Конструкторы
        /// <summary>
        /// default const
        /// </summary>
        public Salesman()
        {

        }
        /// <summary>
        /// КОнструктор на базе родителя, разрешает наличие подчиненных
        /// </summary>
        /// <param name="workerGroup">Рабочая группа</param>
        public Salesman(WorkerGroup workerGroup) : base(workerGroup)
        {
            canHaveSubordinates = true;
        }
        #endregion

        #region Методы

        public override double GetSalary(DateTime date)
        {
            double baseSalary =  base.GetSalary(date);
            double subordinatesSalary = 0;

            //дроп если бонуса нет по какой-либо причине (exception бы)
            if (!WorkerGroup.SubordinateBonus.HasValue)
                return baseSalary;

            List<Worker> allSubordinates = new List<Worker>();
            //Получаем всех подчиненных вниз по ветке (всех уровней)
            allSubordinates = GetAllSubordinates();

            foreach(var sub in allSubordinates)
            {
                subordinatesSalary += sub.GetSalary(date);
            }
            double bonusSalary = subordinatesSalary * WorkerGroup.SubordinateBonus.Value;
            double result = bonusSalary + baseSalary;

            return result;
        }
        #endregion

    }
}
