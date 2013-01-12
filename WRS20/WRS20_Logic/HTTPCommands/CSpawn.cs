using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.HTTPCommands
{
    public class CSpawn : HTTPCommand
    {
        private Guid teamPrivateKey;
        public Guid TeamPrivateKey
        {
            get { return teamPrivateKey; }
            set
            {
                teamPrivateKey = value;
                CalculateParamString();
            }
        }

        private string shipName;
        public string ShipName
        {
            get { return shipName; }
            set
            {
                shipName = value;
                CalculateParamString();
            }
        }

        public CSpawn()
        {
            name = "spawn";
        }
        public override void CalculateParamString()
        {
            paramString = "team-private-key=" + teamPrivateKey + "&ship-name=" + shipName;
        }
    }
}
