using Attendance.Models;

namespace Attendance.Service
{
    public class HRService : IHRService
    {
        ITIDbContext db = new ITIDbContext();

        public List<HR> GetAll()
        {

            return db.HRs.ToList();
        }
        public HR GetById(int id)
        {
            return db.HRs.SingleOrDefault(a => a.Id == id);
        }
        public void Add(HR hr)
        {
            User userhr = new User() 
            { 
                UserEmail = hr.Email,
                UserPassword = hr.Password,
                UserName = hr.Name,
                Role="HR"
                
            
            };
            db.Users.Add(userhr);
            db.SaveChanges();
            hr.UserId=userhr.UserId;
            db.HRs.Add(hr);
            db.SaveChanges();
        }
        public void Update(HR hr)
        {
            db.HRs.Update(hr);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var hr = db.HRs.SingleOrDefault(a => a.Id == id);
            hr.Active= false;
            //db.HRs.Remove(hr);
            db.SaveChanges();
        }
    }
}
