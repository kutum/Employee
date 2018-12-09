using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeTable.Controllers
{
    public class ModifyEmpController : Controller
    {
        // GET: ModifyEmp
        public ActionResult Index()
        {
            return RedirectToRoute("Home", "Index");
        }
    }
}