using System;
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
        private int x, y, alt;
        private TrackCalculator calc;
        private ITransponderReceiver receiver;
        private List<string> datastring;
        private string flightNum;
        private DateTime dateTimeNew;
        private IAirSpace airSpace;
        private ISeparationChecker checkSepa;
        

        public ATMSystem(ITransponderReceiver receiver)
        {

            this.receiver = receiver;
            this.receiver.TransponderDataReady += ReceiverOnTransponderReady;
            datastring = new List<string>() {""};
            airSpace = new AirSpace(10000,90000,90000,10000,20000,500);
            //checkSepa = new SeparationChecker(airSpace,);
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


            if (airSpace.IsInAirSpace(x, y))
            {
                int index = CheckIfTrackIsInList(flightNum);
                if (index > 0)
                {

                    calc = new TrackCalculator(Tracks[index].XCoordinate, Tracks[index].YCoordinate, x, y,
                        Tracks[index].LastDateUpdate, dateTimeNew);
                    Track newTrack = new Track(flightNum, x, y, alt, dateTimeNew, calc.CalculateCompassCourse(),
                        calc.CalculateHorizontalVelocity());
                    Tracks[index] = newTrack;
                }
                else
                {
                    Track newTrack = new Track(flightNum, x, y, alt, dateTimeNew);
                    Tracks.Add(newTrack);
                }

                
            }

        }

        public void AddTrack(ITrack track)
        {
            if (Tracks.Count == 0)
            {

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
            Console.WriteLine(dateTimeNew.ToString());
        }

        private void TypeConverter()
        {

                Int32.TryParse(datastring[1], out x);
                Int32.TryParse(datastring[2], out y);
                Int32.TryParse(datastring[3], out alt);
                dateConverter();
            
        }
        

        

       

        

        


    }
}
