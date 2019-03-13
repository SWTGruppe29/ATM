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

        [Test]
        public void CheckIfTracksConflict_CourseAndSpeedNotInitialized_ListLengthIs0()
        {
            Track newTrack = new Track("adfv32",2300,41092,70000,DateTime.Now);
            List<Track> tracks = new List<Track>
            {
                new Track("ABD21",2000,40000,40000,DateTime.Now),
                new Track("abda232",1200,4002,50000,DateTime.Now)
            };
            _uut.CheckForSeparation(tracks, newTrack).Count.Equals(0);
        }
    }
}
