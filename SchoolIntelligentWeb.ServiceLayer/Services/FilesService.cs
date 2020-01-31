using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class FilesService : IFilesService
    {
        private DatabaseContext _dbContext = null;

        public List<FileInfo> GetFiles(int id)
        {
            List<FileInfo> allfile = new List<FileInfo>();
            using (_dbContext = new DatabaseContext())
            {
                try
                {
                    var findFiles = _dbContext.Files.Where(a => a.Id > id).OrderByDescending(a => a.Id).ToList();
                    foreach (var item in findFiles)
                    {
                        FileInfo newfile = new FileInfo()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Date = item.Date.Value.ToPersian(),
                            Description = item.Description,
                            Time = item.Time,
                            Url = item.Url,
                            FileType = item.TypeFile,
                        };
                        allfile.Add(newfile);
                    }
                    return allfile;
                }
                catch (Exception ex)
                {
                    return allfile;
                }

            }
            return allfile;
        }

        public string AddFiles(string link, string description,string name,int lessonid,int filetype,int schoolid,int teacherid,int studentid)
        {
            try
            {
                File newfile = new File()
                {
                    Url = link,
                    Description = description,
                    Date = DateTime.Now,
                    Time = DateTime.Now.TimeOfDay, 
                    Name = name,
                    LessonId = lessonid,
                    TypeFile = filetype,
                    TeacherId = teacherid,
                    StudentId = studentid
                };
                using (_dbContext = new DatabaseContext())
                {
                    var findprovide = _dbContext.Provided.FirstOrDefault(a => a.Id == lessonid);
                    if (findprovide!=null)
                    {
                        newfile.LessonId = findprovide.LessionId;
                        _dbContext.Files.Add(newfile);
                        _dbContext.SaveChanges();
                        return "true";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return "false";
            }
           
            return "false";
        }



        public List<FileInfo> GetChosesFilewithprovide(int provideid, int type)
        {
            List<FileInfo> allfile = new List<FileInfo>();
            using (_dbContext = new DatabaseContext())
            {
                try
                {
                    var findprovide = _dbContext.Provided.FirstOrDefault(a => a.Id == provideid);
                    if (findprovide!=null)
                    {
                        var findFiles = _dbContext.Files.Where(a => a.LessonId == findprovide.LessionId && a.TypeFile == type).OrderByDescending(a => a.Id).ToList();
                        foreach (var item in findFiles)
                        {
                            string services = "عمومی";
                            if (item.StudentId != 0)
                            {
                                services = "خصوصی";
                                var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == item.StudentId);
                                if (findstudent != null) services = findstudent.Name + " " + findstudent.Family;
                            }
                            FileInfo newfile = new FileInfo()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Date = item.Date.Value.ToPersian(),
                                Description = item.Description,
                                Time = item.Time,
                                Url = item.Url,
                                FileType = item.TypeFile,
                                PrivateStudentName = services
                            };
                            allfile.Add(newfile);
                        }
                        return allfile;
                    }
                   
                }
                catch (Exception ex)
                {
                    return allfile;
                }

            }
            return allfile;
        }


        public List<FileInfo> GetChosesFile(int idlesson, int type)
        {
            List<FileInfo> allfile=new List<FileInfo>();
            using (_dbContext = new DatabaseContext())
            {
                try
                {
                    
                    var findFiles = _dbContext.Files.Where(a => a.LessonId==idlesson && a.TypeFile==type).OrderByDescending(a => a.Id).ToList();
                    foreach (var item in findFiles)
                    {
                        string services = "عمومی";
                        if (item.StudentId!=0)
                        {
                            services = "خصوصی";
                            var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == item.StudentId);
                            if (findstudent != null) services = findstudent.Name + " " + findstudent.Family;
                        }
                        FileInfo newfile=new FileInfo()
                        {
                            Id =item.Id,
                            Name = item.Name,
                            Date = item.Date.Value.ToPersian(),
                            Description = item.Description,
                            Time = item.Time,
                            Url = item.Url,
                            FileType = item.TypeFile,
                            PrivateStudentName=services
                        };
                        allfile.Add(newfile);
                    }
                    return allfile;
                }
                catch (Exception ex)
                {
                    return allfile;
                }

            }
            return allfile;
        }


        public List<FileInfo> GetChosesFileProvide(int idprovide, int type)
        {
            List<FileInfo> allfile = new List<FileInfo>();
            using (_dbContext = new DatabaseContext())
            {
                try
                {
                    var findclass = _dbContext.Provided.FirstOrDefault(a => a.Id == idprovide);
                    if (findclass!=null)
                    {
                        var findFiles = _dbContext.Files.Where(a => a.LessonId == findclass.LessionId && a.TypeFile == type).OrderByDescending(a => a.Id).ToList();
                        foreach (var item in findFiles)
                        {
                            string services = "عمومی";
                            if (item.StudentId != 0)
                            {
                                services = "خصوصی";
                                var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == item.StudentId);
                                if (findstudent != null) services = findstudent.Name + " " + findstudent.Family;
                            }
                            FileInfo newfile = new FileInfo()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Date = item.Date.Value.ToPersian(),
                                Description = item.Description,
                                Time = item.Time,
                                Url = item.Url,
                                FileType = item.TypeFile,
                                PrivateStudentName = services
                            };
                            allfile.Add(newfile);
                        }
                        return allfile;
   
                    }
                                    }
                catch (Exception ex)
                {
                    return allfile;
                }

            }
            return allfile;
        }
        public List<IdString> GetFileVideo()
        {
            List<IdString> alltype=new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var find= _dbContext.Filetypes.ToList();
                foreach (var item in find)
                {
                    IdString newstring=new IdString()
                    {
                        Id = item.Id,
                        Name = item.TypeFile
                    };
                    alltype.Add(newstring);
                }
                return alltype;
            }
        }

        public Boolean DeleteFile(int id)
        {
            using (_dbContext = new DatabaseContext())
            {
                var find = _dbContext.Files.FirstOrDefault(a => a.Id == id);
                if (find!=null)
                {
                    _dbContext.Files.Remove(find);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public List<FileInfo> GetFilterChosesFile(int idlesson, int type,int idstudent)
        {
            List<FileInfo> allfile = new List<FileInfo>();
            using (_dbContext = new DatabaseContext())
            {
                try
                {

                    var findFiles = _dbContext.Files.Where(a => a.LessonId == idlesson && a.TypeFile == type).OrderByDescending(a => a.Id).ToList();
                    foreach (var item in findFiles)
                    {
                        if (item.StudentId==0 || item.StudentId==idstudent)
                        {
                            string services = "عمومی";
                            if (item.StudentId != 0)
                            {
                                services = "خصوصی";
                                var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == item.StudentId);
                                if (findstudent != null) services = findstudent.Name + " " + findstudent.Family;
                            }
                            FileInfo newfile = new FileInfo()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Date = item.Date.Value.ToPersian(),
                                Description = item.Description,
                                Time = item.Time,
                                Url = item.Url,
                                FileType = item.TypeFile,
                                PrivateStudentName = services
                            };
                            allfile.Add(newfile);
                        }
                        
                    }
                    return allfile;
                }
                catch (Exception ex)
                {
                    return allfile;
                }

            }
            return allfile;
        }

    }
}
