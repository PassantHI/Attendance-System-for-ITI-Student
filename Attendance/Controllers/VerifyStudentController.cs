using Attendance.Models;
using Attendance.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attendance.Controllers
{
    [Authorize(Roles = "Admin ,HR")]

    public class VerifyStudentController : Controller
    {
        ITIDbContext db=new ITIDbContext();
        // GET: VerifyStudent
        IStudentService studentService;

        public VerifyStudentController(IStudentService _studentService)
        {
            studentService = _studentService;
            
        }
        public ActionResult Index()
        {
            var model =db.verifyStudents.ToList();
            return View(model);
        }

        // GET: VerifyStudent/Details/5
        public ActionResult Details(int id)
        {
            var std=db.verifyStudents.FirstOrDefault(a=>a.Id==id);
            return View(std);
        }

        // GET: VerifyStudent/Create
        
        public IActionResult Verify(int id)
        {
            var verifystudent=db.verifyStudents.FirstOrDefault(a=>a.Id==id);

            Student student = new Student()
            {
                Name = verifystudent.Name,
                Age = verifystudent.Age,
                Email = verifystudent.Email,
                Password = verifystudent.Password,
                CPassword = verifystudent.Password,
                ImageName = verifystudent.ImageName,
                DeptId = verifystudent.DeptId,
                Active = true


            };
            studentService.Add(student);

            //if (student.ImageFile == null)
            //    return BadRequest();
            //string extension = student.ImageFile.FileName.Split('.').Last();
            //string imgname = $"{student.Id}.{extension}";
            //string filepath = "wwwroot/images/" + $"{imgname}";
            //using (FileStream st = new FileStream(filepath, FileMode.Create))
            //{
            //    student.ImageFile.CopyTo(st);


            //}

            //student.ImageName = imgname;
            //studentService.Update(student);

            db.verifyStudents.Remove(verifystudent);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

      
        public ActionResult Delete(int id)
        {
            var std =db.verifyStudents.FirstOrDefault(a=>a.Id==id);
            db.verifyStudents.Remove(std);
            db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
