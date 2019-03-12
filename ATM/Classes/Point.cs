using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Classes
{
    class Point
    {
        public Point(int x, int y)
        {
            Y = y;
            X = x;
        }
        public int Y { get; set; }
        public int X { get; set; }
    }
}
