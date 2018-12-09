using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EmployeeTable.Models
{
    public class Absence
    {
        /*
         Класс, описывающий сущность записи об отсутствии
        */

        /// <summary>
        /// id Отсутствия - уникальное число
        /// </summary>
        [Key]public int IdAbsences { get; set; }
        /// <summary>
        /// id Отсутствия - уникальное число
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}", ApplyFormatInEditMode = true),
        Required, DataType(DataType.DateTime), Display(Name = "Дата")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Причина отсутствия
        /// </summary>
        [Required, Display(Name = "Причина")]
        public string Reason { get; set; }
        /// <summary>
        /// id Сотрудника
        /// </summary>
        public int idEmployee { get; set; }
        public Employee Employee { get; set; }
    }

    

}