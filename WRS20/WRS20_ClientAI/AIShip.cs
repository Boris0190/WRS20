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
        public String uri;
        public Guid teamPrivateKey;


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

        public AIShip(String uri, Guid teamPrivateKey, JConfiguration config)
        {
            this.uri = uri;
            this.teamPrivateKey = teamPrivateKey;
            this.config = config;

            currRadarCommand = new CRadar();
            currRadarCommand.uri = uri;
            currRadarCommand.secret = secret;

            Task.Factory.StartNew(() => doRadarTask(), TaskCreationOptions.LongRunning);
        }

        private void doRadarTask()
        {
            while (true)
            {
                JRadar newRadarQry = new JRadar(currRadarCommand.SendCommand());
                if (newRadarQry.IsValid) currRadarResult = newRadarQry;
                Thread.Sleep(config.MinRadarInterval);
            }
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
