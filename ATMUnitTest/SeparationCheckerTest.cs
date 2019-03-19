using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using ATM.Interfaces;
using Castle.Core.Internal;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATMUnitTest
{
    [TestFixture]
    public class SeparationCheckerTest
    {
        /*
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


        [Test]
        public void HorizontalDistanceBetweeenTracks_CheckForCorrectDistance()
        {
            Track track1 = new Track("abc123",200,5000,1000,DateTime.Now);
            Track track2 = new Track("ABCeg", 200,7000,1000,DateTime.Now);
            Assert.That(_uut.distanceBetweenTracks(track1, track2), Is.EqualTo(2000));
        }

        [Test]
        public void HorizontalDistanceBetweeenTracks_CheckForCorrectDistance2()
        {
            Track track1 = new Track("abc123", 200, 10000, 10000, DateTime.Now);
            Track track2 = new Track("ABCeg", 200, 0, 0, DateTime.Now);
            Assert.That(_uut.distanceBetweenTracks(track1, track2), Is.EqualTo(Math.Sqrt((10000*10000)*2)));
        }

        [Test]
        public void SeparationChecker_HorizontalSeparationConflict_ReturnsTrue()
        {
            Track track1 = new Track("abc123", 200, 5000, 1000, DateTime.Now);
            Track track2 = new Track("ABCeg", 200, 7000, 1000, DateTime.Now);
            Assert.That(_uut.horizontalSeparationConflict(track1, track2), Is.EqualTo(true));
        }
        [Test]
        public void SeparationChecker_HorizontalSeparationConflict_ReturnsTrue2()
        {
            Track track1 = new Track("abc123", 200, 3000, 2000, DateTime.Now);
            Track track2 = new Track("ABCeg", 200, 6000, 1000, DateTime.Now);
            Assert.That(_uut.horizontalSeparationConflict(track1, track2), Is.EqualTo(true));
        }

        [Test]
        public void SeparationChecker_HorizontalSeparationConflict_ReturnsFalse()
        {
            Track track1 = new Track("abc123", 200, 50000, 10000, DateTime.Now);
            Track track2 = new Track("ABCeg", 200, 7000, 1000, DateTime.Now);
            Assert.That(_uut.horizontalSeparationConflict(track1, track2), Is.EqualTo(false));
        }

        [Test]
        public void SeparationChecker_VerticalSeparationConflict_ReturnsTrue()
        {
            Track track1 = new Track("abc123", 300, 50000, 10000, DateTime.Now);
            Track track2 = new Track("ABCeg", 200, 7000, 10000, DateTime.Now);
            Assert.That(_uut.verticalSeparationConflict(track1,track2),Is.EqualTo(true));
        }

        [Test]
        public void SeparationChecker_VerticalSeparationConflict_ReturnsFalse()
        {
            Track track1 = new Track("abc123", 1000, 50000, 20000, DateTime.Now);
            Track track2 = new Track("ABCeg", 200, 7000, 1000, DateTime.Now);
            Assert.That(_uut.verticalSeparationConflict(track1, track2), Is.EqualTo(false));
        }


        #endregion

        [Test]
        public void SeparationChecker_hasConflict_ReturnsTrue()
        {
            Track track1 = new Track("abc123", 300, 5000, 2000, DateTime.Now, 320,1000);
            Track track2 = new Track("ABCeg", 200, 7000, 1000, DateTime.Now,320,1000);
            Assert.That(_uut.hasConflict(track1, track2), Is.EqualTo(true));
        }

        [Test]
        public void SeparationChecker_hasConflict_HorizontalButNotVertical_ReturnsFalse()
        {
            Track track1 = new Track("abc123", 1500, 5000, 2000, DateTime.Now);
            Track track2 = new Track("ABCeg", 300, 7000, 1000, DateTime.Now);
            Assert.That(_uut.hasConflict(track1, track2), Is.EqualTo(false));
        }

        [Test]
        public void SeparationChecker_hasConflict_VerticalButNotHorizontal_ReturnsFalse()
        {
            Track track1 = new Track("abc123", 300, 10000, 40000, DateTime.Now);
            Track track2 = new Track("ABCeg", 200, 80000, 10000, DateTime.Now);
            Assert.That(_uut.hasConflict(track1, track2), Is.EqualTo(false));
        }

        [Test]
        public void SeparationChecker_hasConflict_HorizontalNorVertical_ReturnsFalse()
        {
            Track track1 = new Track("abc123", 2000, 50000, 20000, DateTime.Now);
            Track track2 = new Track("ABCeg", 200, 200000, 1000, DateTime.Now);
            Assert.That(_uut.hasConflict(track1, track2), Is.EqualTo(false));
        }

        [Test]
        public void SeparationChecker_CheckForSeparation_ContainsExpectedConflict()
        {
            List<Track> tracks = new List<Track>()
            {
                new Track("abc123", 2000, 50000, 20000, DateTime.Now),
                new Track("ABCeg", 200, 20000, 1000, DateTime.Now)
            };
            Track newTrack = new Track("abfasd",350,21000,3000,DateTime.Now);
            List<int> conflicts = _uut.CheckForSeparation(tracks, newTrack);
            Assert.That(conflicts.Contains(1),Is.EqualTo(true));
        }

        [Test]
        public void SeparationChecker_CheckForSeparation_DoesntContainUnexpectedConflict()
        {
            List<Track> tracks = new List<Track>()
            {
                new Track("abc123", 2000, 50000, 20000, DateTime.Now),
                new Track("ABCeg", 200, 20000, 1000, DateTime.Now)
            };
            Track newTrack = new Track("abfasd", 350, 21000, 3000, DateTime.Now);
            List<int> conflicts = _uut.CheckForSeparation(tracks, newTrack);
            Assert.That(conflicts.Contains(0), Is.EqualTo(false));
        }

        [Test]
        public void SeparationChecker_CheckForSeparation_DoesntConflictWithSelf()
        {
            List<Track> tracks = new List<Track>()
            {
                new Track("abc123", 2000, 50000, 20000, DateTime.Now),
                new Track("ABCeg", 200, 20000, 1000, DateTime.Now)
            };
            Track newTrack = new Track("abc123", 2000, 51000,20000, DateTime.Now);
            List<int> conflicts = _uut.CheckForSeparation(tracks, newTrack);
            Assert.That(conflicts.Contains(0), Is.EqualTo(false));
        }


    */


    }
}
