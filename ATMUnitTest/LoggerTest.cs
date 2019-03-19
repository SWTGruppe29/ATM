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

    // Saves file in ATM\ATMUnitTest\bin\Debug

    [TestFixture]
    public class LoggerTest
    {
        [Test]
        public void Print2Tags()
        {
            var uut = new ATM.Classes.Logger();

            DateTime time = new DateTime(2018, 03, 12, 14,50,25,543);

            List<Conflict> tags = new List<Conflict>()
            {
                new Conflict("Aj123j", "123BJH"),
                new Conflict("123918A", "1231nJ")
            };

            uut.LogMessage(tags);
        }

        [Test]
        public void Print3Tags()
        {
            var uut = new ATM.Classes.Logger();

            DateTime time = new DateTime(2017, 03, 12, 14, 50, 25, 543);

            List<Conflict> tags = new List<Conflict>()
            {
                new Conflict("Aj123j", "123BJH"),
                new Conflict("123918A", "1231nJ"),
                new Conflict("198BQ8A", "1U6J1J")
            };

            uut.LogMessage(tags);
        }
    }
}
