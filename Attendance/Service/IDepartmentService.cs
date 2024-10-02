using Attendance.Models;

namespace Attendance.Service
{
    public interface IDepartmentService
    {
        public List<Department> GetAll();
        public void Add(Department department);
        public void Update(Department department);
        public void Delete(int id);
        public Department GetById(int id);
    }
}
