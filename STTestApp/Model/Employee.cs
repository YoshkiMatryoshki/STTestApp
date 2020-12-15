using System;
using System.Collections.Generic;
using System.Text;

namespace STTestApp.Model
{
    /// <summary>
    /// Обычный работяга, может получать надбавку только за выслугу лет
    /// и не может руководить другими сотрудниками
    /// </summary>
    class Employee : Worker
    {

        #region Конструкторы
        /// <summary>
        /// Конструктор, устанавливающий запрет на наличие подчиненных
        /// </summary>
        /// <param name="workerGroup"></param>
        public Employee(WorkerGroup workerGroup) : base(workerGroup)
        {
            canHaveSubordinates = false;
        }
        #endregion

    }
}
