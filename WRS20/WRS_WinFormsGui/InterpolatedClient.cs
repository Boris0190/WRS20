using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WRS20_Logic.JsonObjects;

namespace WRS_WinFormsGui
{
    class InterpolatedClient : JClient
    {
        public InterpolatedClient(String contentString)
            : base(contentString)
        {

        }

        private object lockData = new object();
        public void UpdateData(JClient rawClientData)
        {
            lock (lockData)
            {
                this.contentString = rawClientData.ContentString;
                parseObjects();
            }
        }

        public void Tick(TimeSpan ticks)
        {
            //double timeDelta = ((DateTime.Now - lastTick).TotalMilliseconds / 1000);
            double timeDelta = ticks.TotalSeconds;
            lock (lockData)
            {
                this.X += (this.Dx * timeDelta);
                this.Y += (this.Dy * timeDelta);
            }
        }
    }
}
