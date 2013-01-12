using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.HTTPCommands
{
    public class CTeam : HTTPCommand
    {
        private string teamName;
        public string TeamName
        {
            get { return teamName; }
            set
            {
                teamName = value;
                CalculateParamString();
            }
        }

        private string teamColor;
        public string TeamColor
        {
            get { return teamColor; }
            set
            {
                teamColor = value;
                CalculateParamString();
            }
        }

        public CTeam()
        {
            name = "team";
        }

        public override void CalculateParamString()
        {
            paramString = "team-name=" + teamName + "&team-color=" + teamColor;
        }
    }
}
