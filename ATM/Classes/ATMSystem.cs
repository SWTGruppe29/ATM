﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using TransponderReceiver;

namespace ATM.Classes
{
    public class ATMSystem : IATMSystem, ITrack, IAbstractATMFactory
    {
        private List<Track> Tracks;
        private TrackCalculator calc;
        private ITransponderReceiver receiver;
        private AirSpace airspace;
        private List<string> datastring;
        
        

        public ATMSystem(ITransponderReceiver receiver)
        {

            this.receiver = receiver;
            this.receiver.TransponderDataReady += ReceiverOnTransponderReady;

        }

        private void ReceiverOnTransponderReady(object sender, RawTransponderDataEventArgs e)
        {
            //Receive data, calls list foreach.
            foreach (var data in e.TransponderData)
            {
                List(data);
            }

            //makes Track
            
        }

        public void AddTrack(ITrack track)
        {
            Tracks.Add(null);
        }

        

        public void List(string s)
        {
            datastring = s.Split(';').Reverse().ToList<string>();
            datastring.Reverse();

            
        }
        
        

        

       

        

        


    }
}
