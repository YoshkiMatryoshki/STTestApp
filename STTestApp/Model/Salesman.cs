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

        #endregion

    }
}
