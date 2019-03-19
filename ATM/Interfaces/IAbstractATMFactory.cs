using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;

namespace ATM.Interfaces
{
    public interface IAbstractATMFactory
    {
        ConcreteATMFactory CreateAbstractAtmFactory();
    }
}
