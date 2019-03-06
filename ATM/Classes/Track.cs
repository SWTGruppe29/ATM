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
        

    }
}
