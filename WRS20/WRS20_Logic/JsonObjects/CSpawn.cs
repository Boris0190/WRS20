using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.JsonObjects
{
    class CSpawn : JsonObject
    {
        public CSpawn(String contentString)
            : base(contentString)
        { }
        protected override void parseObjects()
        {
            throw new NotImplementedException();
        }
    }
}
