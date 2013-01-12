using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WRS20_Logic;
using System.Threading.Tasks;
using WRS20_Logic.HTTPCommands;
using WRS20_Logic.JsonObjects;
using WRS20_ClientAI.Actions;
using System.Threading;

namespace WRS20_ClientAI
{
    public class AIShip
    {
        private static int currentID = 0;
        public static int CurrentID
        {
            get { return AIShip.currentID++; }
        }

        private String uri;
        public String Uri
        {
            get { return uri; }
        }

        private Guid teamPrivateKey;
        public Guid TeamPrivateKey
        {
            get { return teamPrivateKey; }
        }

        private JSpawn spawn;
        public JSpawn Spawn
        {
            get { return spawn; }
        }

        private JConfiguration config;
        public JConfiguration Config
        {
            get { return config; }
        }

        private CRadar currRadarCommand;
        public CRadar CurrRadarCommand
        {
            get { return currRadarCommand; }
        }
        private JRadar currRadarResult;
        public JRadar CurrRadarResult
        {
            get { return currRadarResult; }
        }

        private AIAction currMoveAction;
        private AIAction currShootAction;

        System.Timers.Timer radarTimer;

        public AIShip(String uri, Guid teamPrivateKey, JConfiguration config)
        {
            String shipName = "Kittay!" + CurrentID.ToString();
            this.uri = uri;
            this.teamPrivateKey = teamPrivateKey;
            this.config = config;

            CSpawn cSpawn = new CSpawn();
            cSpawn.uri = uri;
            cSpawn.ShipName = shipName;
            cSpawn.TeamPrivateKey = teamPrivateKey;
            spawn = new JSpawn(cSpawn.SendCommand());


            currRadarCommand = new CRadar();
            currRadarCommand.uri = uri;
            currRadarCommand.ShipPrivateKey = spawn.ShipPrivateKey;

            radarTimer.Interval = config.MinRadarInterval + 10; //todo +10 ist unsauber. evtl iwie anders machen
            radarTimer.Elapsed += radarTimer_Elapsed;
            radarTimer.Start();
        }

        void radarTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            JRadar newRadarQry = new JRadar(currRadarCommand.SendCommand());
            if (newRadarQry.IsValid) currRadarResult = newRadarQry;
        }


        public void Think()
        {
            if ((currMoveAction == null || currMoveAction.CurrentState != ActionState.Running) && currRadarResult != null)
            {
                Random rd = new Random(DateTime.Now.Millisecond);
                double newPosX = currRadarResult.Me.X + rd.Next(-200, 200);
                double newPosY = currRadarResult.Me.Y + rd.Next(-200, 200);
                currMoveAction = new AiMoveToPos(newPosX, newPosY, this);
            }

            //letzter Schritt: Ausfühen der aktuellen Move und Shoot Aktionen
            if (currMoveAction != null) currMoveAction.DoIt();
            if (currShootAction != null) currShootAction.DoIt();
        }
    }
}
