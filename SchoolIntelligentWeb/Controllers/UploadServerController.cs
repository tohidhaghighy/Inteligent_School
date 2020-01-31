using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.Models;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.Controllers
{
    public class UploadServerController : ApiController
    {
        public async Task<HttpResponseMessage> Post(int id)
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                var uploadPath = HttpContext.Current.Server.MapPath("~/Upload/");

                var multipartFormDataStreamProvider = new CustomMultipartFormDataStreamProvider(uploadPath);
                await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);
                foreach (var key in multipartFormDataStreamProvider.FormData.AllKeys)
                {
                    foreach (var val in multipartFormDataStreamProvider.FormData.GetValues(key))
                    {
                        Console.WriteLine(string.Format("{0}: {1}", key, val));
                    }
                }

                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(e.Message)
                };
            }
        }
        
    }
}
