using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.HTTPCommands
{
    public class CMove : HTTPCommand
    {
        private Guid shipPrivateKey;
        public Guid ShipPrivateKey
        {
            get { return shipPrivateKey; }
            set
            {
                shipPrivateKey = value;
                CalculateParamString();
            }
        }

        private double shipDesiredDx = 0;
        public double ShipDesiredDx
        {
            get { return shipDesiredDx; }
            set
            {
                shipDesiredDx = value;
                CalculateParamString();
            }
        }
        private double shipDesiredDy = 0;
        public double ShipDesiredDy
        {
            get { return shipDesiredDy; }
            set
            {
                shipDesiredDy = value;
                CalculateParamString();
            }
        }

        public CMove()
        {
            name = "move";
        }


        public override void CalculateParamString()
        {
            paramString = "ship-private-key=" + shipPrivateKey.ToString() + "&ship-desired-dx=" + shipDesiredDx.ToString() + "&ship-desired-dy=" + shipDesiredDy.ToString();
        }
    }
}
