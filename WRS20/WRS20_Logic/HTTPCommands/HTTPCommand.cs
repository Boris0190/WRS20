using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WRS20_Logic;
using System.Net;
using System.IO;

namespace WRS20_Logic.HTTPCommands
{
    public abstract class HTTPCommand
    {
        public String uri;
        //public Guid secret = Guid.Empty;
        protected String name;
        protected String paramString;
        public delegate void JsonAsyncCallback(String result);

        public abstract void CalculateParamString();

        public String SendCommand()
        {
            //return jsonString.getString(uri + name + "?" + (secret != Guid.Empty ? ("secret=" + secret.ToString() + "&") : "") + paramString);
            return jsonString.getString(uri + name + "?" + paramString);
        }

        public void SendCommandAsync(JsonAsyncCallback jCallback)
        {
            jb = jCallback;
            //String qryString = uri + name + "?" + (secret != Guid.Empty ? ("secret=" + secret.ToString() + "&") : "") + paramString;
            String qryString = uri + name + "?" + paramString;

            request = (HttpWebRequest)HttpWebRequest.Create(qryString);
            IAsyncResult result = (IAsyncResult)request.BeginGetResponse(RespCallback, null);
        }

        private HttpWebRequest request;
        private JsonAsyncCallback jb;
        private void RespCallback(IAsyncResult asyncResult)
        {
            // State of request is asynchronous.
            HttpWebRequest myHttpWebRequest = request;
            WebResponse response = null;
            try
            {
                response = (HttpWebResponse)myHttpWebRequest.EndGetResponse(asyncResult);
            }
            catch { return; }


            if (jb == null)
            {
                response.Close();
                return; //Eine Auwertung der Ergebnisse ist nicht notwendig
            }

            // Read the response into a Stream object.
            Stream responseStream = response.GetResponseStream();

            // Begin the Reading of the contents of the HTML page and print it to the console.
            string json = "";

            using (StreamReader reader = new StreamReader(responseStream))
            {
                while (!reader.EndOfStream)
                {
                    json += reader.ReadLine();
                }
            }

            response.Close();
            jb(json);

            return;
        }
    }
}
