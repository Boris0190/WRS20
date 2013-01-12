using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WRS20_Logic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WRS20_Logic.HTTPCommands;
using WRS20_Logic.JsonObjects;

namespace WRS20_ClientAI
{
    class AICore
    {
        String uri = "http://server:31337/";

        List<AIShip> aiShipArr = new List<AIShip>();

        public AICore()
        {

        }

        JConfiguration config;
        JTeam team;

        public void Start()
        {
            //String teamName = "HelloKittyStorm" + DateTime.Now.Ticks.ToString();
            String teamName = "HelloKittyStorm";

            CConfiguration cConfig = new CConfiguration();
            cConfig.uri = uri;
            config = new JConfiguration(cConfig.SendCommand());

            CTeam cTeam = new CTeam();
            cTeam.TeamName = teamName;
            cTeam.uri = uri;
            team = new JTeam(cTeam.SendCommand());


            Task.Factory.StartNew(() => ThinkTask());
        }

        private void bSpawnNewShip()
        {
            aiShipArr.Add(new AIShip(uri, team.TeamPrivateKey, config));


            //CMove move = new CMove();
            //move.uri = uri;
            //move.secret = secret;
            //move.Dx = 6;
            //move.Dy = 20;
            //move.SendCommand();

            //CRadar radar = new CRadar();
            //radar.uri = uri;
            //radar.secret = secret;
            //JRadar radarResult = new JRadar(radar.SendCommand());
        }

        private void ThinkTask()
        {
            while (true)
            {
                Think();
            }
        }

        private void Think()
        {
            if (aiShipArr.Count == 0)
            {
                bSpawnNewShip();
            }
            foreach (var aiShip in aiShipArr)
            {
                aiShip.Think();
            }
        }
    }
}
