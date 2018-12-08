using EmployeeTable.Context;
using EmployeeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeTable.Controllers
{
    public class CreateController : Controller
    {
        AbsencesContext db = new AbsencesContext();

        // GET: Create
        
        public ActionResult Create()
        {
            var model = new Absence();
            model.Date = DateTime.Now;
            return View(model);
        }
    }
}