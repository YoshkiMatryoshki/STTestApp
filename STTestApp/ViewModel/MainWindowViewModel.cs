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
        public bool IsWorkerSelected
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
        /// <summary>
        /// Запускает дочернее окно, показывающее подчиненных выбранного сотрудника
        /// </summary>
        public ICommand ShowSubsCommand { get; private set; }

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
            ShowSubsCommand = new RelayCommand(ShowSubordinatesWindow, CheckSubSelection);

        }
        #endregion



        #region Методы
        /// <summary>
        /// Нарушаем MVVM, но запускаем окно с подчиненными
        /// </summary>
        private void ShowSubordinatesWindow()
        {
            var newWindow = new SubordinatesWindow(SelectedWorker);
            newWindow.ShowDialog();
        }
        /// <summary>
        /// Бессовестное нарушение паттерна MVVM
        /// </summary>
        private void ShowSalaryWindow()
        {
            var MVVMFriendlyWindow = new SalaryWindow(SelectedWorker);
            MVVMFriendlyWindow.ShowDialog();
        }
        /// <summary>
        /// Разрешениен а выполнение команд, связанных с выбором сотрудника из списка
        /// (если никого не выделено => запрет
        /// </summary>
        /// <returns>true если есть команду можно запускать</returns>
        private bool CheckSelection()
        {
            return IsWorkerSelected;
        }
        /// <summary>
        /// В отличие от CheckSelection заврещвает запуск команды просмотра подчиненных для Employee
        /// </summary>
        /// <returns>true если можно посмотреть подчиненных сотрудника</returns>
        private bool CheckSubSelection()
        {
            return IsWorkerSelected && !(SelectedWorker is Employee);
        }

        #endregion

    }
}
