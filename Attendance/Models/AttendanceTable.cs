using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance.Models
{
    public class AttendanceTable
    {
        
        
        [ForeignKey("StdAttend")]
        public int StdId { get; set; }
        public string StdName { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }


        public Student student{ get; set; }
    }
}
