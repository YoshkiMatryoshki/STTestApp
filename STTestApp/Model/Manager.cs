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


        #endregion

    }
}
