using System;
using System.Collections.Generic;
using System.Text;

namespace STTestApp.Model
{
    /// <summary>
    /// Группа работников с информацией о бонусах за выслугу, % от зп подчиненных 
    /// и (потенциально) другой информацией
    /// </summary>
    class WorkerGroup
    {
        #region Свойства
        public int WorkerGroupId { get; set; }
        public string GroupName { get; set; }
        /// <summary>
        /// Базовая ставка
        /// </summary>
        public double BaseValue { get; private set; }
        /// <summary>
        /// Ежегодная надбавка к зп (%)
        /// </summary>
        public double BonusValue { get; private set; }
        /// <summary>
        /// Максимальный бонус от базовой ставки за выслугу лет (%)
        /// </summary>
        public double MaxBonus { get; private set; }
        /// <summary>
        /// Надбавка в % от зарплаты подчиненных
        /// </summary>
        public double? SubordinateBonus { get; private set; }
        #endregion


        #region Конструкторы
        /// <summary>
        /// default constructor
        /// </summary>
        public WorkerGroup()
        {

        }
        /// <summary>
        /// Конструктор с установкой базовых значений
        /// </summary>
        /// <param name="baseValue">Ставка</param>
        /// <param name="bonusValue">ежегодный % за выслугу (в формате 0.x)</param>
        /// <param name="maxBonus">максимальный % от BaseValue для надбавки (в формате 0.x)</param>
        /// <param name="subBonus">% от зарплаты сотрудников в качестве надбавки (в формате 0.x)</param>
        public WorkerGroup(double baseValue, double bonusValue, double maxBonus, double? subBonus)
        {
            BaseValue = baseValue;
            BonusValue = bonusValue;
            MaxBonus = maxBonus;
            SubordinateBonus = subBonus;
        }
        #endregion


        #region Методы
        /// <summary>
        /// Вычисляет максимально возможный бонус (в денежках) за выслугу лет
        /// </summary>
        /// <returns>Максимально возможная сумма в у.е.</returns>
        public double GetMaxBonus()
        {
            return BaseValue * MaxBonus;
        }
        #endregion
    }
}
