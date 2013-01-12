using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.HTTPCommands
{
    public class CRadar : HTTPCommand
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

        public CRadar()
        {
            name = "radar";
        }

        public override void CalculateParamString()
        {
            paramString = "ship-private-key=" + shipPrivateKey.ToString();
        }
    }
}
