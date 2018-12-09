﻿using System;
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

        
        public ActionResult Employees()
        {
            return View(db.Employees);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SelectList Fullnames = new SelectList(db.Employees, "idEmployee", "Fullname");
            ViewBag.Employees = Fullnames;

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

        [HttpGet]
        public ActionResult Newemp()
        {
            return View();
        }
        [HttpPost, ActionName("Newemp")]
        public ActionResult Newemp(Employee employee)
        {
            db.Employees.Add(employee);
            employee.Fullname = employee.Surname + " " +
                                employee.Name.Substring(0, 1) + "." +
                                employee.Lastname.Substring(0, 1) + ".";

            db.SaveChanges();
            return RedirectToAction("Employees");
        }

        [HttpGet]
        public ActionResult Delemp(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost, ActionName("Delemp")]
        public ActionResult DelempConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Employees");
        }

        [HttpGet]
        public ActionResult Modify(int? id)
        {
            SelectList Fullnames = new SelectList(db.Employees, "idEmployee", "Fullname");
            ViewBag.Employees = Fullnames;

            if (id == null)
            {
                return HttpNotFound();
            }
            Absence absence = db.Absences.Find(id);
            if (absence != null)
            {
                return View(absence);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Modify(Absence absence)
        {
            db.Entry(absence).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ModifyEmp(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                return View(employee);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult ModifyEmp(Employee employee)
        {
            employee.Fullname = employee.Surname + " " +
                                employee.Name.Substring(0, 1) + "." +
                                employee.Lastname.Substring(0, 1) + ".";

            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Employees");
        }
    }
}