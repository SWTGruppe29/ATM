using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class TrackCalculator : ITrackCalculator
    {
        public double CalculateHorizontalVelocity(double Last_x, double Last_y, double New_x, double New_y)
        {
            
            double x = (New_x - Last_x) * (Math.PI / 180.0f);
            double y = (New_y - Last_y) * (Math.PI / 180.0f);
            double a = Math.Pow(Math.Sin(y / 2.0), 2)
                         + Math.Cos(Last_y * (Math.PI / 180.0f))
                         * Math.Cos(New_y * (Math.PI / 180f))
                         * Math.Pow(Math.Sin(x / 2.0), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return 3956 * c;
        }

        public double CalculateCompassCourse(double Last_x, double Last_y, double New_x, double New_y)
        {
            double xDiff = New_x - Last_x;
            double yDiff = New_y - Last_y;

            return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
        }
    }
}
