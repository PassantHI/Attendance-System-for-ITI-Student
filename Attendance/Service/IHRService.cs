using Attendance.Models;

namespace Attendance.Service
{
    public interface IHRService
    {
        public List<HR> GetAll();

        public HR GetById(int id);

        public void Add(HR hr);

        public void Update(HR hr);

        public void Delete(int id);
    }
}
