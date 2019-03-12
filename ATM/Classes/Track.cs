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
        public string Tag { get; set; }
        public int Altitude { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public double CurrentCompCourse { get; set; }
        public double Velocity { get; set; }
    }
}
