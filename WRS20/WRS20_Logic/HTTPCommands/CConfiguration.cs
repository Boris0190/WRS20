using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.HTTPCommands
{
    public class CConfiguration : HTTPCommand
    {
        public CConfiguration()
        {
            name = "configuration";
        }
        public override void CalculateParamString()
        {
        }
    }
}
