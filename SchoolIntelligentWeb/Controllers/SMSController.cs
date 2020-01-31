using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;

namespace SchoolIntelligentWeb.Controllers
{
    public class SMSController : ApiController
    {
        private DatabaseContext _dbContext = null;

        [System.Web.Http.HttpGet]
        public string SendSMS(string msg,string tokens)
        {
            using (_dbContext = new DatabaseContext())
            {
                var FindToken = _dbContext.Settings.FirstOrDefault();

                if (FindToken != null)
                {
                    string token = tokens; //read from database
                    try
                    {
                        var applicationID = "AIzaSyCl49GKAHY2h_OQQZwJb_GquDS3zRk1hRI";
                        var senderId = "260185255645";
                        string deviceId = token;
                        System.Net.WebRequest tRequest =
                            System.Net.WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                        tRequest.Method = "post";
                        tRequest.ContentType = "application/json";
                        var data = new
                        {
                            to = deviceId,
                            data = new
                            {
                                value=msg
                            },
                            priority = "high"

                        };

                        var serializer = new JavaScriptSerializer();
                        var json = serializer.Serialize(data);
                        Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);
                        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                        tRequest.ContentLength = byteArray.Length;

                        using (System.IO.Stream dataStream = tRequest.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);

                            using (System.Net.WebResponse tResponse = tRequest.GetResponse())
                            {
                                using (System.IO.Stream dataStreamResponse = tResponse.GetResponseStream())
                                {
                                    using (
                                        System.IO.StreamReader tReader = new System.IO.StreamReader(dataStreamResponse))
                                    {
                                        String sResponseFromServer = tReader.ReadToEnd();
                                        return sResponseFromServer;
                                    }
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }
            return "TokenNotfind";
        }


        /// <summary>
        /// topic == all   && topic== lesson id
        /// MessageBox.Show(SendSimpleMessageToTopic("4", "hiii"));
        /// MessageBox.Show(SendSimpleMessageToTopic("all", "hiii"));
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public string SendSMStotopic(string msg,string topic)
        {
            using (_dbContext = new DatabaseContext())
            {
                var FindToken = _dbContext.Settings.FirstOrDefault();

                if (FindToken != null)
                {
                    string token = FindToken.Token; //read from database
                    try
                    {
                        var applicationID = "AIzaSyCl49GKAHY2h_OQQZwJb_GquDS3zRk1hRI";
                        var senderId = "260185255645";
                        string deviceId = token;
                        System.Net.WebRequest tRequest =
                            System.Net.WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                        tRequest.Method = "post";
                        tRequest.ContentType = "application/json";
                        var data = new
                        {
                            to = "/topics/" + topic,
                            data = new
                            {
                                value = msg
                            },
                            priority = "high"

                        };

                        var serializer = new JavaScriptSerializer();
                        var json = serializer.Serialize(data);
                        Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);
                        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                        tRequest.ContentLength = byteArray.Length;

                        using (System.IO.Stream dataStream = tRequest.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);

                            using (System.Net.WebResponse tResponse = tRequest.GetResponse())
                            {
                                using (System.IO.Stream dataStreamResponse = tResponse.GetResponseStream())
                                {
                                    using (
                                        System.IO.StreamReader tReader = new System.IO.StreamReader(dataStreamResponse))
                                    {
                                        String sResponseFromServer = tReader.ReadToEnd();
                                        return sResponseFromServer;
                                    }
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }
            return "TokenNotfind";
        }
    }
}
