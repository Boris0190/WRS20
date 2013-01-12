using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.HTTPCommands
{
    public class CDump : HTTPCommand
    {
        public CDump()
        {
            name = "dump";
        }
        public override void CalculateParamString()
        {
        }
    }
}
