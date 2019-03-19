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
        
        [TestCase(60000, 60000, 30000, 30000,"20000101000000000","20000101000015000",225)]
        [TestCase(30000, 30000, 60000, 60000, "20000101000000000", "20000101000030000", 45)]
        [TestCase(10000,40000,40000,10000, "20000101000000000", "20000101000100000", 135)]
        [TestCase(40000,10000,10000,40000, "20000101000000000", "20000101000115000", 315)]
        
        public void TestCalcAngle(int Last_x, int Last_y, int New_x, int New_y,string dt1, string dt2, double result)
        {
            DateTime dt1DateTime = DateTime.ParseExact(dt1, "yyyyMMddHHmmssfff", null);
            DateTime dt2DateTime = DateTime.ParseExact(dt2, "yyyyMMddHHmmssfff", null);

            _uut = new TrackCalculator(Last_x,Last_y,New_x,New_y,dt1DateTime,dt2DateTime);
            

            Assert.That(_uut.CalculateCompassCourse(),Is.EqualTo(result));
        }
    }
}
