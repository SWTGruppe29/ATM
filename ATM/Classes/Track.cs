using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class Track : ITrack
    {
        public int Tag { get; set; }
        public int Altitude { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int CurrentCompCourse { get; set; }
        public int Velocity { get; set; }
    }
}
