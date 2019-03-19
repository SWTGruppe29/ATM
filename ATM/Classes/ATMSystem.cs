﻿using System;
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
        private int x, y, alt;
        private TrackCalculator calc;
        private List<string> datastring;
        private List<object> objectlist;
        private DateTime dateTimeNew;
        private string flightNum;
        private Track newTrack;

        private ITransponderReceiver receiver;
        private IAirSpace _airSpace;
        private ICondition _condition;
        private IConsolePrinter _consolePrinter;
        private ILogger _logger;
        private ISeparationChecker _separationChecker;
        private ITrackCalculator _calc;

        public event EventHandler<SeparationLogEventArgs> SeparationLogDataReady;
        public event EventHandler<ConsoleSeparationEventArgs> ConsoleSeparationDataReady;

        public ATMSystem(ITransponderReceiver receiver)
        {
            this.receiver = receiver;
            this.receiver.TransponderDataReady += ReceiverOnTransponderReady;
            this.SeparationLogDataReady += _logger.SeparationLogDataHandler;
            this.ConsoleSeparationDataReady += _consolePrinter.ConsoleSeparationDataHandler;
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
            this.SeparationLogDataReady += _logger.SeparationLogDataHandler;
            this.ConsoleSeparationDataReady += _consolePrinter.ConsoleSeparationDataHandler;


            _airSpace = airspace;
            _condition = condition;
            _consolePrinter = consolePrinter;
            _logger = logger;
            _separationChecker = separationChecker;
            _calc = trackCalculator;
            datastring = new List<string>() {""};
        }


        private void ReceiverOnTransponderReady(object sender, RawTransponderDataEventArgs e)
        {
            //Receive data, calls list foreach.
            foreach (var data in e.TransponderData)
            {
                List(data);
            }
            
            //Converts datastring to separate variables and appropiate types.
            TypeConverter();


            if (_airSpace.IsInAirSpace(x, y))
            {
                int index = CheckIfTrackIsInList(flightNum);
                if (index > 0)
                {

                    calc = new TrackCalculator(Tracks[index].XCoordinate, Tracks[index].YCoordinate, x, y,
                        Tracks[index].LastDateUpdate, dateTimeNew);
                    newTrack = new Track(flightNum, x, y, alt, dateTimeNew, calc.CalculateCompassCourse(),
                        calc.CalculateHorizontalVelocity());
                    Tracks[index] = newTrack;
                }
                else
                {
                    Track newTrack = new Track(flightNum, x, y, alt, dateTimeNew);
                    Tracks.Add(newTrack);
                }

                List<Conflict> ConflictList = _separationChecker.CheckForSeparation(Tracks, newTrack);
                _separationChecker = new SeparationChecker(_airSpace, _condition);
                if (ConflictList.Count > 1)
                {
                    SeparationLogEventArgs LogArgs = new SeparationLogEventArgs();
                    LogArgs.ConflictList = ConflictList;
                    SeparationLogDataReady?.Invoke(this, LogArgs);
                    ConsoleSeparationEventArgs conArgs = new ConsoleSeparationEventArgs();
                    ConsoleSeparationDataReady?.Invoke(this, conArgs);


                }
            }

        }
       

        public int CheckIfTrackIsInList(string tag)
        {
            for (int i = 0; i < Tracks.Count; i++)
            {
                if (Tracks[i].Tag == tag)
                    return i;
            }

            return -1;
        }

        private void List(string s)
        {
            datastring = s.Split(';').Reverse().ToList<string>();
            datastring.Reverse();
        }

        private void dateConverter()
        {
            dateTimeNew = DateTime.ParseExact(datastring[4],"yyyyMMddHHmmssfff",null);
            
        }

        private void TypeConverter()
        {
                Int32.TryParse(datastring[1], out x);
                Int32.TryParse(datastring[2], out y);
                Int32.TryParse(datastring[3], out alt);
                dateConverter();   
        }


        public void AddTrack(ITrack track)
        {
            throw new NotImplementedException();
        }
    }
}
