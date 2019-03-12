using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class ATMSystem : IATMSystem
    {
        private List<Track> Tracks;

        public void AddTrack(ITrack track)
        {
            throw new NotImplementedException();
        }

        public void ReceiverOnTransponderReady()
        {
            throw new NotImplementedException();
        }
    }
}
