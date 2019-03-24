using System;
using System.Collections.Generic;
using System.Drawing;
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
        public IAirSpace CreateAirSpace()
        {
            return new AirSpace(10000, 90000, 90000, 10000, 20000, 500);
        }

        public ICondition CreateCondition()
        {
            return new SeparationCondition(300,5000);
        }

        public IConsolePrinter CreateConsolePrinter()
        {
            return new ConsolePrinter();
        }

        public ILogger CreateLogger()
        {
            return new Logger();
        }

        public ISeparationChecker CreateSeparationChecker()
        {
            return new SeparationChecker(CreateAirSpace(),CreateCondition());
        }

        public ITransponderReceiver CreateTransponderReceiver()
        {
            return TransponderReceiverFactory.CreateTransponderDataReceiver();
        }
    }
}
