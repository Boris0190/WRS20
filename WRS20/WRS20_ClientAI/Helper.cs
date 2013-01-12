using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WRS20_ClientAI
{
    public static class Helper
    {
        public static double getDistanceToPoint(double fromX, double fromY, double toX, double toY)
        {
            double dx = fromX - toX;
            double dy = fromY - toY;

            double distance = Math.Sqrt(dx * dx + dy * dy);

            return distance;
        }
    }
}
