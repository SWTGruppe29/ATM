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
        private ITransponderReceiver _fakeTransponderReceiver;
        [SetUp]
        public void Setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = Substitute.For<ATMSystem>(new ATMTestFactory(),_fakeTransponderReceiver);
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
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Test event received
            _uut.Received().ReceiverOnTransponderReady(this,args);

        }

        [Test]
        public void ATMSystemUnitTests_TestForLogEventNotFired_EmptyList()
        {
            SeparationLogEventArgs testArgs = null;
            _uut.SeparationLogDataReady += 
                (object o, SeparationLogEventArgs a) => { testArgs = a; };
            List<string> testData = new List<string>();
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);

            //Raise event
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Test event not fired
            Assert.That(testArgs, Is.Null);
        }

        [Test]
        public void ATMSystemUnitTests_TestForConsoleEventNotFired_EmptyList()
        {
            ConsoleSeparationEventArgs testArgs = null;
            _uut.ConsoleSeparationDataReady +=
                (object o,ConsoleSeparationEventArgs a) => { testArgs = a; };

            List<string> testData = new List<string>();
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);

            //Raise event
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Test event not fired
            Assert.That(testArgs, Is.Null);
        }

        [Test]
        public void ATmSystemUnitTests_TestForConsoleEventFired_NonEmptyList()
        {
            ConsoleSeparationEventArgs testArgs = null;
            _uut.ConsoleSeparationDataReady +=
                (object o, ConsoleSeparationEventArgs a) => { testArgs = a; };

            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);

            //Raise event
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Test event fired
            Assert.That(testArgs, Is.Not.Null);
        }

        [Test]
        public void ATMSystemUnitTests_TestForLogEventFired_NonEmptyList()
        {
            SeparationLogEventArgs testArgs = null;
            _uut.SeparationLogDataReady +=
                (object o, SeparationLogEventArgs a) => { testArgs = a; };
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);
            
            //Raise event
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Test event not fired
            Assert.That(testArgs, Is.Not.Null);
        }

        [Test]
        public void ATMSystemUnitTests_CheckIfTrackIsInListIsCalledForEachTrack_3Tracks()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);

            //Raise event
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            _uut.ReceivedWithAnyArgs(3).CheckIfTrackIsInList("abc");
        }

        [Test]
        public void ATMSystemUnitTests_CheckThatTracksUpdatedAndNotDuplicated_UpdatedTracks()
        {
            ConsoleSeparationEventArgs testArgs = null;
            _uut.ConsoleSeparationDataReady +=
                (object o, ConsoleSeparationEventArgs a) => { testArgs = a; };

            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);

            //Raise event
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Raise event with updated data
            testData = new List<string>();
            testData.Add("ATR423;45003;30000;15200;20151006213456789");
            testData.Add("BCD123;20000;70890;11030;20151006213456789");
            testData.Add("XYZ987;22059;79654;5000;20151006213456789");
            args = new RawTransponderDataEventArgs(testData);
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);
            //Check that data still only contains 3 tracks
            Assert.That(testArgs.tracks.Count,Is.EqualTo(3));
        }

        [Test]
        public void ATMSystemUnitTests_CheckThatOutOfBoundsFlightNotAdded_UpdatedTracks()
        {
            ConsoleSeparationEventArgs testArgs = null;
            _uut.ConsoleSeparationDataReady +=
                (object o, ConsoleSeparationEventArgs a) => { testArgs = a; };

            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);

            //Raise event
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Raise event with updated data
            testData = new List<string>();
            testData.Add("ATR423;45003;30000;15200;20151006213456789");
            testData.Add("BCD123;20000;92890;11030;20151006213456789"); //Flight now out of bounds
            testData.Add("XYZ987;22059;79654;5000;20151006213456789");
            args = new RawTransponderDataEventArgs(testData);
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);
            //Check that data still only contains 3 tracks
            Assert.That(testArgs.tracks.Count, Is.EqualTo(2));
        }

        [Test]
        public void ATMSystemUnitTests_CheckThatExpectedAmmountOfConflictsExist_Expected1ConflictAfterUpdate()
        {
            ConsoleSeparationEventArgs testArgs = null;
            _uut.ConsoleSeparationDataReady +=
                (object o, ConsoleSeparationEventArgs a) => { testArgs = a; };

            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);

            //Raise event
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Raise event with updated data
            testData = new List<string>();
            testData.Add("ATR423;45003;30000;15200;20151006213456789");
            testData.Add("BCD123;20000;82890;11030;20151006213456789"); //Conflict
            testData.Add("XYZ987;22059;81654;11000;20151006213456789"); //Conflict
            args = new RawTransponderDataEventArgs(testData);
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);
            //Check that list contains 1 conflict
            Assert.That(testArgs.conflictList.Count, Is.EqualTo(1));
        }

        [Test]
        public void ATMSystemUnitTests_CheckThatConflictDeletedAfterUpdate_1ConflictDeleted()
        {
            ConsoleSeparationEventArgs testArgs = null;
            _uut.ConsoleSeparationDataReady +=
                (object o, ConsoleSeparationEventArgs a) => { testArgs = a; };
            List<string> testData = new List<string>();
            

            testData = new List<string>();
            testData.Add("ATR423;45003;30000;15200;20151006213456789");
            testData.Add("BCD123;20000;82890;11030;20151006213456789"); //Conflict
            testData.Add("XYZ987;22059;81654;11000;20151006213456789"); //Conflict
            RawTransponderDataEventArgs args = new RawTransponderDataEventArgs(testData);
           
            //Raise event
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Test that there is an initial conflict
            Assert.That(testArgs.conflictList.Count,Is.EqualTo(1));

            //Raise event with updated data
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");
            args = new RawTransponderDataEventArgs(testData);
            _fakeTransponderReceiver.TransponderDataReady += Raise.EventWith(this, args);

            //Check that conflict doesnt exist anymore
            Assert.That(testArgs.conflictList.Count, Is.EqualTo(0));
        }



    }
}
