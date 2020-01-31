using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{

    public class TokenService
    {
        private DatabaseContext _dbContext = null;
        public Boolean Addtokenfcm(string fcm,int type,int peopleid)
        {
            using (_dbContext=new DatabaseContext())
            {
                var findtoken = _dbContext.Settings.FirstOrDefault(a => a.Token == fcm && a.Type==type && a.PeopleId==peopleid);
                if (findtoken==null)
                {
                    _dbContext.Settings.Add(new DomainClasses.Setting()
                    {
                        PeopleId=peopleid,
                        Token=fcm,
                        Type=type
                    });
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        public List<MemorizeMassage> listmassege(int studentid)
        {
            List<MemorizeMassage> allmessage = new List<MemorizeMassage>();
            using (_dbContext = new DatabaseContext())
            {
                var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == studentid);
                if (findstudent!=null)
                {
                    return _dbContext.MemorizeMassage.Where(a => a.Type == 2 || (a.Ref_Id == studentid && a.Type==0) || (a.Ref_Id == findstudent.ClassId && a.Type==1)).ToList();
                }
            }
            return allmessage;
        }
    }
}
