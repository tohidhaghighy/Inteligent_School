using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class EmtehanatService:IEmtehanat
    {
        private DatabaseContext _dbContext = null;
        public List<Emtehanat> GetEmtehanat(int classid)
        {
            using (_dbContext = new DatabaseContext())
            {
                return _dbContext.Emtehanats.Where(a => a.Classid == classid).OrderByDescending(a => a.Id).ToList();
            }
            return null;
        }

        public bool AddEmtehanat(int classid, string text, string date)
        {
            using (_dbContext = new DatabaseContext())
            {
                _dbContext.Emtehanats.Add(new Emtehanat()
                {
                    Classid = classid,
                    Text = text,
                    Date = date,
                });
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
