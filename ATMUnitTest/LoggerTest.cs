using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace ATMUnitTest
{
    [TestFixture]
    public class LoggerTest
    {
        [Test]
        public void Print2Tags()
        {
            var uut = new ATM.Classes.Logger();

            DateTime time = new DateTime(2018, 03, 12, 14,50,25,543);

            List<string> tags = new List<string>() {"5B3123", "34123B"};

            uut.LogMessage(time, tags);
        }

        [Test]
        public void Print3Tags()
        {
            var uut = new ATM.Classes.Logger();

            DateTime time = new DateTime(2017, 03, 12, 14, 50, 25, 543);

            List<string> tags = new List<string>() { "5B3123", "34123B", "64BJ92"};

            uut.LogMessage(time, tags);
        }
    }
}
