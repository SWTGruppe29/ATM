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
        
        [SetUp]
        public void Setup()
        {
            _uut = new TrackCalculator();
        }

        
        [TestCase(60000, 60000, 30000, 30000,225)]
        [TestCase(30000, 30000, 60000, 60000, 45)]
        [TestCase(10000,40000,40000,10000,135)]
        [TestCase(40000,10000,10000,40000,315)]
        
        public void TestCalcDegress(double Last_x, double Last_y, double New_x, double New_y, double result)
        {
            
            Assert.That(_uut.CalculateCompassCourse(Last_x, Last_y, New_x, New_y),Is.EqualTo(result));
        }
    }
}
