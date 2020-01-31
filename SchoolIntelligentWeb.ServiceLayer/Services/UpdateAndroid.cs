using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class UpdateAndroid
    {
        private DatabaseContext _dbContext = null;

        public Boolean addversion(string description,int type,string url,int versioncode)
        {
            using (_dbContext=new DatabaseContext())
            {
                _dbContext.UpdateApp.Add(new DomainClasses.UpdateApp()
                {
                    datetime=DateTime.Now.ToString(),
                    description=description,
                    type=type,
                    url=url,
                    versioncode=versioncode
                });
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

       
        public UpdateApp checkversion(int versioncode,int type)
        {
            using (_dbContext = new DatabaseContext())
            {
                var find = _dbContext.UpdateApp.FirstOrDefault(a => a.versioncode > versioncode && a.type==type);
                return find;
            }
        }
        
    }
}
