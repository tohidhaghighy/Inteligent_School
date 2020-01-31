using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class TadrisshodehaService : ITadrisshodeha
    {
        private DatabaseContext _dbContext = null;
        PersianCalendar pc = new PersianCalendar();
        public List<DomainClasses.Tadrisshode> GetTadrisshodeha(int classid, int teacherid, int lessonid)
        {
            using (_dbContext = new DatabaseContext())
            {
                return _dbContext.Tadrisshodes.Where(a => a.Classid == classid && a.Teacherid == teacherid && a.Lessonid == lessonid).OrderByDescending(a=>a.Id).ToList();
            }
            return null;
        }

        public bool Addtadrisshodeha(int classid, int teacherid, int lessonid, string text, string date)
        {
            using (_dbContext = new DatabaseContext())
            {
                _dbContext.Tadrisshodes.Add(new Tadrisshode()
                {
                    Lessonid = lessonid,
                    Classid = classid,
                    Teacherid = teacherid,
                    Text = text,
                    Date = date,
                });
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }



        public List<DomainClasses.Tashvighi> Getalltashvighi(int studentid)
        {
            using (_dbContext = new DatabaseContext())
            {
                return _dbContext.Tashvighies.Where(a => a.Studentid==studentid).OrderByDescending(a => a.Id).ToList();
            }
            return null;
        }
        

        public Boolean addtashvighi(int studentid,string text)
        {
            using (_dbContext = new DatabaseContext())
            {
                string time = pc.GetYear(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now) + "/" + pc.GetDayOfMonth(DateTime.Now);
               _dbContext.Tashvighies.Add(new Tashvighi() { Date=time,Studentid=studentid,Text=text});
               _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
