using AspDotNetExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspDotNetExample.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDBAccessLayer empdb = new EmployeeDBAccessLayer();
        // GET: Employee
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //POST: Employee
        [HttpPost]
        public ActionResult Create([Bind] EmployeeEntities employeeEntities)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = empdb.AddEmployeeRecord(employeeEntities);
                    TempData["msg"] = resp;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            EmployeeEntities employeeEntities = empdb.GetEmployeeData(id);

            if (employeeEntities == null)
            {
                return HttpNotFound();
            }
            return View(employeeEntities);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind] EmployeeEntities emp)
        {
            if (ModelState.IsValid)
            {
                empdb.UpdateEmployee(emp);
                return RedirectToAction("Index");
            }
            return View(empdb);
        }
    }
}
