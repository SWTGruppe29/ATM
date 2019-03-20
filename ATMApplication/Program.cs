using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATM.Classes;
using TransponderReceiver;
using ATM;


namespace ATMApplication
{
    class Program
    {
        

        public static void Main(string[] args)
        {
            ConcreteATMFactory ATMfactory = new ConcreteATMFactory();
            ATMSystem atm = ATMfactory.CreateAtmSystem();




            //var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            //var airSpace = new AirSpace(10000, 90000, 90000, 10000, 20000, 500);
            //var separationCondition = new SeparationCondition(300, 5000);

            //var consolePrinter = new ConsolePrinter();
            //var logger = new Logger();
            //var trackCalc = new TrackCalculator();
            //var spChecker = new SeparationChecker(airSpace, separationCondition);
            //ATMSystem atm = new ATM.Classes.ATMSystem(receiver, airSpace, separationCondition, consolePrinter, logger, trackCalc, spChecker);







            while (true)
            {
                Thread.Sleep(1000);
            }
            
        }
    }
}
