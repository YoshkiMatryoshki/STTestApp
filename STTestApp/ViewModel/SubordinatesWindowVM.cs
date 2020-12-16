using STTestApp.Model;
using STTestApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace STTestApp.ViewModel
{
    class SubordinatesWindowVM : WorkerBasedViewModel
    {
        #region Св-ва
        /// <summary>
        /// Список подчиненных
        /// </summary>
        private IEnumerable<Worker> subordinates;
        public IEnumerable<Worker> Subordinates
        {
            get => subordinates;
            set
            {
                subordinates = value;
                OnPropertyChanged(nameof(Subordinates));
            }
        }

        /// <summary>
        /// Показаны все подчиненные - true
        /// Только подчиненные первого уровня - false
        /// Отвечает за переключение кнопок
        /// </summary>
        private bool allSubsShowed;
        public bool AllSubsShowed
        {
            get => allSubsShowed;
            set
            {
                if (allSubsShowed == value)
                    return;
                allSubsShowed = value;
                OnPropertyChanged(nameof(AllSubsShowed));
            }
        }

        #endregion


        #region Команды
        /// <summary>
        /// Отображает всех подчиненных всех уровней
        /// </summary>
        public ICommand ShowAllSubs { get; private set; }
        /// <summary>
        /// Отображает работников в непосредственном подчинении
        /// </summary>
        public ICommand ShowOnlyFans { get; private set; }
        #endregion

        #region Конструктор
        /// <summary>
        /// Заряжает дополнительные команды для отображения разных уровней подчиненных
        /// </summary>
        /// <param name="worker"></param>
        public SubordinatesWindowVM(Worker worker) : base(worker)
        {
            Subordinates = worker.Subordinates;
            AllSubsShowed = false;
            //команды
            ShowOnlyFans = new RelayCommand(ShowDirectSubordinates, CheckDirSubsMode);
        }
        #endregion


        #region Методы

        /// <summary>
        /// Заполняет VM.Subordinates только подчиненными первого уровня
        /// </summary>
        private void ShowDirectSubordinates()
        {
            Subordinates = worker.Subordinates;
            AllSubsShowed = false;
        }
        /// <summary>
        /// Заполняет VM.Subordinates подчиненными всех уровней
        /// </summary>
        private void ShowAllSubordinates()
        {

            AllSubsShowed = true;
        }
        /// <summary>
        /// Определяет текущий режим отображения подчиненных
        /// </summary>
        /// <returns>false - все подчиненные, true - только онлифанс</returns>
        private bool CheckAllSubsMode()
        {
            return !AllSubsShowed;
        }
        /// <summary>
        /// Определяет текущий режим отображения подчиненных
        /// </summary>
        /// <returns>true - все подчиненные, false - только онлифанс</returns>
        private bool CheckDirSubsMode()
        {
            return AllSubsShowed;
        }
        #endregion
    }
}
