using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;
using TransponderReceiver;

namespace ATM.Classes
{
    public class ATMSystem : IATMSystem, ITrack
    {
        private List<Track> Tracks;
        private List<string> datastring;
        private List<object> objectlist;

        private IAirSpace _airspace;
        private ICondition _condition;
        private IConsolePrinter _consolePrinter;
        private ILogger _logger;
        private ISeparationChecker _separationChecker;
        private ITrackCalculator _calc;
        private ITransponderReceiver receiver;
        

        public ATMSystem(ITransponderReceiver receiver)
        {
            this.receiver = receiver;
            this.receiver.TransponderDataReady += ReceiverOnTransponderReady;
        }

        /// <summary>
        /// Receives all of the necesary components to build the ATM System
        /// </summary>
        /// <param name="receiver">Receives data from the tracks</param>
        /// <param name="airspace">Domain class for Airspace bounds</param>
        /// <param name="condition">Domain class that holds the minimum vertical and horizontal distance</param>
        /// <param name="consolePrinter">Prints the airplanes to the console</param>
        /// <param name="logger">Logs the airplanes that are too close</param>
        /// <param name="track">Domain class that keeps the trackdata</param>
        /// <param name="trackCalculator">Class that calculates the airplanes speed and course</param>
        /// <param name="separationChecker">Class to check that airplanes are separated</param>
        public ATMSystem(ITransponderReceiver receiver, 
            IAirSpace airspace, 
            ICondition condition, 
            IConsolePrinter consolePrinter, 
            ILogger logger,
            ITrackCalculator trackCalculator,
            ISeparationChecker separationChecker)
        {
            this.receiver = receiver;
            this.receiver.TransponderDataReady += ReceiverOnTransponderReady;

            _airspace = airspace;
            _condition = condition;
            _consolePrinter = consolePrinter;
            _logger = logger;
            _separationChecker = separationChecker;
            _calc = trackCalculator;
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

        public void TypeConverter()
        {
            objectlist[0] = datastring[0];
            Int32.TryParse(datastring[1], out int x);
            objectlist[1] = x;
            Int32.TryParse(datastring[2], out int y);
            objectlist[2] = y;
            Int32.TryParse(datastring[2], out int alt);
            objectlist[3] = alt;


        }
        

        

       

        

        


    }
}
