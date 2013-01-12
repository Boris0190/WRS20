using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WRS20_Logic
{
    public class Settings
    {
        private double shipRadius;
        public double ShipRadius
        {
            get { return shipRadius; }
        }
        private int gameZone;
        public int GameZone
        {
            get { return gameZone; }
        }

        public void Refresh(string json)
        {
            JObject o = JObject.Parse(json);
            gameZone = (int)o["game-zone"];
            shipRadius = (int)o["ship-radius"];
        }
    }
}
