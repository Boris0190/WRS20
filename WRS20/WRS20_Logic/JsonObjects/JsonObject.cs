using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_Logic.JsonObjects
{
    public abstract class JsonObject
    {
        protected String contentString;
        public String ContentString
        {
            get { return contentString; }
        }

        protected bool isValid = false;
        public bool IsValid
        {
            get { return isValid; }
        }

        public JsonObject(String contentString)
        {
            this.contentString = contentString;
            if (contentString == "")
            {
                isValid = false;
                return;
            }
            try
            {
                parseObjects();
                isValid = true;
            }
            catch { throw new Exception("Debug: invalid Json String?"); }
        }

        protected abstract void parseObjects();
    }
}
