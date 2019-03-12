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
            double deltaX = New_x - New_y;

            double angle = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;

            if (angle < 0)
            {
                angle += 2*Math.PI;
            }
            
            return (360-(Trim(angle)-90));
        }

        public double Trim(double deg)
        {
            return (Math.Round(deg, 5));
        }
    }
}
