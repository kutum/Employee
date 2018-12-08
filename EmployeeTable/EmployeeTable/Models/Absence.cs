using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EmployeeTable.Models
{
    public class Absence
    {
        [Key]public int IdAbsences { get; set; }

        [DataType(DataType.DateTime), Required]
        //[DisplayFormat(DataFormatString = "yyyy/MM/dd", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Reason { get; set; }

        public int idEmployee { get; set; }
        public Employee Employee { get; set; }
    }

    

}