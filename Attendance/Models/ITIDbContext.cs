using Microsoft.EntityFrameworkCore;

namespace Attendance.Models
{
    public class ITIDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<HR> HRs { get; set; }

        public DbSet<StdAttend> StdAttends { get; set; }
        public DbSet<AttendanceTable> Attendances { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerifyStudent>verifyStudents { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.\\sqlexpress;Database=ITIAttendance;Integrated Security=sspi;TrustServerCertificate=true;Encrypt=true;");
            base.OnConfiguring(optionsBuilder);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttendanceTable>(a =>
            {
                a.HasKey(a => new { a.StdId, a.Date });

            });



            
            base.OnModelCreating(modelBuilder);
        }





    }
}
