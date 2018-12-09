using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeTable.Models
{
    public class Position
    {
        /*
         Класс - должность сотрудника
             */

         /// <summary>
         /// Должность
         /// </summary>
        [Key]public int IdPosition { get; set; }
        /// <summary>
        /// Название должности
        /// </summary>
        [Required,Display (Name ="Должность")]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public Position()
        {
            Employees = new List<Employee>();
        }
    }
}