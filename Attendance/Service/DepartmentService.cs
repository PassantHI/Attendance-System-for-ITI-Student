using Attendance.Models;

namespace Attendance.Service
{
    public class DepartmentService:IDepartmentService
    {
        ITIDbContext db = new ITIDbContext();

        public List<Department> GetAll()
        {

            return db.Departments.Where(a => a.Active == true).ToList();
        }
        public Department GetById(int id)
        {
            return db.Departments.SingleOrDefault(a => a.DeptId == id);
        }
        public void Add(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }
        public void Update(Department department)
        {
            db.Departments.Update(department);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var dept = db.Departments.SingleOrDefault(a => a.DeptId == id);
            dept.Active = false;
            //db.Departments.Remove(dept);
            db.SaveChanges();
        }
    }
}
