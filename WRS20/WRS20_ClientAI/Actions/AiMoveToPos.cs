using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WRS20_Logic;
using WRS20_Logic.JsonObjects;
using WRS20_Logic.HTTPCommands;

namespace WRS20_ClientAI.Actions
{
    class AiMoveToPos : AIAction
    {
        private double toPosX;
        public double ToPosX
        {
            get { return toPosX; }
            set { toPosX = value; }
        }

        private double toPosY;
        public double ToPosY
        {
            get { return toPosY; }
            set { toPosY = value; }
        }

        private bool stopAtPosition = false;
        public bool StopAtPosition
        {
            get { return stopAtPosition; }
            set { stopAtPosition = value; }
        }


        public AiMoveToPos(double toPosX, double toPosY, AIShip host)
            : base(host)
        {
            this.toPosX = toPosX;
            this.toPosY = toPosY;
        }

        public override ActionState DoIt()
        {
            if (CurrentState == ActionState.Finished) return CurrentState;
            CurrentState = ActionState.Running;

            double dx = toPosX - Host.CurrRadarResult.Me.X;
            double dy = toPosY - Host.CurrRadarResult.Me.Y;

            double len = Math.Sqrt(dx * dx + dy * dy);
            dx = (dx / len) * (Host.Config.MaxShipSpeed - 1); //TODO: -1 unsauber, aber Server weist Nachrichten ab, die speed>maxShipSpeed haben
            dy = (dy / len) * (Host.Config.MaxShipSpeed - 1);

            CMove move = new CMove();
            move.uri = Host.CurrRadarCommand.uri;
            move.secret = Host.CurrRadarCommand.secret;
            move.Dx = dx;
            move.Dy = dy;


            move.SendCommandAsync(null);


            if (Helper.getDistanceToPoint(Host.CurrRadarResult.Me.X, Host.CurrRadarResult.Me.Y, toPosX, toPosY) < 5)
            {
                CurrentState = ActionState.Finished;
            }
            return CurrentState;
        }
    }
}
