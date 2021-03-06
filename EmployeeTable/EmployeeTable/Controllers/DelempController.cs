﻿using EmployeeTable.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeTable.Controllers
{
    public class DelempController : Controller
    {
        AbsencesContext db = new AbsencesContext();

        // GET: Delemp
        /*
         * Контроллер удаления сотрудника
         */

        public ActionResult Index(int id)
        {
            var employees = db.Employees.Where(x => x.IdEmployee == id);
            return View(employees);
        }
    }
}