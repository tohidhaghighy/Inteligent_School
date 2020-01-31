using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolIntelligentWeb.Utilities
{
    public class UploadFile
    {
        private readonly string _root;
        private readonly string _fileNameAllowedChar;
        private readonly IList<string> _extToFilter;
        private readonly IList<string> _nameToFilter;
        public UploadFile()
        {
            _root = HttpContext.Current.Server.MapPath("~/Upload");

            _fileNameAllowedChar = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789- _";

            _extToFilter = new List<string> 
            { 
            ".aspx", ".asax", ".asp", ".ashx", ".asmx", ".axd", ".master", ".svc", ".php" ,        
            ".php3" , ".php4", ".ph3", ".ph4", ".php4", ".ph5", ".sphp", ".cfm", ".ps", ".stm",
            ".htaccess", ".htpasswd", ".php5", ".phtml", ".cgi", ".pl", ".plx", ".py", ".rb", ".sh", ".jsp",
            ".cshtml", ".vbhtml", ".swf" , ".xap", ".asptxt", ".html"
             };

            _nameToFilter = new List<string> 
            { 
             "web.config" , "htaccess" , "htpasswd", "web~1.con"
             };

        }


        public JsonFileResult FileUpload()
        {
            bool isSavedSuccessfully = true;
            string Name = "";
            try
            {
                foreach (string fileName in HttpContext.Current.Request.Files)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                   var fName = file.FileName;

                    //اگر فایل خطر ناکی است آپلود نشود
                    if (!CanUploadFile(fName))
                    {
                        isSavedSuccessfully = false;
                        continue;
                    }

                    if (file != null && file.ContentLength > 0)
                    {
                        //ایجاد یک نام معتبر برای فایلی که نامش معتبر نیست
                        fName = GenerateFineFileName(fName);
                        Name = fName;
                        //جلوگیری از ذخیره فایلهای هم نام
                        var i = 1;
                        while (File.Exists(Path.Combine(_root, fName)))
                        {
                            fName = fName.Insert(fName.IndexOf('.'), "_" + i.ToString());
                            i = i + 1;
                        }

                       var savePath = string.Format("{0}/{1}", _root, fName);
                       file.SaveAs(savePath);
                    }

                }

            }
            catch (Exception)
            {
                isSavedSuccessfully = false;
            }
            return new JsonFileResult() { result = isSavedSuccessfully ,name = Name};
        }


        /// <summary>
        //جلوگیری از آپلود فایل های خطر ناک
        /// </summary>
        private bool CanUploadFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            fileName = fileName.ToLowerInvariant();
            var name = Path.GetFileName(fileName);
            var ext = Path.GetExtension(fileName);

            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Uploaded file should have a name.");

            return !_extToFilter.Contains(ext) &&
                   !_nameToFilter.Contains(name) &&
                   !_nameToFilter.Contains(ext) &&
                //for "file.asp;.jpg" files
                   _extToFilter.All(item => !name.Contains(item));
        }



        /// <summary>
        ///ایجاد یک نام مجاز
        /// </summary>
        private string GenerateFineFileName(string name)
        {
            var file = new FileInfo(name);
            string newName = DateTime.Now.ToString("d_MMM_yyyy_HH_mm_ssff");
            return newName + file.Extension;
        }

    }
}
public class JsonFileResult
{
    public bool result { get; set; }
    public string msg { get; set; }
    public string name { get; set; }
}