using Microsoft.EntityFrameworkCore;
using STTestApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STTestApp.ViewModel
{
    /// <summary>
    /// ViewModel для основного окна приложения
    /// </summary>
    class MainWindowViewModel : BaseViewModel
    {

        #region Поля и свойства

        /// <summary>
        /// Список всех работников для отображения
        /// </summary>
        private IEnumerable<Worker> workers;
        public IEnumerable<Worker> Workers
        {
            get => workers;
            set
            {
                workers = value;
                OnPropertyChanged(nameof(Workers));
            }
        }
        /// <summary>
        /// Текущий выделенный работник
        /// </summary>
        private Worker selectedWorker;
        public Worker SelectedWorker
        {
            get => selectedWorker;
            set
            {
                if (selectedWorker?.WorkerId == value.WorkerId)
                    return;
                selectedWorker = value;
                OnPropertyChanged(nameof(SelectedWorker));
            }
        }

        #endregion


        #region Конструктор
        /// <summary>
        /// дефолтный конструктор
        /// </summary>
        public MainWindowViewModel()
        {
            //Подгрузка работничков из бд
            using (var db = new WorkersContext())
            {
                Workers = db.Workers.Include(e => e.Subordinates).Include(e => e.WorkerGroup).ToList();
            }
        }
        #endregion

    }
}
