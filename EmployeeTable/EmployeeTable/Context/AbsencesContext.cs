using EmployeeTable.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeTable.Context
{
    public class AbsencesContext : DbContext
    {
        /*
         * Контекст данных
         * DBModel - подключение описано в web.config
         */

        public AbsencesContext() : base("name = DBModel")
        { }
        /// <summary>
        /// Отсутствия
        /// </summary>
        public DbSet<Absence> Absences { get; set; }
        /// <summary>
        /// Сотрудники
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
        /// <summary>
        /// Должности
        /// </summary>
        public DbSet<Position> Positions { get; set; }
    }
}