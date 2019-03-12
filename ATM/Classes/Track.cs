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
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public double CurrentCompCourse { get; set; }
        public double Velocity { get; set; }
       
    }
}
