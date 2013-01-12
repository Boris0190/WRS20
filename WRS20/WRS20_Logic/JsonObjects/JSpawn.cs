using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WRS20_Logic.JsonObjects
{
    public class JSpawn : JsonObject
    {
        private Guid shipPublicKey;
        public Guid ShipPublicKey
        {
            get { return shipPublicKey; }
            set { shipPublicKey = value; }
        }

        private Guid shipPrivateKey;
        public Guid ShipPrivateKey
        {
            get { return shipPrivateKey; }
            set { shipPrivateKey = value; }
        }

        public JSpawn(String contentString)
            : base(contentString)
        { }

        protected override void parseObjects()
        {
            JObject o = JObject.Parse(ContentString);
            shipPublicKey = (Guid)o["ship-public-key"];
            shipPrivateKey = (Guid)o["ship-private-key"];
        }
    }
}
