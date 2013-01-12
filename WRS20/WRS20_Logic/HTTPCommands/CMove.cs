using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.HTTPCommands
{
    public class CMove : HTTPCommand
    {
        private double dx=0;
        public double Dx
        {
            get { return dx; }
            set 
            {
                dx = value;
                CalculateParamString();
            }
        }
        private double dy = 0;
        public double Dy
        {
            get { return dy; }
            set 
            {
                dy = value;
                CalculateParamString(); 
            }
        }

        public CMove()
        {
            name = "move";
        }


        public override void CalculateParamString()
        {
            paramString = "dx=" + dx.ToString() + "&dy=" + dy.ToString();
        }
    }
}
