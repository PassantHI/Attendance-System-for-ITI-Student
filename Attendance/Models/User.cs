using System.ComponentModel.DataAnnotations;

namespace Attendance.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }
        [Required]

        public string UserPassword { get; set; }
        [Required]

        public string Role { get; set; }
    }
}
