using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class ConsolePrinter : IConsolePrinter
    {
        /// <summary>
        /// Receives a List of tracks currently in the airspace, and tracks in conflict by 
        /// </summary>
        /// <param name="tracks">Tracks currently in the airspace</param>
        /// <param name="conflictTags">Tracks in breaking separation condition in the airspace</param>
        public static void Print(List<Track> tracks, string[] conflictTags)
        {
            Console.Clear();
            Console.WriteLine("Airplane Tracks currently in the airspace:\n");

            foreach (var track in tracks)
            {
                Console.WriteLine($"Flight tag: {track.Tag} " +
                                  $"X: {track.XCoordinate} " +
                                  $"Y: {track.YCoordinate} " +
                                  $"Altitude: {track.Altitude} " +
                                  $"Horizontal Velocity: {track.Velocity}" +
                                  $"Compass Course: {track.CurrentCompCourse}");
            }
            Console.WriteLine("\nAirplane tags in conflict");

            foreach (var tag in conflictTags)
            {
                Console.WriteLine($"Airplane tag: {tag}");
            }
        }


        /// <summary>
        /// Receives a List of tracks currently in the airspace, and tracks in conflict by 
        /// </summary>
        /// <param name="tracks">Tracks currently in the airspace</param>
        /// <param name="conflictTags">Tracks in breaking separation condition in the airspace</param>
        void IConsolePrinter.Print(List<Track> tracks, string[] conflictTags) //Only declared for making static function
        {
            ConsolePrinter.Print(tracks, conflictTags);
        }
    }
}
