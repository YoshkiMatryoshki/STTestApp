using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace STTestApp.Model
{
    /// <summary>
    /// Для взаимодействия с Sqlite DB
    /// </summary>
    class WorkersContext : DbContext
    {
        #region Свойства
        public DbSet<WorkerGroup> WorkerGroups { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Salesman> Salesmans { get; set; }
        #endregion

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public WorkersContext()
        {

        }

        #region Методы
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Filename=Omegarofl.db");
        }
        #endregion


    }
}
