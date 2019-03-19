using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using ATM.Interfaces;
using Castle.Core.Internal;
using NSubstitute;
using NUnit.Framework;

namespace ATMUnitTest
{
    [TestFixture]
    public class SeparationCheckerTest
    {
        private SeparationChecker _uut;
        [SetUp]
        public void Init()
        {
            IAirSpace airSpace = new AirSpace(0, 80000, 80000, 0, 2500, 500);
            ICondition condition = new SeparationCondition(300, 5000);
            _uut = Substitute.For<SeparationChecker>(airSpace,condition);
        }


        #region InternalFunctionTests

        [Test]
        public void AltitudeSeparation_CheckValidityForNegative()
        {
            Assert.That(_uut.AltitudeSeparation(100,200), Is.EqualTo(100));
        }

        [Test]
        public void AltitudeSeparation_CheckValidityForPositive()
        {
            Assert.That(_uut.AltitudeSeparation(200,100), Is.EqualTo(100));
        }



        #endregion


        [Test]
        public void CheckIfTracksConflict_CourseAndSpeedNotInitialized_ListLengthIs0()
        {
            Track newTrack = new Track("adfv32",2300,41092,70000,DateTime.Now);
            List<Track> tracks = new List<Track>
            {
                new Track("ABD21",2000,40000,40000,DateTime.Now),
                new Track("abda232",1200,4002,50000,DateTime.Now)
            };
            Assert.That(_uut.CheckForSeparation(tracks, newTrack).IsNullOrEmpty(), Is.EqualTo(true));
        }

        [Test]
        public void CheckIfTracksConflict_CoursesDontConflict_ListLength0()
        {
            Track newTrack = new Track("adfv32", 2100, 0, 20000, DateTime.Now,90,1000);
            List<Track> tracks = new List<Track>
            {
                new Track("ABD21",2000,40000,40000,DateTime.Now,270,1000)
            };
            Assert.That(_uut.CheckForSeparation(tracks, newTrack).IsNullOrEmpty, Is.EqualTo(true));
        }


        /*[Test]
        public void CheckIfTracksConflict_1CourseIntersects_ListLength1()
        {
            Track newTrack = new Track("adfv32", 2100, 0, 20000, DateTime.Now, 90, 1000);
            List<Track> tracks = new List<Track>
            {
                new Track("ABD21",2000,40000,40000,DateTime.Now,180,1000)
            };
            Assert.That(_uut.CheckForSeparation(tracks, newTrack).Count, Is.EqualTo(1));
        }*/


    }
}
