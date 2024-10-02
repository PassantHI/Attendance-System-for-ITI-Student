﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Models
{
    public class VerifyStudent
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(2)]
        public string Name { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z]+.[a-zA-Z]{2,4}")]
        public string Email { get; set; }
        public int? Age { get; set; }
        [ForeignKey("Department")]

        public int DeptId { get; set; }

        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [NotMapped]
        public string CPassword { get; set; }


        public Department Department { get; set; }

       

    }
}
