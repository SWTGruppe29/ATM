using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMUnitTest
{

    // Saves file in ATM\ATMUnitTest\bin\Debug

    
    [TestFixture]
    public class FactoryTest
    {
        [Test]
        public void InstantiateFactory()
        {
            var ATMSystem = new ATMSystem(new ConcreteATMFactory(), TransponderReceiverFactory.CreateTransponderDataReceiver());
        }
    }
    
    
}
