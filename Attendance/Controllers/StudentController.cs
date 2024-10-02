using Attendance.Models;
using Attendance.Service;
using DinkToPdf;
using DinkToPdf.Contracts;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Attendance.Controllers
{
    [Authorize(Roles = "Admin ,HR,Student")]

    public class StudentController : Controller
    {
        ITIDbContext db = new ITIDbContext();
        IStudentService studentService;
        IDepartmentService departmentService;
        private readonly IConverter pdfConverter;
        private readonly ICompositeViewEngine viewEngine;
        //private readonly ITempDataProvider _tempDataProvider;
        //private readonly IServiceProvider _serviceProvider;


        public StudentController(IStudentService _studentService, IDepartmentService _departmentService , IConverter _pdfconverter, ICompositeViewEngine _viewEngine  )
        {
            studentService = _studentService;
            departmentService = _departmentService;
            pdfConverter = _pdfconverter;
            this.viewEngine = _viewEngine;
        }

        [Authorize(Roles = "Admin, HR, Student")] 
        public ActionResult Index()
        {
           List<Student> stds= studentService.GetAll();
            return View(stds);
        }

        [Authorize(Roles = "Admin, HR, Student")] 

        public ActionResult Details(int id)
        {
            var model=studentService.GetById(id);

            return View(model);
        }

        [Authorize(Roles = "Admin, HR")]

        // GET: StudentController/Create
        public ActionResult Create()
        {
            var depts=departmentService.GetAll();
            SelectList deptlist=new SelectList(depts ,"DeptId" ,"DeptName");
            ViewBag.deptlist=deptlist;
            
            return View();
        }
        [Authorize(Roles = "Admin, HR")]


        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            student.Active= true;
            if (ModelState.IsValid) 
            {
                studentService.Add(student);
                if(student.ImageFile==null)
                    return BadRequest();
                string extension=student.ImageFile.FileName.Split('.').Last();
                string imgname = $"{student.Id}.{extension}";
                string filepath = "wwwroot/images/" +$"{imgname}";
                using(FileStream st=new FileStream(filepath , FileMode.Create))
                {
                    student.ImageFile.CopyTo(st);


                }

                student.ImageName= imgname;
                studentService.Update(student);
                return RedirectToAction(nameof(Index));




            }
            var depts = departmentService.GetAll();
            SelectList deptlist = new SelectList(depts, "DeptId", "DeptName");
            ViewBag.deptlist = deptlist;

            return View();

            
           
        }
        [Authorize(Roles = "Admin, HR")]

        public IActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult UploadFile(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (file != null&&file.Length>0)
            {
                string filename=file.FileName;
                string filePath = "wwwroot/Uploads/" +$"{filename}";
                using (FileStream exfil=new FileStream(filePath , FileMode.Create))
                {
                    file.CopyTo(exfil);
                }

                using (var stream =System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        
                        do
                        {
                            bool skiprow=false;
                            while (reader.Read())
                            {
                                if(!skiprow)
                                { skiprow = true; continue; }


                                Student std = new Student();
                                std.Name=reader.GetValue(0).ToString();
                                std.Email=reader.GetValue(1).ToString();
                                std.Age=Convert.ToInt32(reader.GetValue(2).ToString());
                                std.DeptId=Convert.ToInt32(reader.GetValue(3).ToString());
                                std.ImageName = reader.GetValue(4).ToString();
                                std.Password = reader.GetValue(5).ToString();
                                // std.CPassword = reader.GetValue(6).ToString();
                                std.Active = Convert.ToBoolean(reader.GetValue(6).ToString());
                                studentService.Add(std);
                               // db.Add(std);
                                //db.SaveChanges();




                            }
                        } while (reader.NextResult());

                        ViewBag.Message = "success";

                        
                    }
                }


            }

            return View();
        }

        [Authorize(Roles = "Admin ,HR,Student")]

        public IActionResult TakeAttendance(int id) 
        {
            var std =db.Attendances.FirstOrDefault(a=>a.StdId==id&&a.Date==DateTime.Today);

            if (std != null)
            {
                ViewBag.Message = "You Already Recorded Your Attendance For Today ";
                return View(new AttendanceTable { StdId = id, Date = DateTime.Today });
            
            }

            return View(new AttendanceTable { StdId = id, Date = DateTime.Today });
        
        }


        [Authorize(Roles = "Admin ,HR,Student")]

        [HttpPost]
        public IActionResult TakeAttendance(AttendanceTable attendanceTable)
        {
            var attenstd=db.Attendances.FirstOrDefault(a=>a.StdId == attendanceTable.StdId&&a.Date==DateTime.Today);
            if (attenstd != null) 
            {
                ViewBag.Message = "You Already Recorded Your Attendance For Today ";
                return View();
            }
            else
            {
                var student = studentService.GetById(attendanceTable.StdId);
                if (student == null) { return BadRequest("student not found"); }
                var attend = new AttendanceTable()
                {
                    StdId = student.Id,
                    Date = DateTime.Today,
                    StdName = student.Name,
                    IsPresent = true
                };

                db.Attendances.Add(attend);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = student.Id });

            }
        }

        public IActionResult StudentCardAC(int id)
        {
            var student=studentService.GetById(id);
            if(student!=null)
            {
                var htmlContent = RenderViewToStringAsync("StudentCardAC", student).Result;

                System.IO.File.WriteAllText("studentCard.html", htmlContent);

                // Create the PDF document
                var pdfDoc = new HtmlToPdfDocument()
                {
                    GlobalSettings = new GlobalSettings
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Out = "StudentCard.pdf" // Set output file path if saving to file
                    },
                    Objects = {
                new ObjectSettings
                {
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
                };

                var pdf = pdfConverter.Convert(pdfDoc);

                // Return the PDF as a file download
                return File(pdf, "application/pdf", "StudentCard.pdf");
            
        }

            return BadRequest("student not found");


        }

        private async Task<string> RenderViewToStringAsync(string StudentCardAC, object model)
        {
            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = viewEngine.FindView(ControllerContext, StudentCardAC, false);

                if (!viewResult.Success)
                {
                    throw new FileNotFoundException($"View '{StudentCardAC}' not found.");
                }

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }


        public ActionResult Edit(int id)
        {
            Student model=studentService.GetById(id);
            var depts = departmentService.GetAll();
            SelectList deptlist = new SelectList(depts, "DeptId", "DeptName");
            ViewBag.deptlist = deptlist;

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
             student.Active= true;
            if (ModelState.IsValid) 
            {
                if (student.ImageFile == null)
                    return BadRequest();
                string extension = student.ImageFile.FileName.Split('.').Last();
                string imgname = $"{student.Id}.{extension}";
                string filepath = "wwwroot/images/" + $"{imgname}";
                using (FileStream st = new FileStream(filepath, FileMode.Create))
                {
                    student.ImageFile.CopyTo(st);


                }

                student.ImageName = imgname;
                studentService.Update(student);
                return RedirectToAction(nameof(Index));

            }

            Student model = studentService.GetById(student.Id);
            var depts = departmentService.GetAll();
            SelectList deptlist = new SelectList(depts, "DeptId", "DeptName");
            ViewBag.deptlist = deptlist;

            return View(model);
        }


        public ActionResult Delete(int id)
        {
            studentService.Delete(id);
            return RedirectToAction(nameof(Index));


        }

    }
}
