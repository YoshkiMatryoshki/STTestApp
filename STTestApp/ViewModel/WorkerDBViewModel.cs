using STTestApp.Model;
using STTestApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace STTestApp.ViewModel
{
    /// <summary>
    /// VM для добавления и редактирования информации о сотруднике
    /// </summary>
    class WorkerDBViewModel : BaseViewModel
    {
        #region Св-ва
        /// <summary>
        /// Новый работник или работник с измененными атрибутами
        /// </summary>
        public Worker EditedWorker { get; private set; }


        private string workerName;
        /// <summary>
        /// Имя работника
        /// </summary>
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

        private WorkerGroup selectedWorkerGroup;
        /// <summary>
        /// Выбранная из списка группа работников
        /// </summary>
        public WorkerGroup SelectedWorkerGroup
        {
            get => selectedWorkerGroup;
            set
            {
                if (selectedWorkerGroup == value)
                    return;
                selectedWorkerGroup = value;
                OnPropertyChanged(nameof(SelectedWorkerGroup));
            }
        }

        private IEnumerable<WorkerGroup> allWorkerGroups;
        /// <summary>
        /// Доступные для выбора группы сотрудников
        /// </summary>
        public IEnumerable<WorkerGroup> AllWorkerGroups
        {
            get => allWorkerGroups;
            set
            {
                allWorkerGroups = value;
                OnPropertyChanged(nameof(AllWorkerGroups));
            }
        }

        private DateTime hiringDate;
        /// <summary>
        /// Дата вербовки сотрудника
        /// </summary>
        public DateTime HiringDate
        {
            get => hiringDate;
            set
            {
                if (hiringDate == value)
                    return;
                hiringDate = value;
                OnPropertyChanged(nameof(HiringDate));
            }
        }

        private Worker boss;
        /// <summary>
        /// Сотрудник, выбранный в качестве начальника
        /// </summary>
        public Worker Boss
        {
            get => boss;
            set
            {
                if (boss == value)
                    return;
                boss = value;
                OnPropertyChanged(nameof(Boss));
            }
        }

        private IEnumerable<Worker> bossCandidates;
        /// <summary>
        /// Список сотрудников, которых можно назначить в качестве начальника.
        /// </summary>
        public IEnumerable<Worker> BossCandidates
        {
            get => bossCandidates;
            set
            {
                bossCandidates = value;
                OnPropertyChanged(nameof(BossCandidates));
            }
        }

        #endregion

        #region Команды
        public ICommand AddWorkerCommand { get; private set; } 

        #endregion

        #region Конструктор
        /// <summary>
        /// Дефолтный конструктор для добавления нового сотрудника
        /// </summary>
        public WorkerDBViewModel()
        {
            HiringDate = DateTime.Today;
            LoadFromDB();
            AddWorkerCommand = new RelayCommand(AddRecord, CheckInputs);
        }

        #endregion

        #region Методы
        /// <summary>
        /// ПОдгрузка из БД рабочих групп и потенциальныъх начальников и подчиненных
        /// </summary>
        private void LoadFromDB()
        {
            using (var db = new WorkersContext())
            {
                AllWorkerGroups = db.WorkerGroups.ToList();
                ///Employee не может иметь сотрудников в подчинении (is not в C# 9)
                BossCandidates = db.Workers.Where(worker => !(worker is Employee)).ToList();
            }
        }
        /// <summary>
        /// Проверка заполненности всхе полей перед разрешением добавить сотрудника в Дб
        /// </summary>
        /// <returns>true - если "поехали"</returns>
        private bool CheckInputs()
        {
            //Начальник может быть null
            return (WorkerName != "" && WorkerName != null)
                && SelectedWorkerGroup != null && HiringDate != null;
        }
        /// <summary>
        /// Сброс всех заполненных полей после добавления
        /// </summary>
        private void DropInputs()
        {
            WorkerName = null;
            EditedWorker = null;
            SelectedWorkerGroup = null;
            Boss = null;
            HiringDate = DateTime.Today;
        }
        /// <summary>
        /// Добавляет запись о новом сотруднике
        /// </summary>
        private void AddRecord()
        {
            EditedWorker = (SelectedWorkerGroup.GroupName) switch
            {
                "Employee" => new Employee(SelectedWorkerGroup),
                "Manager" => new Manager(SelectedWorkerGroup),
                "Salesman" => new Salesman(SelectedWorkerGroup),
                _ => throw new ArgumentException("Неизвестная группа пользователей", nameof(SelectedWorkerGroup))
            };

            if (Boss != null)
                EditedWorker.BossId = this.Boss.WorkerId;
            EditedWorker.WorkerName = WorkerName;
            EditedWorker.HiringDate = HiringDate;

            try
            {
                using(var db = new WorkersContext())
                {
                    db.Workers.Add(EditedWorker);
                    db.SaveChanges();
                }
                //var res = MessageBox.Show("Сотрудник добавлен");
            }
            catch (Exception e)
            {
                throw new Exception($"WTF? {e}");
            }
            DropInputs();


        }
        #endregion
    }
}
