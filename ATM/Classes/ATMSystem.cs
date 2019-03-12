using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class ATMSystem : IATMSystem, ITrack, IAbstractATMFactory
    {
        private List<Track> Tracks;
        private TrackCalculator calc;

        public void AddTrack(ITrack track)
        {
            Tracks.Add(null);
        }

        public void ReceiverOnTransponderReady()
        {
            throw new NotImplementedException();
        }

        

        


    }
}
