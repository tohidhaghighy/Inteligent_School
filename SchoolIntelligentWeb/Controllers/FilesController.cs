using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;

namespace SchoolIntelligentWeb.Controllers
{
    public class FilesController : ApiController
    {
        private IFilesService _file = new FilesService();
        [System.Web.Http.HttpGet]
        public List<FileInfo> GetFiles(int id)
        {
            return _file.GetFiles(id);
        }
       
        public string Post(string link, string description)
        {
            string newfile = _file.AddFiles(link, description,"",1,1,1,1,1);
            if (newfile != null)
            {
                return newfile;
            }
            return null;
        }
    }
}
