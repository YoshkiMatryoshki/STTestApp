using Microsoft.EntityFrameworkCore;
using STTestApp.Model;
using STTestApp.ViewModel.Commands;
using STTestApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

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
                if (selectedWorker?.WorkerId == value?.WorkerId)
                    return;
                selectedWorker = value;
                OnPropertyChanged(nameof(SelectedWorker));
            }
        }

        /// <summary>
        /// Выделен ли работник из списка
        /// Влияет на CanExecute для команд
        /// </summary>
        public bool isWorkerSelected
        {
            get
            {
                return (SelectedWorker == null) ? false : true;
            }
        }
        
        #endregion

        #region Команды
        /// <summary>
        /// Запускает дочернее окно, позволяющее 
        /// произвести расчет зп выделенного сотрудника (SelectedWorker)
        /// </summary>
        public ICommand CheckSalaryCommand { get; private set; }
        public ICommand TESTCmd { get; private set; }

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

            //Команды
            CheckSalaryCommand = new RelayCommand(ShowSalaryWindow, CheckSelection);
            TESTCmd = new BaseCommand(ROFL);
        }
        #endregion



        #region Методы
        private void ROFL()
        {
            SelectedWorker = null;
        }
        /// <summary>
        /// Бессовестное нарушение паттерна MVVM
        /// </summary>
        private void ShowSalaryWindow()
        {
            var xd = new SalaryWindow(SelectedWorker);
            xd.ShowDialog();
        }
        /// <summary>
        /// Разрешениен а выполнение команд, связанных с выбором сотрудника из списка
        /// (если никого не выделено => запрет
        /// </summary>
        /// <returns>true если есть команду можно запускать</returns>
        private bool CheckSelection()
        {
            return isWorkerSelected;
        }

        #endregion

    }
}
