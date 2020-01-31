using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.Utilities;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.DomainClasses;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class MessageService : IMessageService
    {
        private DatabaseContext _dbContext = null;
        public List<Messagelist> Getalllist(int studentid)
        {
            List<Messagelist> allmessage = new List<Messagelist>();
            using (_dbContext = new DatabaseContext())
            {
                var findclassid = _dbContext.Students.FirstOrDefault(a => a.Id == studentid);
                if (findclassid != null)
                {
                    var findmessages = _dbContext.AssistentMassages.Where(a => a.SchoolId == 1 || a.StudentId == studentid || a.AssistentId == findclassid.ClassId).OrderByDescending(a=>a.Id).ToList();
                    foreach (var item in findmessages)
                    {
                        string didan = "مشاهده نشده";
                        if (item.Seen==true)
                        {
                            didan = "مشاهده شده";
                        }
                        string st = "عمومی";
                        if (item.StudentId!=0)
                        {
                            st = "خصوصی";
                        }
                        Messagelist newmessage = new Messagelist()
                        {
                            Date=item.SendDate.ToPersian(),
                            Id=item.Id,
                            Message=item.Message,
                            Seen=didan,
                            Status=st,
                        };
                        allmessage.Add(newmessage);
                        item.Seen = true;
                    }
                    _dbContext.SaveChanges();
                    return allmessage;
                }

            }
            return allmessage;
        }

        public List<Messagelist> GetalllistTeacher(int teacherid)
        {
            List<Messagelist> allmessage = new List<Messagelist>();
            using (_dbContext = new DatabaseContext())
            {
                    var findmessages = _dbContext.Requestses.Where(a => a.reciver == teacherid.ToString() || a.sender==teacherid.ToString()).ToList();
                    foreach (var item in findmessages)
                    {
                        string didan = "مشاهده نشده";
                        if (item.Seen == true)
                        {
                            didan = "مشاهده شده";
                        }
                        string st = "عمومی";
                        if (item.ID_RequestType != null)
                        {
                            st = item.RequestType.RequestType1;
                        }
                        string name = "";
                        if (item.ID_Student!=null)
                        {
                            name = " ( " + item.Student.Name + " " + item.Student.Family + " )";
                        }
                        string typemessage="modir";
                        if (item.sender!="modir")
                        {
                            typemessage = "moalem";
                        }
                        Messagelist newmessage = new Messagelist()
                        {
                            Date = item.Date.ToPersian(),
                            Id = item.ID,
                            Message = item.Description+ name,
                            Seen = didan,
                            Status=st,
                            Type = typemessage,
                        };
                        allmessage.Add(newmessage);
                        item.Seen = true;
                    }
                    _dbContext.SaveChanges();
                    return allmessage;
                }
            return allmessage;
        }
        public Boolean SendMessagetomodir(int teacherid,string Message)
        {
            using (_dbContext = new DatabaseContext())
            {
                Requests newrequest=new Requests(){
                    Seen=false,
                    sender=teacherid.ToString(),
                    Date=DateTime.Now,
                    reciver="modir",
                    Description=Message,
                };
                _dbContext.Requestses.Add(newrequest);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Messagelist> NewMassageTeacher(int teacherid, int massageid)
        {
            List<Messagelist> allmessage = new List<Messagelist>();
            using (_dbContext = new DatabaseContext())
            {
                var findmessages = _dbContext.Requestses.Where(a => (a.reciver == teacherid.ToString() || a.sender == teacherid.ToString())&& a.ID>massageid).ToList();
                foreach (var item in findmessages)
                {
                    string didan = "مشاهده نشده";
                    if (item.Seen == true)
                    {
                        didan = "مشاهده شده";
                    }
                    string st = "عمومی";
                    if (item.ID_RequestType != null)
                    {
                        st = item.RequestType.RequestType1;
                    }
                    string name = "";
                    if (item.ID_Student != null)
                    {
                        name = " ( " + item.Student.Name + " " + item.Student.Family + " )";
                    }
                    string typemessage = "modir";
                    if (item.sender != "modir")
                    {
                        typemessage = "moalem";
                    }
                    Messagelist newmessage = new Messagelist()
                    {
                        Date = item.Date.ToPersian(),
                        Id = item.ID,
                        Message = item.Description + name,
                        Seen = didan,
                        Status = st,
                        Type = typemessage,
                    };
                    allmessage.Add(newmessage);
                    item.Seen = true;
                }
                _dbContext.SaveChanges();
                return allmessage;
            }
            return allmessage;
        }

        public List<Messagelist> NewMassageParent(int studentid,int massageid)
        {
            List<Messagelist> allmessage = new List<Messagelist>();
            using (_dbContext = new DatabaseContext())
            {
                var findclassid = _dbContext.Students.FirstOrDefault(a => a.Id == studentid);
                if (findclassid != null)
                {
                    var findmessages = _dbContext.AssistentMassages.Where(a => (a.SchoolId == 1 || a.StudentId == studentid || a.AssistentId == findclassid.ClassId) && a.Id>massageid).ToList();
                    foreach (var item in findmessages)
                    {
                        string didan = "مشاهده نشده";
                        if (item.Seen == true)
                        {
                            didan = "مشاهده شده";
                        }
                        string st = "عمومی";
                        if (item.StudentId != 0)
                        {
                            st = "خصوصی";
                        }
                        Messagelist newmessage = new Messagelist()
                        {
                            Date = item.SendDate.ToPersian(),
                            Id = item.Id,
                            Message = item.Message,
                            Seen = didan,
                            Status = st,
                        };
                        allmessage.Add(newmessage);
                        item.Seen = true;
                    }
                    _dbContext.SaveChanges();
                    return allmessage;
                }

            }
            return allmessage;
        }
    }
}
