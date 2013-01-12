using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WRS20_Logic.JsonObjects
{
    public class JDump : JsonObject
    {
        public JDump(String contentString)
            : base(contentString)
        { }

        private List<JClient> connectedClients = new List<JClient>();
        public List<JClient> ConnectedClients
        {
            get { return connectedClients; }
            set { connectedClients = value; }
        }

        private List<JShot> existingShots = new List<JShot>();
        public List<JShot> ExistingShots
        {
            get { return existingShots; }
            set { existingShots = value; }
        }

        protected override void parseObjects()
        {
            connectedClients = new List<JClient>();
            existingShots = new List<JShot>();

            JObject o = JObject.Parse(ContentString);
            //JObject jMe = (JObject)o["me"];
            //me = new JClient(jMe.ToString());

            JArray jConnectedClients = (JArray)o["clients"];
            jConnectedClients.ToList().ForEach(prop =>
            {
                JObject client = (JObject)prop["public"];
                connectedClients.Add(new JClient(client.ToString()));
            });

            JArray jExistingShots = (JArray)o["shots"];
            jExistingShots.ToList().ForEach(prop =>
            {
                JObject client = (JObject)prop["public"];
                existingShots.Add(new JShot(client.ToString()));
            });
        }
    }
}
