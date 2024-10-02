using Attendance.Models;
using Attendance.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Attendance.Controllers
{
    [Authorize(Roles ="Admin ,HR")]
    public class DepartmentController : Controller
    {
        IDepartmentService departmentService;

        public DepartmentController(IDepartmentService _departmentService)
        {
            departmentService = _departmentService;
        }
        // GET: DepartmentController
        public ActionResult Index()
        {
            List<Department> departments = departmentService.GetAll();
            return View(departments);
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {

            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            department.Active = true;
            if (ModelState.IsValid)
            {
                try
                {
                    departmentService.Add(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }



            }

            return View("Create");


        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            Department model = departmentService.GetById(id);
            return View(model);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        public ActionResult Edit( Department department)
        {
            


            if (ModelState.IsValid)
            {

                departmentService.Update(department);
                return RedirectToAction(nameof(Index));


            }

            return View("Edit");



        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            // var dept=departmentService.GetById(id);
            departmentService.Delete(id);
            return RedirectToAction("index");
        }

      

    }

}
