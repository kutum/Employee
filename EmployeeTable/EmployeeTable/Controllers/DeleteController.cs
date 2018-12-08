using EmployeeTable.Context;
using EmployeeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeTable.Controllers
{
    public class DeleteController : Controller
    {
        AbsencesContext db = new AbsencesContext();

        // GET: DeleteConfirmed
        public ActionResult Index(int id)
        {
            var absences = db.Absences.Where(x => x.idEmployee == id);
            return View(absences);
        }
    }
}