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
    public class ConcreteATMFactory : IAbstractATMFactory
    {
        public ATMSystem CreateATMSystem()
        {
            return new ATMSystem(TransponderReceiverFactory.CreateTransponderDataReceiver(),
                new AirSpace(),
                new SeparationCondition(),
                new ConsolePrinter(),
                new Logger(),
                new TrackCalculator(),
                new SeparationChecker(),
                );

    }
    }
}
