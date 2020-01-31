using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.Utilities
{
    public static class SendEmail
    {
        public static string SendEmailAction(this string email,string emailmassage)
        {
            try
            {
                string massage = "<div style='color:#ff6a00; border:solid; background-color:lightgrey;'><h3 style='text-shadow:2px; text-align:center;'>پستچی مدارس هوشمند</h3></div>";
                string massage1 = "<div style='background-color:#7fbfb5;'><br /><h3 style='text-align:center;'>با سلام</h3><br /><h3 style='text-align:center;'>" + emailmassage + "</h3><br /></div>";


                string Admin = "tohidhaghighi@gmail.com";
                string Pass = "taktazegarn0123";
                string giver = email;
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(Admin);
                msg.To.Add(giver);
                msg.Subject = "پستچی مدارس هوشمند";
                msg.Body = massage + massage1;
                msg.IsBodyHtml = true;
                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com";
                System.Net.NetworkCredential ntwd = new NetworkCredential();
                ntwd.UserName = Admin; //Your Email ID
                ntwd.Password = Pass; // Your Password
                smt.UseDefaultCredentials = true;
                smt.Credentials = ntwd;
                smt.Port = 587;
                smt.EnableSsl = true;
                smt.Send(msg);
                return "true";
            }
            catch
            {
                return "false";
            }
            return "false";
        }
    }
}
