using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface IAirSpace
    {
        int getWestBoundary();
        int getEastBoundary();
        int getNorthBoundary();
        int getSouthBoundary();
        int getUpperBoundary();
        int getLowerBoundary();
    }
}
