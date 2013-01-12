using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WRS20_Logic.JsonObjects
{
    public class JConfiguration : JsonObject
    {
        private int teams;
        public int Teams
        {
            get { return teams; }
            set { teams = value; }
        }

        private int shipsPerTeam;
        public int ShipsPerTeam
        {
            get { return shipsPerTeam; }
            set { shipsPerTeam = value; }
        }


        private double gameZone;
        public double GameZone
        {
            get { return gameZone; }
            set { gameZone = value; }
        }

        private double shipRadius;
        public double ShipRadius
        {
            get { return shipRadius; }
            set { shipRadius = value; }
        }


        private double maxShipSpeed;
        public double MaxShipSpeed
        {
            get { return maxShipSpeed; }
            set { maxShipSpeed = value; }
        }

        private double maxShotSpeed;
        public double MaxShotSpeed
        {
            get { return maxShotSpeed; }
            set { maxShotSpeed = value; }
        }



        private int minRadarInterval;
        public int MinRadarInterval
        {
            get { return minRadarInterval; }
            set { minRadarInterval = value; }
        }

        private int minShootInterval;
        public int MinShootInterval
        {
            get { return minShootInterval; }
            set { minShootInterval = value; }
        }

        public JConfiguration(string contentString)
            : base(contentString)
        { }

        protected override void parseObjects()
        {
            JObject o = JObject.Parse(ContentString);

            teams = (int)o["teams"];
            shipsPerTeam = (int)o["ships-per-team"];

            gameZone = (int)o["game-zone"];
            shipRadius = (double)o["ship-radius"];

            maxShipSpeed = (double)o["max-ship-speed"];
            maxShotSpeed = (double)o["max-shot-speed"];


            minRadarInterval = (int)o["min-radar-interval"];
            minShootInterval = (int)o["min-shoot-interval"];
        }
    }
}
