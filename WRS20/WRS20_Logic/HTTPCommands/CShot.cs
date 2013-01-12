using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.HTTPCommands
{
    class CShot : HTTPCommand
    {
        private double dx;
        public double Dx
        {
            get { return dx; }
            set 
            { 
                dx = value;
                CalculateParamString();
            }
        }

        private double dy;
        public double Dy
        {
            get { return dy; }
            set 
            { 
                dy = value;
                CalculateParamString();
            }
        }


        public CShot()
        {
            name = "shot";
        }

        public override void CalculateParamString()
        {
            paramString = "dx=" + dx.ToString() + "&dy=" + dy.ToString();
        }
    }
}
