using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WRS20_Logic.JsonObjects
{
    public class JRadar : JsonObject
    {
        private JClient me;
        public JClient Me
        {
            get { return me; }
            set { me = value; }
        }

        private List<JClient> nearbyClients = new List<JClient>();
        public List<JClient> NearbyClients
        {
            get { return nearbyClients; }
            set { nearbyClients = value; }
        }

        private List<JShot> nearbyShots = new List<JShot>();
        public List<JShot> NearbyShots
        {
            get { return nearbyShots; }
            set { nearbyShots = value; }
        }

        public JRadar(string contentString)
            : base(contentString)
        {
            
        }

        protected override void parseObjects()
        {
            nearbyClients = new List<JClient>();
            nearbyShots = new List<JShot>();

            JObject o = JObject.Parse(ContentString);
            JObject jMe = (JObject)o["me"];
            me = new JClient(jMe.ToString());

            JObject jNearbyClients = (JObject)o["nearby-clients"];
            jNearbyClients.Properties().ToList().ForEach(prop =>
            {
                JObject client = (JObject)prop.Value;
                nearbyClients.Add(new JClient(client.ToString()));
            });

            JObject jNearbyShots = (JObject)o["nearby-shots"];
            jNearbyShots.Properties().ToList().ForEach(prop =>
            {
                JObject client = (JObject)prop.Value;
                nearbyShots.Add(new JShot(client.ToString()));
            });
        }
    }
}
