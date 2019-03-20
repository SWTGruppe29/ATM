using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using ATM.Classes;
using ATM.Interfaces;
using NUnit.Framework;

namespace ATMUnitTest
{
    [TestFixture]
    public class TrackCalculatorTest
    {
        private TrackCalculator _uut;
        
        [TestCase(60000, 60000, 30000, 30000,"00:00","00:15",225)]
        [TestCase(30000, 30000, 60000, 60000, "00:00", "00:30", 45)]
        [TestCase(10000,40000,40000,10000, "00:00", "00:45", 135)]
        [TestCase(40000,10000,10000,40000, "00:00", "01:30", 315)]
        
        public void TestCalcAngle(int Last_x, int Last_y, int New_x, int New_y,string dt1, string dt2, double result)
        {
            DateTime dt1DateTime = DateTime.ParseExact(dt1, "mm:ss", null);
            DateTime dt2DateTime = DateTime.ParseExact(dt2, "mm:ss", null);

            _uut = new TrackCalculator(Last_x,Last_y,New_x,New_y,dt1DateTime,dt2DateTime);
            

            Assert.That(_uut.CalculateCompassCourse(),Is.EqualTo(result));
        }

        [TestCase(60000, 60000, 30000, 30000, "00:00", "00:15", 2828.42712)]
        [TestCase(30000, 30000, 60000, 60000, "00:00", "00:30", 1414.21356)]
        [TestCase(10000, 40000, 40000, 10000, "00:00", "00:45", 942.80904)]
        [TestCase(40000, 10000, 10000, 40000, "00:00", "01:30", 471.40452)]
        public void TestHorizontalVelocity(int Last_x, int Last_y, int New_x, int New_y, string dt1, string dt2,
            double result)
        {
            DateTime dt1DateTime = DateTime.ParseExact(dt1, "mm:ss", null);
            DateTime dt2DateTime = DateTime.ParseExact(dt2, "mm:ss", null);

            _uut = new TrackCalculator(Last_x, Last_y, New_x, New_y, dt1DateTime, dt2DateTime);

            Assert.That(_uut.CalculateHorizontalVelocity(),Is.EqualTo(result));
        }
    }
}
