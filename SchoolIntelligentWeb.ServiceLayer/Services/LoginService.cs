using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class LoginService:ILogin
    {
        private DatabaseContext _dbContext = null;
        private TokenService _tokenservice = null;
        public int LoginRequest(string username, string password)
        {
            using (_dbContext = new DatabaseContext())
            {
                var finduser = _dbContext.Users.FirstOrDefault(a => a.Username == username && a.Password == password);
                if (finduser != null) return finduser.UserId;
            }
            return 0;
        }

       
        public List<Multiply_ChoiseItems> GetAllInfo(int id)
        {
            List<Multiply_ChoiseItems> AllInfo=new List<Multiply_ChoiseItems>();
            using (_dbContext = new DatabaseContext())
            {
                var findinfo = _dbContext.Provided.Where(a => a.TeacherId == id).ToList();
                if (findinfo.Any())
                {
                    AllInfo.Add(new Multiply_ChoiseItems() { Id = id, Text = findinfo[0].Teacher.Name + " " + findinfo[0].Teacher.Family });
                }
                foreach (var value in findinfo)
                {
                    AllInfo.Add(new Multiply_ChoiseItems(){Id = value.Id,Text = " درس "+value.Lesson.Name+" کلاس "+value.Class.Name});
                }
            }
            return AllInfo;
        }
        private ITeacherService _teacherservice=new TeacherService();
        public LoginInfo GetLoginInfo(string username, string password,int roll)
        {
            using (_dbContext = new DatabaseContext())
            {
                var finduser = _dbContext.Users.FirstOrDefault(a => a.Username == username && a.Password == password && a.Type==roll);
                if (finduser != null)
                {
                    if (finduser.Type==2)
                    {
                        var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == finduser.UserId);
                        if (findstudent!=null)
                        {
                            LoginInfo studentinfo = new LoginInfo() { Id = finduser.UserId ,Name = findstudent.Name,Family = findstudent.Family,ClassId = findstudent.ClassId,Classname ="کلاس "+ findstudent.Class.Name ,Image = findstudent.Photo};
                            
                            return studentinfo;
                        }
                    }
                    if (finduser.Type == 1)
                    {
                        var findstudent = _dbContext.Teachers.FirstOrDefault(a => a.Id == finduser.UserId);
                        if (findstudent != null)
                        {
                            LoginInfo studentinfo = new LoginInfo() { Id = finduser.UserId, Name = findstudent.Name, Family = findstudent.Family, Image = findstudent.Photo };
                            //studentinfo.ClassList = _teacherservice.GetTeacherClasslist(findstudent.Id);
                            //studentinfo.TypeList = _teacherservice.GetTypeofgrade();
                            return studentinfo;
                        }
                    }
                    if (finduser.Type == 3)
                    {
                        var findparent = _dbContext.Parents.FirstOrDefault(a => a.Id == finduser.UserId);

                        if (findparent != null)
                        {
                            var findstudent = _dbContext.Students.FirstOrDefault(a => a.FamilyId == findparent.Id);

                            if (findstudent != null)
                            {
                                LoginInfo studentinfo = new LoginInfo() { Id = findstudent.Id, ParentName = findparent.Name + " " + findparent.Family, Name = findstudent.Name, Family = findstudent.Family, Image = findstudent.Photo, ClassId = findstudent.ClassId, Classname = "کلاس " + findstudent.Class.Name }; 
                                return studentinfo;
                            }
                        }
                    }
                }
            }
            return null;
        }


        public LoginInfo GetLoginInfoandroid(string username, string password, int roll,string fcm)
        {
            _tokenservice = new TokenService();
            using (_dbContext = new DatabaseContext())
            {
                var finduser = _dbContext.Users.FirstOrDefault(a => a.Username == username && a.Password == password && a.Type == roll);
                if (finduser != null)
                {
                    if (finduser.Type == 2)
                    {
                        var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == finduser.UserId);
                        if (findstudent != null)
                        {
                            LoginInfo studentinfo = new LoginInfo() { Id = finduser.UserId, Name = findstudent.Name, Family = findstudent.Family, ClassId = findstudent.ClassId, Classname = "کلاس " + findstudent.Class.Name, Image = findstudent.Photo };
                            _tokenservice.Addtokenfcm(fcm, 1, findstudent.Id);
                            return studentinfo;
                        }
                    }
                    if (finduser.Type == 1)
                    {
                        var findstudent = _dbContext.Teachers.FirstOrDefault(a => a.Id == finduser.UserId);
                        if (findstudent != null)
                        {
                            LoginInfo studentinfo = new LoginInfo() { Id = finduser.UserId, Name = findstudent.Name, Family = findstudent.Family, Image = findstudent.Photo };
                            studentinfo.ClassList = _teacherservice.GetTeacherClasslist(findstudent.Id);
                            studentinfo.TypeList = _teacherservice.GetTypeofgrade();
                            _tokenservice.Addtokenfcm(fcm, 2, findstudent.Id);
                            return studentinfo;
                        }
                    }
                    if (finduser.Type == 3)
                    {
                        var findparent = _dbContext.Parents.FirstOrDefault(a => a.Id == finduser.UserId);

                        if (findparent != null)
                        {
                            var findstudent = _dbContext.Students.FirstOrDefault(a => a.FamilyId == findparent.Id);

                            if (findstudent != null)
                            {
                                LoginInfo studentinfo = new LoginInfo() { Id = findstudent.Id, ParentName = findparent.Name + " " + findparent.Family, Name = findstudent.Name, Family = findstudent.Family, Image = findstudent.Photo, ClassId = findstudent.ClassId, Classname = "کلاس " + findstudent.Class.Name };
                                _tokenservice.Addtokenfcm(fcm, 3, findstudent.Id);
                                return studentinfo;
                            }
                        }
                    }
                }
            }
            return null;
        }
        public LoginInfo GetForgetLoginInfo(string username, string email, int roll)
        {
            using (_dbContext = new DatabaseContext())
            {
                var finduser = _dbContext.Users.FirstOrDefault(a => a.Username == username && a.Type == roll);
                if (finduser != null)
                {
                    if (finduser.Type == 2)
                    {
                        var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == finduser.UserId && a.Email==email);
                        if (findstudent != null)
                        {
                           
                            LoginInfo studentinfo = new LoginInfo() { Id = finduser.UserId, Name = findstudent.Name, Family = findstudent.Family, ClassId = findstudent.ClassId, Classname = "کلاس " + findstudent.Class.Name, Image = findstudent.Photo ,Password = finduser.Password.Substring(0,7)};
                            finduser.Password = finduser.Password.Substring(0, 7).Hashmd5();
                            _dbContext.SaveChanges();
                            return studentinfo;
                        }
                    }
                    if (finduser.Type == 1)
                    {
                        
                        var findstudent = _dbContext.Teachers.FirstOrDefault(a => a.Id == finduser.UserId && a.Email==email);
                        if (findstudent != null)
                        {
                            LoginInfo studentinfo = new LoginInfo() { Id = finduser.UserId, Name = findstudent.Name, Family = findstudent.Family, Image = findstudent.Photo ,Password = finduser.Password.Substring(0,7)};
                            finduser.Password = finduser.Password.Substring(0, 7).Hashmd5();
                            _dbContext.SaveChanges();
                            return studentinfo;
                        }
                    }
                }
            }
            return null;
        }
    }
}
