using Attendance.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Service
{
    public class StudentService:IStudentService
    {

        ITIDbContext db=new ITIDbContext();

        public List<Student> GetAll() 
        {
            return db.Students.Include(a=>a.Department).Where(a=>a.Active==true).ToList();
        }

        public Student GetById(int id) 
        {
            return db.Students.Include(a=>a.Department).FirstOrDefault(a=>a.Id==id);
        
        }
        public Student GetByEmail(string email)
        {
            return db.Students.FirstOrDefault(a=>a.Email==email);

        }

        public void Add(Student student) 
        {
            User userstd = new User() 
            {
                UserName=student.Name ,
                UserEmail=student.Email ,
                UserPassword=student.Password,
                Role="Student"
                
            };
            db.Users.Add(userstd);
            db.SaveChanges();

            student.UserId=userstd.UserId;
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void Update(Student student) 
        {
            //var std=db.Students.FirstOrDefault(a=>a.Id == student.Id);
            //student.UserId=std.UserId;
            db.Students.Update(student);
            db.SaveChanges();
        
        }

        public void Delete(int id) 
        {
            var student = db.Students.FirstOrDefault(x=>x.Id==id);
            student.Active=false;
            db.SaveChanges();
        
        }


        

    }
}
