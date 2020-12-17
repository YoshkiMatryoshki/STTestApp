using STTestApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STTestApp.ViewModel
{
    /// <summary>
    /// VM для отображения списка групп сотрудников 
    /// (Сейчас в ней нет особой необходимости, но в теории функционал может расширяться)
    /// </summary>
    class WorkerGroupsVM : BaseViewModel
    {
        private IEnumerable<WorkerGroup> groups;
        /// <summary>
        /// Группы сотрудников
        /// </summary>
        public IEnumerable<WorkerGroup> Groups
        {
            get => groups;
            set
            {
                groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public WorkerGroupsVM()
        {
            using (var db = new WorkersContext())
            {
                Groups = db.WorkerGroups.ToList();
            }
        }
    }
}
