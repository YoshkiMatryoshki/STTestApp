using STTestApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace STTestApp.ViewModel
{
    /// <summary>
    /// VM, основанная на единичном работнике
    /// Основа для видов, отображающих информацию ободном работнике
    /// </summary>
    class WorkerBasedViewModel : BaseViewModel
    {
        #region Св-ва

        /// <summary>
        /// Тот самый переданный работник
        /// </summary>
        protected Worker worker;

        /// <summary>
        /// Имя отображаемого работника
        /// </summary>
        private string workerName;
        public string WorkerName
        {
            get => workerName;
            set
            {
                if (workerName == value)
                    return;
                workerName = value;
                OnPropertyChanged(nameof(WorkerName));
            }
        }

        /// <summary>
        /// Название группы, к которой относится пользователь
        /// (не музыкальной ахахахахаххахаха)
        /// </summary>
        private string workerGroupName;
        public string WorkerGroupName
        {
            get => workerGroupName;
            set
            {
                if (workerGroupName == value)
                    return;
                workerGroupName = value;
                OnPropertyChanged(nameof(WorkerGroupName));
            }
        }

        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор на базе посланного сотрудника
        /// </summary>
        public WorkerBasedViewModel(Worker worker)
        {
            this.worker = worker;
            WorkerName = worker.WorkerName;
            WorkerGroupName = worker.WorkerGroup.GroupName;
        }

        #endregion

    }
}
