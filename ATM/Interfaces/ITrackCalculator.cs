using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface ITrackCalculator
    {
        double CalculateHorizontalVelocity(double Last_x, double Last_y, double New_x, double New_y);
        double CalculateCompassCourse(double Last_x, double Last_y, double New_x, double New_y);

    }
}
