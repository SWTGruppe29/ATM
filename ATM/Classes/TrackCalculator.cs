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
        
        private DateTime dt1, dt2;

        public TrackCalculator()
        {
            P1 = new Point();
            P2 = new Point();
            


        }
        
        public double CalculateHorizontalVelocity()
        {
            return 123123; 
        }

        public double CalculateCompassCourse()
        {
            double deltaY = P2.Y - P1.Y;
            double deltaX = P2.X - P1.X;

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

        public double Distance()
        {
            double deltaY = P2.Y - P1.Y;
            double deltaX = P2.X - P1.X;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }


        #region properties

        public Point P1 { get; set; }

        public Point P2
        {
            get => P2;
            set => P1 = value;
        }

        #endregion
    }

}
