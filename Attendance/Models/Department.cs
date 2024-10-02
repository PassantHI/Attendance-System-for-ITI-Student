using System.ComponentModel.DataAnnotations;

namespace Attendance.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public bool Active { get; set; }


        public virtual List<Student> Students { get; set; } = new List<Student>();




    }
}
