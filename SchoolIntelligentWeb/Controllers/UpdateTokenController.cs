   using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolIntelligentWeb.DataLayer;
   using SchoolIntelligentWeb.DomainClasses;

namespace SchoolIntelligentWeb.Controllers
{
    public class UpdateTokenController : ApiController
    {
        private DatabaseContext _dbContext = null;
        [HttpGet]
        public Boolean Get(string token)
        {
            using (_dbContext = new DatabaseContext())
            {
                var find = _dbContext.Settings.FirstOrDefault();
                if (find != null)
                {
                    find.Token = token;
                    _dbContext.SaveChanges();
                    return true;
                }
                else if (find==null)
                {
                    _dbContext.Settings.Add(new Setting() {Token = token});
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
