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
        public int MaxBonus { get; private set; }
        /// <summary>
        /// Надбавка в % от зарплаты подчиненных
        /// </summary>
        public double? SubordinateBonus { get; private set; }
        #endregion

        #region Методы
        /// <summary>
        /// Вычисляет максимально возможный бонус (в денежках) за выслугу лет
        /// </summary>
        /// <returns>Максимально возможная сумма в у.е.</returns>
        public double GetMaxBonus()
        {
            return BaseValue * Convert.ToDouble(MaxBonus);
        }
        #endregion
    }
}
