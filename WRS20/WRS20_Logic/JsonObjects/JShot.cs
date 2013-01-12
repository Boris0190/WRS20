using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WRS20_Logic.JsonObjects
{
    public class JShot : JsonObject
    {
        private Guid id;
        public Guid Id
        {
            get { return id; }
            set { id = value; }
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

        public JShot(String contentString)
            : base(contentString)
        { }

        protected override void parseObjects()
        {
            JObject o = JObject.Parse(ContentString);
            id = (Guid)o["id"];
            x = (double)o["x"];
            y = (double)o["y"];
            dx = (double)o["dx"];
            dy = (double)o["dy"];
        }
    }
}
