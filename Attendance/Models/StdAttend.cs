using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance.Models
{
    public class StdAttend
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StdId {  get; set; }
        public string StdName { get; set; }

        public ICollection<AttendanceTable> Attendances { get; set; }




    }
}
