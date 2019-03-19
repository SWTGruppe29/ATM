using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using TransponderReceiver;

namespace ATM.Classes
{
    public class ConcreteATMFactory : IATMFactory
    {

        public ATMSystem CreateAtmSystem()
        {
            var airSpace = new AirSpace(10000, 90000, 90000, 10000, 20000, 500);
            var separationCondition = new SeparationCondition(300, 5000);

            return new ATMSystem(TransponderReceiverFactory.CreateTransponderDataReceiver(),
                airSpace,
                separationCondition,
                new ConsolePrinter(),
                new Logger(),
                new TrackCalculator(),
                new SeparationChecker(airSpace, separationCondition)
            );
        }
    }
}
