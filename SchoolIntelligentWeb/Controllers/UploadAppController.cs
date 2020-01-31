using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;
using CustomMultipartFormDataStreamProvider = SchoolIntelligentWeb.Models.CustomMultipartFormDataStreamProvider;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.Controllers
{
    public class UploadAppController : ApiController
    {
        private IFilesService _file = new FilesService();
        [HttpPost]
        public Task<IEnumerable<FileDesc>> Post(string description, int teacherId, int lessonId, int fileType, int schoolId)
        {
            string filename = "";
            string folderName = "Upload";
            string PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            string rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            try
            {
                if (Request.Content.IsMimeMultipartContent())
                {
                    var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
                    var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<FileDesc>>(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }
                        var fileInfo = streamProvider.FileData.Select(i =>
                        {
                            var info = new System.IO.FileInfo(i.LocalFileName);
                            return new FileDesc(info.Name,folderName + "/" + info.Name, info.Length / 1024);
                        });
                        _file.AddFiles("http://mahamsys.com/Upload/" + UtilitiValues.name, description,
                           UtilitiValues.name, lessonId, fileType, schoolId, teacherId,0);
                        return fileInfo;
                    });

                    return task;
                }
                else
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
                }
            }
            catch(Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }
        
       
        
    }
}
