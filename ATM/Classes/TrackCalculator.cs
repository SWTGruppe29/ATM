using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace ATM.Classes
{
    public class TrackCalculator : ITrackCalculator
    {
        public double CalculateHorizontalVelocity(double Last_x, double Last_y, double New_x, double New_y)
        {
            return Last_y;
        }

        public double CalculateCompassCourse(double Last_x, double Last_y, double New_x, double New_y)
        {
            double deltaY = New_y - Last_y;
            double deltaX = New_x - Last_x;

            double angle = Math.Atan2(deltaY, deltaX) * (180 / Math.PI);

            angle = 360 - ((angle) - 90);

            if (angle < 0)
            {
                angle += 360;
            }

            if (angle > 360)
            {
                angle -= 360;
            }

            return Trim(angle);
        }

        public double Trim(double deg)
        {
            return (Math.Round(deg, 5));
        }
    }
}
