using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WRS20_Logic.JsonObjects
{
    public class JClient : JsonObject
    {
        private Guid id;
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private String team;
        public String Team
        {
            get { return team; }
            set { team = value; }
        }

        private double x;
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        private double dx;
        public double Dx
        {
            get { return dx; }
            set { dx = value; }
        }

        private double dy;
        public double Dy
        {
            get { return dy; }
            set { dy = value; }
        }


        public JClient(string contentString)
            : base(contentString)
        { }

        protected override void parseObjects()
        {
            String rndString = DateTime.Now.Ticks.ToString();
            JObject o = JObject.Parse(ContentString);
            id = (Guid)o["id"];
            name = (String)o["name"] ?? ("player" + rndString);
            team = (String)o["team"] ?? ("team" + rndString);
            x = double.Parse(((string)o["x"] ?? "0"), CultureInfo.InvariantCulture);
            y = double.Parse(((string)o["y"] ?? "0"), CultureInfo.InvariantCulture);
            dx = double.Parse(((string)o["dx"] ?? "0"), CultureInfo.InvariantCulture);
            dy = double.Parse(((string)o["dy"] ?? "0"), CultureInfo.InvariantCulture);
        }
    }
}
