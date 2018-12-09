using EmployeeTable.Context;
using EmployeeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeTable.Controllers
{
    public class ModifyController : Controller
    {
        AbsencesContext db = new AbsencesContext();

        // GET: Modify
        public ActionResult Index(int id)
        {
            var absences = db.Absences.Where(x => x.IdAbsences == id);
            return View(absences);
        }
    }
}