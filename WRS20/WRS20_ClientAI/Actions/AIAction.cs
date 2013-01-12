using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace WRS20_ClientAI.Actions
{
    public enum ActionState
    {
        None,
        Running,
        Finished,
        Error
    }

    public abstract class AIAction
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(AIAction));

        private AIShip host;
        public AIShip Host
        {
            get { return host; }
        }

        private ActionState currentState;
        public ActionState CurrentState
        {
            get { return currentState; }
            protected set
            {
                currentState = value;
                logger.Info("AIAction Status: " + value.ToString());
            }
        }


        public AIAction(AIShip host)
        {
            this.host = host;
        }

        public abstract ActionState DoIt();
    }
}
