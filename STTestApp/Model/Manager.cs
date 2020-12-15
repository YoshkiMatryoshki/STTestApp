using System;
using System.Collections.Generic;
using System.Text;

namespace STTestApp.Model
{
    /// <summary>
    /// Самый настоящий эффективный менеджер! 
    /// Получает бонус не только за выслугу, но и за подчиненных
    /// </summary>
    class Manager : Worker
    {

        #region Конструкторы
        /// <summary>
        /// default const
        /// </summary>
        public Manager()
        {

        }
        /// <summary>
        /// Конструктор на базе ролительского класса, 
        /// разрешающий наличие подчиненных
        /// </summary>
        /// <param name="workerGroup">Рабочая группа</param>
        public Manager(WorkerGroup workerGroup) : base(workerGroup)
        {
            canHaveSubordinates = true;
        }

        #endregion


        #region Методы

        /// <summary>
        /// Надбавка к зп менеджера в виде процента от зарплаты всех подчиненных ПЕРВОГО УРОВНЯ!!! (непосредственное подчинение???)
        /// </summary>
        /// <param name="date">Дата, на которую производится расчет</param>
        /// <returns></returns>
        public override double GetSalary(DateTime date)
        {
            double baseSalary =  base.GetSalary(date);
            double subordinatesSalary = 0;

            //дроп если бонуса нет по какой-либо причине (exception бы)
            if (!WorkerGroup.SubordinateBonus.HasValue)
                return baseSalary;

            foreach(var sub in Subordinates)
            {
                subordinatesSalary += sub.GetSalary(date);
            }
            double bonusSalary = subordinatesSalary * WorkerGroup.SubordinateBonus.Value;
            return bonusSalary + baseSalary;
        }
        #endregion

    }
}
