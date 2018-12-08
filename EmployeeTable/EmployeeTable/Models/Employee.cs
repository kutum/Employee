using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeTable.Models
{
    public class Employee
    {

        [Key]public int idEmployee { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string Fullname { get; set; }
        public ICollection<Absence> Absences { get; set; }

        public Employee()
        {
            Absences = new List<Absence>();
        }
    }
}