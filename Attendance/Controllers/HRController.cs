using Attendance.Models;
using Attendance.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Attendance.Controllers
{
    [Authorize(Roles = "Admin")]

    public class HRController : Controller
    {
        IHRService HRService;

        public HRController(IHRService _hrService)
        {
            HRService = _hrService;
            
        }
        // GET: HRController
        public ActionResult Index()
        {
            List<HR> model=HRService.GetAll();
            
            return View(model);
        }

        // GET: HRController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HRController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HR hr)
        {
            hr.Active = true;
            if (ModelState.IsValid) 
            {
                try
                {
                    HRService.Add(hr);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }
           
            return View();
        }

        // GET: HRController/Edit/5
        public ActionResult Edit(int id)
        {
            var hr=HRService.GetById(id);
            return View(hr);
        }

        // POST: HRController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HR hr)
        {
            hr.Active=true; 
            if (ModelState.IsValid) 
            {
                try
                {
                    HRService.Update(hr);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }



            }
            return View();
           
        }

        // GET: HRController/Delete/5
        public ActionResult Delete(int id)
        {
            HRService.Delete(id);
            return RedirectToAction(nameof(Index));

        }

        //// POST: HRController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
