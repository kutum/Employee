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
        public AbsencesContext() : base("name = DBModel")
        { }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}