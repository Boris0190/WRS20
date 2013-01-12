using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace WRS20_Logic
{
    public static class jsonString
    {
        public static String getString(String uri)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json, text/javascript, */*";
            request.Method = "POST";
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write("{id : 'test'}");
            }

            WebResponse response;
            try
            {
                response = request.GetResponse();
            }
            catch (Exception e)
            {
                return "";
            }
            

            Stream stream = response.GetResponseStream();
            string json = "";

            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    json += reader.ReadLine();
                }
            }
            return json;
        }
    }
}
