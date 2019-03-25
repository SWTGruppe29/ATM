using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ATMUnitTest
{
    [TestFixture]
    class AirSpaceTest
    {
        private AirSpace _uut;
        [SetUp]
        public void Init()
        {
            _uut = Substitute.For<AirSpace>(10000, 90000, 90000, 10000, 2500, 500);
        }

        [TestCase(12000,50000,2000)]
        [TestCase(50000,20000,2100)]
        [TestCase(11000,80000,700)]
        public void AirSpaceTest_IsInAirSpace_TestsReturnTrue(int x, int y, int alt)
        {
            var result = _uut.IsInAirSpace(x, y, alt);
            Assert.That(result,Is.EqualTo(true));
        }

        [TestCase(5000, 40000, 2010)]
        [TestCase(92000,2000,500)]
        [TestCase(20000,93000,700)]
        [TestCase(24000,52000,3000)]
        public void AirSpaceTest_IsInAirSpace_TestsReturnFalse(int x, int y, int alt)
        {
            var result = _uut.IsInAirSpace(x, y, alt);
            Assert.That(result,Is.EqualTo(false));
        }


    }
}
