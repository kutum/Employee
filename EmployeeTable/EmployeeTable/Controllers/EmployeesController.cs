using EmployeeTable.Context;
using EmployeeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeTable.Controllers
{
    public class EmployeesController : Controller
    {
        AbsencesContext db = new AbsencesContext();

        // GET: Employees

        /*
         * Контроллер сотрудники
         */

        public ActionResult Index(int? id)
        {
            var employee  = db.Employees.Where(x => x.IdEmployee == id);
            return View(employee);
        }
    }
}