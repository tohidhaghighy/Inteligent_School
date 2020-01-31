using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.Models
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {

        public CustomMultipartFormDataStreamProvider(string path)
            : base(path)
        {
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            Guid newGuid = Guid.NewGuid();
            string imageName = newGuid.ToString();
            string extentionfile = headers.ContentDisposition.FileName;
            String FileExtension = "";
            try
            {
                FileExtension = System.IO.Path.GetExtension(headers.ContentDisposition.FileName);
            }
            catch (Exception)
            {
                FileExtension = ".jpg";
            }
            
            var name = imageName + FileExtension;
            UtilitiValues.name = name;
            return name;
        }
    }
}