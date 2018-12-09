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
        // GET: Create
        /*
         * Контроллер создания новой записи об отсутствии
         */
        public ActionResult Index()
        {
            var model = new Absence();
            model.Date = DateTime.Now;
            return View(model);
        }
    }
}