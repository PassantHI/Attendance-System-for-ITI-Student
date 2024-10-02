using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance.Models
{
    public class HR
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(2)]
        public string Name { get; set; }
        public int? Age {  get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z]+.[a-zA-Z]{2,4}")]

        public string Email {  get; set; }
        [Required]
        public string Password {  get; set; }
        [Compare("Password")]
        [NotMapped]
        public string CPassword {  get; set; }
        public bool Active { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }








    }
}
