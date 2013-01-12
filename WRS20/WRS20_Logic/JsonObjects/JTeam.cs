using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WRS20_Logic.JsonObjects
{
    public class JTeam : JsonObject
    {
        private Guid teamPublicKey;
        public Guid TeamPublicKey
        {
            get { return teamPublicKey; }
            set { teamPublicKey = value; }
        }
        private Guid teamPrivateKey;
        public Guid TeamPrivateKey
        {
            get { return teamPrivateKey; }
            set { teamPrivateKey = value; }
        }

        public JTeam(String contentString)
            : base(contentString)
        {
        }
        protected override void parseObjects()
        {
            JObject o = JObject.Parse(ContentString);

            teamPublicKey = (Guid)o["team-public-key"];
            teamPrivateKey = (Guid)o["team-private-key"];
        }
    }
}
