using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace ATMUnitTest
{
    [TestFixture]
    public class ConsolePrinterTest
    {
        [Test]
        public void TwoTracksInConflictAndAirspace()
        {
            DateTime time1 = new DateTime(2018, 03, 12, 14, 50, 25, 543);
            DateTime time2 = new DateTime(2016, 03, 12, 15, 50, 25, 543);

            List<Track> uutTracks = new List<Track>()
            {
                new Track("5412BJ", 12345, 15312,84393, time1, 154.5433, 1000),
                new Track("5417KJ", 15445, 15342,84393, time1, 154.233, 987.123)
            };

            string[] Tags = new string[2]
            {
                "612HJ",
                "12353"
            };

            ConsolePrinter.Print(uutTracks, Tags);
        }

        [Test]
        public void NoTracksInList()
        {
            List<Track> uutTracks = new List<Track>();
            ConsolePrinter.Print(uutTracks, null);
        }


        [Test]
        public void TwoTracksInAirspace()
        {
            DateTime time1 = new DateTime(2018, 03, 12, 14, 50, 25, 543);
            DateTime time2 = new DateTime(2016, 03, 12, 15, 50, 25, 543);

            List<Track> uutTracks = new List<Track>()
            {
                new Track("5412BJ", 12345, 15312,84393, time1, 154.5433, 1000),
                new Track("5417KJ", 15445, 15342,84393, time1, 154.233, 987.123)
            };

            ConsolePrinter.Print(uutTracks, null);
        }

    }
}
