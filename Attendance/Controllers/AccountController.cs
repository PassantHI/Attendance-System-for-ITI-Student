using Attendance.Models;
using Attendance.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Attendance.Controllers
{
    public class AccountController : Controller
    {
        ITIDbContext db=new ITIDbContext();

        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            var depts = db.Departments.ToList();
            SelectList deptlist = new SelectList(depts, "DeptId", "DeptName");
            ViewBag.deptlist = deptlist;
            return View();

        }

        [HttpPost]

        public IActionResult Register(VerifyStudent std)
        {
            var verify=db.verifyStudents.FirstOrDefault(a=>a.Email==std.Email);
            if (verify != null)
            {
                ViewBag.Message = "exist";

                return View();

            }

            if (ModelState.IsValid)
            {

                db.verifyStudents.Add(std);
                db.SaveChanges();
                if (std.ImageFile == null)
                    return BadRequest();
                string extension = std.ImageFile.FileName.Split('.').Last();
                string imgname = $"{std.Id}Verify.{extension}";
                string filepath = "wwwroot/images/" + $"{imgname}";
                using (FileStream st = new FileStream(filepath, FileMode.Create))
                {
                    std.ImageFile.CopyTo(st);


                }

                std.ImageName = imgname;
                db.verifyStudents.Update(std);
                db.SaveChanges();


                ViewBag.Message = "success";

                return View();
            }

            return Content("error");


        }
        public IActionResult Login()
        {

            return View();

        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginViewModel loginViewModel)
        {
            var userlogin = db.Users.FirstOrDefault(a=>a.UserEmail==loginViewModel.UserEmail && a.UserPassword==loginViewModel.Password);

            if (userlogin == null) 
            {
                return RedirectToAction("register");

            }

            loginViewModel.Role=userlogin.Role;
            Claim c1 = new Claim(ClaimTypes.Name,loginViewModel.UserEmail);
            Claim c2 = new Claim(ClaimTypes.Role, loginViewModel.Role);


            ClaimsIdentity ci1 = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            ci1.AddClaim(c1);
            ci1.AddClaim(c2);

            ClaimsPrincipal cp = new ClaimsPrincipal(ci1);

            await HttpContext.SignInAsync(cp); //add in cookie

            return RedirectToAction("index", "home");

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("index", "home");

        }





    }
}
