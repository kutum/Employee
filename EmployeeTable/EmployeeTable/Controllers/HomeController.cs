using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI.WebControls;
using EmployeeTable.Context;
using System.Threading.Tasks;
using EmployeeTable.Models;

namespace EmployeeTable.Contollers
{
    public class HomeController : Controller
    {
        AbsencesContext db = new AbsencesContext();

        public ActionResult Index()
        {

            var absences = db.Absences.Include(f => f.Employee);

            return View(absences.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            SelectList surnames = new SelectList(db.Employees, "idEmployee", "Fullname");
            ViewBag.Employees = surnames;

            return View();
        }
        [HttpPost]
        public ActionResult Create(Absence absence)
        {
            db.Absences.Add(absence);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Absence absence = db.Absences.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Absence absence = db.Absences.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            db.Absences.Remove(absence);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}