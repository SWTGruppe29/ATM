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
    public class Class1
    {
        private TrackCalculator _uut;

        
        [TestCase(60000, 60000, 30000, 30000,225)]
        [TestCase(30000, 30000, 60000, 60000, 45)]
        [TestCase(10000,40000,40000,10000,135)]
        [TestCase(40000,10000,10000,40000,315)]
        
        public void TestCalcDegress(int Last_x, int Last_y, int New_x, int New_y, double result)
        {
            _uut = new TrackCalculator(Last_x,Last_y,New_x,New_y);

            Assert.That(_uut.CalculateCompassCourse(),Is.EqualTo(result));
        }
    }
}
