using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeTable.Models
{
    public class AbsenceListViewModel
    {
        public IEnumerable<Absence> Absences { get; set; }
        public SelectList Fullnames { get; set; }
        public SelectList Position { get; set; }
        public SelectList Reason { get; set; }
    }
}