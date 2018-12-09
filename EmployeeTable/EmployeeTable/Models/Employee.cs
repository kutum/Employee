using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeTable.Models
{
    public class Employee
    {
        /*
         Класс - сотрудник
         */
         /// <summary>
         /// id Сотрудника
         /// </summary>
        [Key]public int IdEmployee { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required, Display(Name = "Фамилия")]
        public string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Required, Display(Name = "Имя")]
        public string Name { get; set; }
        /// <summary>
        /// Отчёство
        /// </summary>
        [Required, Display(Name = "Отчество")]
        public string Lastname { get; set; }
        /// <summary>
        /// Фамилия и инициалы
        /// </summary>
        [Required, Display(Name = "ФИО")]
        public string Fullname { get; set; }

        /// <summary>
        /// id Должности
        /// </summary>
        public int idPosition { get; set; }
        public Position Position { get; set; }

        public ICollection<Absence> Absences { get; set; }

        public Employee()
        {
            Absences = new List<Absence>();
        }
    }
}