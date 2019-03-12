using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class AirSpace : IAirSpace
    {
        private int westBoundary;
        private int eastBoundary;
        private int northBoundary;
        private int southBoundary;
        private int upperBoundary;
        private int lowerBoundary;


        public AirSpace(int w, int e, int n, int s, int u, int l)
        {
            westBoundary = w;
            eastBoundary = e;
            northBoundary = n;
            southBoundary = s;
            upperBoundary = u;
            lowerBoundary = l;

        }

        public int getWestBoundary()
        {
            return westBoundary;
        }

        public int getEastBoundary()
        {
            return eastBoundary;
        }

        public int getNorthBoundary()
        {
            return northBoundary;
        }

        public int getSouthBoundary()
        {
            return southBoundary;
        }

        public int getUpperBoundary()
        {
            return upperBoundary;
        }

        public int getLowerBoundary()
        {
            return lowerBoundary;
        }
    }
}
