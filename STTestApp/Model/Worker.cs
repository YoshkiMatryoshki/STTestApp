using System;
using System.Collections.Generic;
using System.Text;

namespace STTestApp.Model
{
    /// <summary>
    /// Работник с зарплатой, начисляющейся за выслугу лет.  
    /// Подчиняется Boss. Подчиненные в зависимости от canhaveSubordinates
    /// </summary>
    public class Worker
    {
        #region Базовые свойства: Id, Имя, дата вербовки

        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public DateTime HiringDate { get; set; }
        #endregion

        #region Для иерархии отношений начальник - подчиненный

        /// <summary>
        /// id руководителя
        /// </summary>
        public int? BossId { get; set; }
        /// <summary>
        /// нав. свойство руководитель
        /// </summary>
        public virtual Worker Boss { get; set; }

        /// <summary>
        /// Разрешено ли иметь кого-л в подчинении. (Employee - false)
        /// </summary>
        public bool canHaveSubordinates { get; protected set; }
        /// <summary>
        /// Список сотрудников, находящихся в подчинении у thisWorkera
        /// </summary>
        private ICollection<Worker> subordinates;
        public ICollection<Worker> Subordinates
        {

            get => canHaveSubordinates ? subordinates : null;
            set
            {
                subordinates = canHaveSubordinates ? value : null;
            }
        }
        #endregion

        #region Группа работников
        public int WorkerGroupId { get; private set; }
        public WorkerGroup WorkerGroup { get; protected set; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// default const
        /// </summary>
        public Worker()
        {

        }
        /// <summary>
        /// Конструктор на базе рабочей группы
        /// </summary>
        /// <param name="workerGroup">Рабочая группа</param>
        public Worker(WorkerGroup workerGroup)
        {
            WorkerGroup = workerGroup;
        }
        #endregion


        #region Методы 
        /// <summary>
        /// Вычисляет зп с учетом выслуги для определенной даты
        /// </summary>
        /// <param name="date">Дата для которой производится расчет</param>
        /// <returns>ЗП в уе + выслуга</returns>
        public virtual double GetSalary(DateTime date)
        {
            /// если кое-кто забыл вытянуть из соседней таблицы отдел, он же группа.
            if (WorkerGroup == null)
                return -100;
            double yearsWorked = GetWorkTime(date);
            double bonusPercent = yearsWorked * WorkerGroup.BonusValue;
            double bonus = WorkerGroup.BaseValue * bonusPercent;
            double maxBonus = WorkerGroup.GetMaxBonus();
            double realBonus = Math.Clamp(bonus, 0, maxBonus);
            return WorkerGroup.BaseValue + realBonus;

        }
        /// <summary>
        /// Подсчитывает сколько лет Worker воркал (или отработает) в какой-то момент в будущем
        /// </summary>
        /// <param name="momentOfTime">Дата, относительно которой считается разница</param>
        /// <returns>разница между датой поступления на работу и указанной датой в ГОДАХ</returns>
        public double GetWorkTime(DateTime momentOfTime)
        {
            if (momentOfTime < HiringDate)
                return 0;
            int workYears = momentOfTime.Year - HiringDate.Year - 1;
            if ((momentOfTime.Month > HiringDate.Month) || ((momentOfTime.Month == HiringDate.Month) && (momentOfTime.Day >= HiringDate.Day)))
                workYears++;
            return workYears;
        }
        /// <summary>
        /// Метод для рекурсивного сбора всех "наследников" всех уровней
        /// </summary>
        /// <returns>такой себе список подчиненных</returns>
        public List<Worker> GetAllSubordinates()
        {
            List<Worker> subs = new List<Worker>();
            if (!canHaveSubordinates)
                return subs;
            subs.AddRange(Subordinates);
            foreach(var subordinate in Subordinates)
            {
                subs.AddRange(subordinate.GetAllSubordinates());
            }
            return subs;
        }
        #endregion
    }
}
