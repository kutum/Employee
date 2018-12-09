using EmployeeTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeTable.Controllers
{
    public class NewempController : Controller
    {
        // GET: Newemp
        public ActionResult Index()
        {
            var model = new Employee();
            return View(model);
        }
    }
}