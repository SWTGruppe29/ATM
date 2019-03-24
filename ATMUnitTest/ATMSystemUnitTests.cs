using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using ATM.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace ATMUnitTest
{
    [TestFixture]
    class ATMSystemUnitTests
    {
        private ATMSystem _uut;
        [SetUp]
        public void Init()
        {
            _uut = Substitute.For<ATMSystem>(new ATMTestFactory());
        }


        [Test]
        public void ATMSystemUnitTests_TestForEventHandlerCalledOnDataReady_EventHandlerIsCalled()
        {
            //Make test data
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);

            //Raise event
            _uut.receiver.TransponderDataReady += Raise.EventWith(args);

        }

    }
}
