﻿using EmployeeTable.Context;
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
        public ActionResult Index()
        {
            return View();
        }
    }
}