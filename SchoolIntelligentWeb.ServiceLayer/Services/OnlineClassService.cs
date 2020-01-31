using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.Utilities;


namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class OnlineClassService:IOnlineClassService
    {
        private DatabaseContext _dbContext = null;
        private MakeUsername mkuser = new MakeUsername();
        public List<OnlineClass> ListClass(int teacherid)
        {
            using (_dbContext = new DatabaseContext())
            {
                return _dbContext.OnlineClasses.Where(a=>a.TeacherId==teacherid).ToList();
            }
            return null;
        }

        public OnlineClass GetClass(int teacherid)
        {
            using (_dbContext = new DatabaseContext())
            {
                return _dbContext.OnlineClasses.FirstOrDefault(a => a.TeacherId == teacherid);
            }
            return null;
        }

        public Boolean AddClass(int teacherid,string name,string family,string email,string phone,string detail,string maghtae,string date,string username,string password)
        {
            using (_dbContext = new DatabaseContext())
            {
                password = password.Hashmd5();
                DateTime newdate = date.tomiladilimited();
                OnlineClass newclass = new OnlineClass()
                {
                    Name=name,
                    Family=family,
                    Email=email,
                    Phone=phone,
                    Description=detail,
                    Maghtae=maghtae,
                    Date=date,
                    SearchDatetime=newdate,
                    TeacherId=teacherid,
                    Username=username,
                    Password=password,
                };
                _dbContext.OnlineClasses.Add(newclass);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean DeleteClass(int classid)
        {
            using (_dbContext = new DatabaseContext())
            {
                var find=_dbContext.OnlineClasses.FirstOrDefault(a => a.Id == classid);
                _dbContext.OnlineClasses.Remove(find);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public OnlineClass allinfo(int teacherid)
        {
            OnlineClass newclass = new OnlineClass();
            using (_dbContext = new DatabaseContext())
            {
                var findteacher = _dbContext.Teachers.FirstOrDefault(a => a.Id == teacherid);
                if (findteacher!=null)
                {
                    newclass.Id = findteacher.Id;
                    newclass.Name = findteacher.Name;
                    newclass.Family = findteacher.Family;
                    newclass.Email = findteacher.Email;
                    newclass.Phone = findteacher.Phone;
                    
                    while (true)
                    {
                        string user = mkuser.CreatePassword(6);
                        if (_dbContext.OnlineClasses.FirstOrDefault(a => a.Username == "Onlinecl_" + user) == null)
                        {
                            newclass.Username = "Onlinecl_" + user;
                            return newclass;
                        }
                    }
                   
                }
            }
            return null;
        }


        public OnlineClass Logininfo(string username, string password)
        {
            using (_dbContext = new DatabaseContext())
            {
                password=password.Hashmd5();
                return _dbContext.OnlineClasses.FirstOrDefault(a => a.Username == username && a.Password == password);
            }
        }

        public Boolean Addfiletoonlineclass(string name, string url,int id)
        {
            using (_dbContext = new DatabaseContext())
            {
                OnlineClassFiles newfile = new OnlineClassFiles()
                {
                    Id_ClassOnline=id,
                    Name=name,
                    Url=url,
                };
                _dbContext.OnlineClassFiles.Add(newfile);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<OnlineClassFiles> allfiles(int classid)
        {
            using (_dbContext = new DatabaseContext())
            {
               
                    return _dbContext.OnlineClassFiles.Where(a => a.Id_ClassOnline == classid).ToList();

            }
            return null;
        }
        public List<OnlineClassFiles> allfileswithname(string classname)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findclass = _dbContext.OnlineClasses.FirstOrDefault(a => a.Username == classname);
                if (findclass!=null)
                {
                    return _dbContext.OnlineClassFiles.Where(a => a.Id_ClassOnline == findclass.Id).ToList();
                }
                
            }
            return null;
        }

        public Boolean deletefiles(int id)
        {
            using (_dbContext = new DatabaseContext())
            {
               
                  var find=  _dbContext.OnlineClassFiles.FirstOrDefault(a => a.Id == id);
                  _dbContext.OnlineClassFiles.Remove(find);
                  _dbContext.SaveChanges();
                  return true;
            }
            return false;
        }
    }
}
