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
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var system = new ATM.Classes.ATMSystem(receiver);

            while (true)
            {
                Thread.Sleep(5000);
            }
        }
    }
}
